using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmCore : MonoBehaviour {
	/// <summary>
	/// 트레지션 규칙
	/// </summary>
	[System.Serializable]
	public class TransitionRule
	{
		[Tooltip("다음 상태")]
		public FsmState Next;

		[Tooltip("전환이 발생할 조건")]
		public FsmCondition Cond;

		[Tooltip("우선순위")]
		public int Priority;

		[Tooltip("조건이 충족되지 않을 때 전환이 발생하는 경우")]
		public bool Not;

		[System.NonSerialized]
		public StateEntity StateEntityOfNext;
	}

	[System.Serializable]
	public class StateEntity
	{
		[Tooltip("상태 구성요소")]
		public FsmState State;

		[Tooltip("전환 규칙 리스트")]
		public TransitionRule[] Transitions;
	}


	[Tooltip("현재 상태에 관계없이 검사되는 전환 규칙")]
	public TransitionRule[] GlobalState;

	[Tooltip("이 FSM에서 발견된 모든 상태 및 전환 규칙 목록(첫번째 상태 == 초기 상태)")]
	public StateEntity[] States;

	[Tooltip("현재 상태")]
	private StateEntity current;


	/// <summary>
	/// 현재 상태와 그것에 대한 전환 규칙 가져오기
	/// </summary>
	public StateEntity GetCurrentState()
	{
		return current;
	}

	/// <summary>
	/// 상태 변경 메소드
	/// </summary>
	/// <param name="Next">변경할 상태</param>
	public void ChangeState(FsmState Next)
	{
		//Debug.Log("LEAVE");
		//현재 스테이지를 떠남
		leaveCurrentState();
		current = null;

		//Next상태가 나올때까지 반복
		foreach (StateEntity se in States)
		{
			if (Next == se.State)
			{
				//햔재 상태를 Next 상태로 변경
				current = se;
				break;
			}
		}

		//FsmCore에 나열되지 않은 상태
		if (current == null)
		{
			//새로운 StateEntity 생성
			current = new StateEntity();
			//새로운 생성한 상태를 Next로 변경
			current.State = Next;
			current.Transitions = null;
		}

		//현재 상태로 들어가기
		enterCurrentState();
	}

	void Start()
	{
		//상태가 있다면
		if (States.Length > 0)
		{
			//초기 세팅
			current = States[0];

			//초기 세팅을 제외한 나머지 상태를 비활성함
			for (int i = 1; i < States.Length; ++i)
				if (States[i].State != null)
					States[i].State.enabled = false;

			//현재 상태로 들어가기
			enterCurrentState();
		}
	}


	void LateUpdate()
	{
		//Debug.Log(current.State.);
		//선택할 규칙 초기세팅
		TransitionRule chosenTransition = null;

		//현재 상태에 관계없이 검사되는 전환 규칙들을 실행
		for (int i = 0; i < GlobalState.Length; ++i)
		{
			TransitionRule tr = GlobalState[i];

			//(검사되는 규칙이 있고 선택한 규칙이 없을때) 또는 (검사되는 규칙이 우선순위↑)
			//그리고 조건이 충족되면(XOR연산자를 통하여 Not처리도 함)
			if (tr != null &&
				(chosenTransition == null || tr.Priority > chosenTransition.Priority) &&
				(tr.Cond != null && (tr.Not ^ tr.Cond.IsSatisfied(current != null ? current.State : null, tr.Next))))

				//선택할 규칙을 변경
				chosenTransition = tr;
		}

		//현재 상태랑 현재 상태에 대한 규칙이 있다면
		if (current != null && current.Transitions != null)
		{
			//모든 조건을 다 순회
			for (int i = 0; i < current.Transitions.Length; ++i)
			{
				//현재 인덱스의 규칙 저장
				TransitionRule tr = current.Transitions[i];

				//(검사되는 규칙이 있고 선택한 규칙이 없을때) 또는 (검사되는 규칙이 우선순위↑)
				//그리고 조건이 충족되면(XOR연산자를 통하여 Not처리도 함)
				if (tr != null &&
					(chosenTransition == null || tr.Priority > chosenTransition.Priority) &&
					(tr.Cond != null && (tr.Not ^ tr.Cond.IsSatisfied(current != null ? current.State : null, tr.Next))))

					//선택할 규칙을 변경
					chosenTransition = tr;
			}
		}

		//선택한 규칙이 있다면
		if (chosenTransition != null)
		{


			//현재 상태를 선택한 규칙에 다음 상태로 저장
			current = chosenTransition.StateEntityOfNext;

			//현재 상태가 NULL이면(규칙에 다음 상태가 X)
			if (current == null)
			{

				// 선택한 상태의 다음 상태가 나올때까지 반복
				foreach (StateEntity se in States)
				{
					if (se != null && chosenTransition.Next == se.State)
					{
						//Setting
						chosenTransition.StateEntityOfNext = se;
						//현재 상태를 변경
						current = se;
						break;
					}
				}

				//현재 상태가 NULL이면(다음 상태가 없음)
				if (current == null)
				{
					// FsmCore에 등록되지 않은 상태에 도달

					//생성
					current = new StateEntity();
					//연결
					current.State = chosenTransition.Next;
					current.Transitions = null;
				}
			}

			//현재 상태로 들어가기
			enterCurrentState();
		}
	}

	/// <summary>
	/// 현재 상태를 떠나기
	/// </summary>
	private void leaveCurrentState()
	{
		//Debug.Log("LEAVE");
		if (current != null && current.State != null)
		{
			current.State.OnStateLeave();
			//현재 상태 비활성화
			current.State.enabled = false;
		}
	}

	/// <summary>
	/// 현재 상태 들어오기
	/// </summary>
	private void enterCurrentState()
	{
		if (current != null && current.State != null)
		{
			//현재 상태 활성화
			current.State.enabled = true;
			current.State.OnStateEnter();
		}
	}
}
