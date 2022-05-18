using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SteminaManager : MonoSingleton<SteminaManager>
{
    [Serializable]
    public struct DoShakeField
    {
        [Header("시간")]
        public float duration;
        [Header("강도")]
        public float strength;
        [Header("떨림의 랜덤성")]
        public float randomness;
        [Header("진동 정도")]
        public int vibrato;
    }

    [Header("DoShake함수에 쓰는 매개변수 구조체")]
    public DoShakeField shakeField;

    private float stemina = 5f;
    public readonly float MAX_STEMINA = 5f;
    public float Stemina
    {
        get { return stemina; }
        private set
        {
            stemina = value;
            UIManager.Instance.SteminaBarValue();
        }
    }

    [SerializeField, Header("스테미나 재생 속도 조절 값, 높을 수록 느려짐")]
    private float steminaRecoveringDelay = 3f;

    [SerializeField, Header("스테미나 더하는 값")]
    private float plusSteminaValue = 2f;
    [SerializeField, Header("스테미나 빼는 값")]
    private float minusSteminaValue = 1f;

    // 있을 필요가 있나?
    private bool recovering = true;

    private void Start()
    {
        Stemina = MAX_STEMINA;
    }

    void Update()
    {
        SteminaRecovering();
    }

    /// <summary>
    /// 스테미나를 쓸 수 있는 량인지 확인 하는 bool값 리턴 함수
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool CheckStemina(float value)
    {
        if (Stemina > value)
            return true;
        else
            return false;
    }

    public void MinusStemina(float value)
    {
        Stemina -= value;
        StartCoroutine(SteminaRecoveringDelayCoroutine());
    }

    public void PlusStemina(float input)
    {
        float value = Mathf.Min(input, MAX_STEMINA - Stemina);
        Stemina += value;
        //StartCoroutine(PlusSteminaCoroutine(input));
    }

    /// <summary>
    /// 스테미나 재생
    /// </summary>
    private void SteminaRecovering()
    {
        // 스테미나 재생중 && 스테미나가 최대 스테미나보다 낮을 때
        if (recovering && (stemina <= MAX_STEMINA))
        {
            Stemina += Time.deltaTime / steminaRecoveringDelay;
        }

        if (Stemina > MAX_STEMINA)
            Stemina = MAX_STEMINA;
    }

    /// <summary>
    /// 스테미나 사용시 약간의 텀을 준후 스테미나 재생시키는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator SteminaRecoveringDelayCoroutine()
    {
        recovering = false;
        yield return new WaitForSecondsRealtime(.5f);
        recovering = true;
    }
}
