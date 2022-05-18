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
        [Header("�ð�")]
        public float duration;
        [Header("����")]
        public float strength;
        [Header("������ ������")]
        public float randomness;
        [Header("���� ����")]
        public int vibrato;
    }

    [Header("DoShake�Լ��� ���� �Ű����� ����ü")]
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

    [SerializeField, Header("���׹̳� ��� �ӵ� ���� ��, ���� ���� ������")]
    private float steminaRecoveringDelay = 3f;

    [SerializeField, Header("���׹̳� ���ϴ� ��")]
    private float plusSteminaValue = 2f;
    [SerializeField, Header("���׹̳� ���� ��")]
    private float minusSteminaValue = 1f;

    // ���� �ʿ䰡 �ֳ�?
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
    /// ���׹̳��� �� �� �ִ� ������ Ȯ�� �ϴ� bool�� ���� �Լ�
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
    /// ���׹̳� ���
    /// </summary>
    private void SteminaRecovering()
    {
        // ���׹̳� ����� && ���׹̳��� �ִ� ���׹̳����� ���� ��
        if (recovering && (stemina <= MAX_STEMINA))
        {
            Stemina += Time.deltaTime / steminaRecoveringDelay;
        }

        if (Stemina > MAX_STEMINA)
            Stemina = MAX_STEMINA;
    }

    /// <summary>
    /// ���׹̳� ���� �ణ�� ���� ���� ���׹̳� �����Ű�� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator SteminaRecoveringDelayCoroutine()
    {
        recovering = false;
        yield return new WaitForSecondsRealtime(.5f);
        recovering = true;
    }
}
