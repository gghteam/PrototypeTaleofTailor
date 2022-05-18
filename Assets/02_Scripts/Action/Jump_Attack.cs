using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public LayerMask layer;
    public float distance;
    public Vector3 cubeScale;
    private void Start()
    {
        animator = GetComponent<Animator>();
        fsmCore = GetComponent<FsmCore>();
        chaseState = GetComponent<EnemyIdle>();
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
            else if(currentTime <= time)
            {
                transform.position = MathParabola.Parabola(temp, target - dir * 2.5f, 3.5f, currentTime / time);
            }
            else
            {
                Vector3 pos = target - dir * 2.5f;
                //pos.y = 0;
                transform.position = pos;
                isPlay = false;
            }
        }

        if(!isPlay)
        {
            Debug.Log("머워우머ㅓㅁ");
            fsmCore.ChangeState(chaseState);
            isPlay = true;
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
        animator.SetBool("IsMove", false);
        animator.SetTrigger("JumpAttack");
    }

    public override void OnStateLeave()
    {
        isStart = false;
        isPlay = false;
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

        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log("Hit : " + hitColliders[i].name);
            i++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * distance, cubeScale);
    }
}