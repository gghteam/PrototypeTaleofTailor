using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
	// onstateenter is called when a transition starts and the state machine starts to evaluate this state
	//override public void onstateenter(animator animator, animatorstateinfo stateinfo, int layerindex)
	//{
	//    
	//}

	private EventParam eventParam;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		EventManager.TriggerEvent("Start", eventParam);
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateExit(animator, stateInfo, layerIndex);
		EventManager.TriggerEvent("Attack", eventParam);
	}
}
