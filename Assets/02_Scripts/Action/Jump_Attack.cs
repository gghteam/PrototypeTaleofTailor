using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jump_Attack : FsmState
{
    [SerializeField]
    private float time;
    protected float currentTime;

    [SerializeField]
    private GameObject targetPos;

    private Vector3 temp;
    private Vector3 dir;
    private Vector3 target;

    bool isStart = false;
    bool isPlay = true;

    private Animator animator;

    private FsmCore fsmCore;
    private EnemyIdle chaseState;

    [SerializeField] CrackControll _CrackPrefab;
    Vector3 direction;
    Vector3 originPos;

    public LayerMask layer;
    public float distance;
    public Vector3 cubeScale;

    EventParam eventParam = new EventParam();

    [SerializeField]
    private Transform camera;

    private readonly int isMove = Animator.StringToHash("IsMove");
    private readonly int jumpAttack = Animator.StringToHash("JumpAttack");
    private void Start()
    {
        animator = GetComponent<Animator>();
        fsmCore = GetComponent<FsmCore>();
        chaseState = GetComponent<EnemyIdle>();
        originPos = camera.localPosition;
    }

    private void Update()
    {
        /*
       //Debug.Log(Vector3.Distance(transform.position, targetPos.position));
        if (Vector3.Distance(transform.position, targetPos.position) >= 10 && !isStart)
        {
            temp = transform.position;
            target = targetPos.position;
            dir = (targetPos.position - transform.position).normalized;

            isStart = true;
            isPlay = true;
            animator.SetTrigger("JumpAttack");

            Debug.Log("좀 가라");
        }
        */
        if (isPlay)
        {
            currentTime += Time.deltaTime;
            transform.LookAt(targetPos.transform);

            if (currentTime <= time * 0.6f)
            {
                transform.position = MathParabola.Parabola(temp, targetPos.transform.position - dir * 2.5f, 3.5f, currentTime / time);
                target = targetPos.transform.position;
                dir = (target - transform.position).normalized;
            }
            else if (currentTime <= time)
            {
                transform.position = MathParabola.Parabola(temp, target - dir * 2.5f, 3.5f, currentTime / time);
            }
            else
            {
                Vector3 pos = target - dir * 2.5f;
                pos.y = 0;
                transform.position = pos; // 여기 버구 추정
                isPlay = false;
            }
        }

        if (!isPlay)
        {
            Debug.Log("머워우머ㅓㅁ");
            fsmCore.ChangeState(chaseState);
            this.GetComponent<Jump_Attack>().enabled = false;
            //isPlay = true;
        }

    }

    public override void OnStateEnter()
    {
        Debug.Log("ㅋㅋㅋ");
        temp = transform.position;
        target = targetPos.transform.position;
        dir = (target - transform.position).normalized;

        currentTime = 0;
        isStart = true;
        isPlay = true;
        animator.SetBool(isMove, false);
        animator.SetTrigger(jumpAttack);
    }

    public override void OnStateLeave()
    {
        isStart = false;
        isPlay = false;
        animator.SetBool(isMove, true);
        Debug.Log("떠나라!");
    }

    public void AnimationCallback_SlamEffect()
    {
        direction = transform.forward;
        Debug.Log("접근중");
        Vector3 pos = transform.position;
        pos.y = 0;
        CrackControll crackControll = Instantiate(_CrackPrefab, pos, Quaternion.identity);
        crackControll.transform.forward = direction;
        crackControll.Open(15);
        MyCollisions();
    }

    public void MyCollisions()
    {
        Debug.Log("MyCOllision");

        Collider[] hitColliders = Physics.OverlapBox(transform.position + transform.forward * distance, cubeScale / 2, Quaternion.identity, layer);

        if (hitColliders.Length > 0)
        {
            Debug.Log("ㅋ깍힘");
            StartCoroutine(Shake(0.5f, 0.15f));
            eventParam.intParam = 2000;
            eventParam.stringParam = "PLAYER";
            EventManager.TriggerEvent("DAMAGE", eventParam);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * distance, cubeScale);
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            camera.localPosition = (Vector3)Random.insideUnitCircle * _amount + camera.localPosition;

            timer += Time.deltaTime;
            yield return null;
        }
        camera.localPosition = originPos;

    }
}