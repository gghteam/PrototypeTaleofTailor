using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerParrying : Character
{
    //재필아 안녕???? 안녕
    [SerializeField, Header("패링할시에 사용하는 스테미나 양")]
    private int parriyngStemina = 1;
    [SerializeField, Header("패링 성공시에 얻는 스테미나 양")]
    private int parringSuccessStemina = 2;

    //[SerializeField, Header("패링 키")]
    //private bool parryingKeyCode;
    [SerializeField, Header("패링 딜레이")]
    private float parryingDelay = .5f;

    [SerializeField, Tooltip("시야각 범위")]
    private float viewAngle = 60f;

    [SerializeField]
    private Transform swordPos;

    public UnityEvent OnParrying = null;

    private float timer = 0f;

    private bool isParrying = false;
    public bool IsParrying { get => isParrying; }

    private readonly int parrying = Animator.StringToHash("isParrying");
    void Start()
    {
        timer = parryingDelay;
    }

    void Update()
    {
        timer += Time.deltaTime;

        ani.SetBool(parrying, IsParrying);

        if (timer >= parryingDelay)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (SteminaManager.Instance.CheckStemina(parriyngStemina))
                {
                    SteminaManager.Instance.MinusStemina(parriyngStemina);
                    Parrying();
                    timer = 0f;
                }
                else
                    UIManager.Instance.UseSteminaFailedEffect();
            }
        }
    }

    /// <summary>
    /// 패링 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ParryingCoroutine()
    {
        isParrying = true;
        yield return new WaitForSeconds(.3f);
        isParrying = false;
    }

    /// <summary>
    /// 패링 함수
    /// </summary>
    public void Parrying()
    {
        // TODO : Parring 행동하기
        Debug.Log("패링");
        StartCoroutine(ParryingCoroutine());
    }

    /// <summary>
    /// tr과 나의 시야각을 계산해서 tr이 내 시야내에 있는지를 반환해주는 함수
    /// </summary>
    /// <param name="tr"></param>
    /// <returns></returns>
    public bool IsInViewangle(Transform tr)
    {
        Vector3 targetDir = (tr.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, targetDir);

        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (theta <= viewAngle)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 패링 실패시에 행동 함수
    /// </summary>
    public void FailedParrying()
    {
        // TODO : Failed Parring
        Debug.Log("패링 실패!");
    }

    /// <summary>
    /// 패링 성공시에 행동 함수
    /// </summary>
    public void SuccessParrying()
    {
        // TODO : Success Parring
        Debug.Log("패링 성공!");
        SteminaManager.Instance.PlusStemina(parringSuccessStemina); // 스테미나 증가
        ParticlePool parryingParticle = PoolManager.Instance.Pop("ParryingParticle") as ParticlePool;
        parryingParticle.transform.position = swordPos.position;

        OnParrying?.Invoke();
    }
}