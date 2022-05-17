using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Animator player_Controller;
    public GameObject cameraObj;
    public GameObject player;
    private bool isStart = false;
    private bool isOpen = false;
    private Rigidbody rigidbody;
    [SerializeField]
    private Text storyTxt;
    [SerializeField]
    private Image panel;

    bool isFade = false;

    private Queue<string> story;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        story = new Queue<string>();
    }

    private void Start()
    {
        story.Enqueue("샐리 아직까지 잠 안자고 뭐하는거야.");
        story.Enqueue("샐리:인형이랑 놀다가 그만..");
        story.Enqueue("샐리 그러다가 내일 늦잠을 자게 될거야. 얼른 잠에 들렴..");
        Invoke("OpenDoor", 1f);
    }

    private void Update()
    {
        if(!isStart)
        {
            panel.color -= new Color(0, 0, 0, Time.deltaTime / 2);
            if (panel.color.a <= 0)
            {
                isStart = true;
            }
        }
        if (isFade)
        {
            panel.color += new Color(0, 0, 0, Time.deltaTime / 2);
            if(panel.color.a >= 1)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    void OpenDoor()
    {
        player_Controller.SetBool("open", true);
        animator.SetBool("open", true);
        cameraObj.transform.DOMove(new Vector3(-5, cameraObj.transform.position.y, -5.5f), 6).OnComplete(() =>
        {
            StartCoroutine("StartStory");
            rigidbody.useGravity = true;
        });
    }

    IEnumerator StartStory()
    {
        //player_Controller.applyRootMotion = false;
        while(story.Count != 0)
        {
            string sentence = story.Dequeue();
            storyTxt.text = sentence;
            yield return new WaitForSeconds(2f);
            storyTxt.text = "";
            yield return new WaitForSeconds(1f);
        }
        player_Controller.SetBool("turn", true);
        player.transform.DORotate(new Vector3(0, -90, 0), 2f).OnComplete(() =>
        {
            player_Controller.SetBool("turn", false);
            isFade = true;
        });
    }
}
