using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SteminaManager : MonoBehaviour
{
    // 간이 싱글톤
    public static SteminaManager Instance = null;

    // 디버깅용. 삭제 예정
    //public Text text = null;

    public Image intImage = null;
    public Image floatImage = null;


    public GameObject steminaBar = null;

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
    private const float MAX_STEMINA = 5f;
    public float Stemina
    {
        get { return stemina; }
    }

    [SerializeField, Header("스테미나 재생 속도 조절 값, 높을 수록 느려짐")]
    private float steminaRecoveringDelay = 3f;

    [SerializeField, Header("스테미나 더하는 값")]
    private float plusSteminaValue = 2f;
    [SerializeField, Header("스테미나 빼는 값")]
    private float minusSteminaValue = 1f;

    [SerializeField, Header("더하는 키")]
    private KeyCode plusKeyCode = KeyCode.F;
    [SerializeField, Header("빼는 키")]
    private KeyCode minusKeyCode = KeyCode.D;

    // 있을 필요가 있나?
    private bool recovering = true;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple SteminaManager is running!");
        }
        Instance = this;
    }

    private void Start()
    {
        stemina = 0;
    }

    void Update()
    {
        SteminaRecovering();

        // 디버깅 용. 삭제 예정
        //text.text = string.Format("{0:F2}", stemina);

        intImage.fillAmount = (int)stemina / MAX_STEMINA;
        floatImage.fillAmount = (float)stemina / MAX_STEMINA;

        if (Input.GetKeyDown(minusKeyCode))
        {
            if (CheckStemina(minusSteminaValue))
            {
                MinusStemina(1f);
            }
            else
            {
                MinusStemianFailedEffect();
            }
        }

        if (Input.GetKeyDown(plusKeyCode))
            PlusStemina(plusSteminaValue);
    }

    private void MinusStemianFailedEffect()
    {
        // TODO : 스테미나바 흔들기
        steminaBar.transform.DOShakePosition(shakeField.duration, shakeField.strength, shakeField.vibrato, shakeField.randomness);
    }

    /// <summary>
    /// 스테미나를 쓸 수 있는 량인지 확인 하는 bool값 리턴 함수
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool CheckStemina(float value)
    {
        if (stemina > value)
            return true;
        else
            return false;
    }

    public void MinusStemina(float value)
    {
        stemina -= value;
        StartCoroutine(SteminaRecoveringDelayCoroutine());
        //StartCoroutine(MinusSteminaCoroutine(value));
    }

    /// <summary>
    /// 스테미나 줄어드는 애니메니션을 넣어주는 코루틴.
    /// 삭제 예정
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    IEnumerator MinusSteminaCoroutine(float input)
    {
        float value = input;
        recovering = false;
        while(input > 0)
        {
            input -= Time.deltaTime;
            stemina -= Time.deltaTime;
            //yield return new WaitForSeconds(.0001f);
            yield return null;
        }
        recovering = true;
    }
    public void PlusStemina(float input)
    {
        float value = Mathf.Min(input, MAX_STEMINA - stemina);
        stemina += value;
        //StartCoroutine(PlusSteminaCoroutine(input));
    }

    /// <summary>
    /// 스테미나 늘어나는 애니메이션을 넣어주는 코루틴.
    /// 삭제 예정
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    IEnumerator PlusSteminaCoroutine(float input)
    {
        float value = input;
        recovering = false;
        while (input > 0)
        {
            input -= Time.deltaTime;
            stemina += Time.deltaTime;
            //yield return new WaitForSeconds(.0001f);
            yield return null;
        }
        recovering = true;
    }

    /// <summary>
    /// 스테미나 재생
    /// </summary>
    private void SteminaRecovering()
    {
        // 스테미나 재생중 && 스테미나가 최대 스테미나보다 낮을 때
        if (recovering && (stemina <= MAX_STEMINA))
        {
            stemina += Time.deltaTime / steminaRecoveringDelay;
        }

        if (stemina > MAX_STEMINA)
            stemina = MAX_STEMINA;
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
