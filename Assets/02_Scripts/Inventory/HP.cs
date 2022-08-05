using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    [Header("HP")]
    [Header("�÷��̾�")]
    [SerializeField]
    float maxPlayerHP = 3000;
    [SerializeField]
    public float playerHP = 3000;
    [Header("����")]
    [SerializeField]
    float maxBossHP = 3000;
    [SerializeField]
    public float bossHP = 3000;
    [SerializeField, Header("HP �����̴� �ӵ�")]
    float sliderSpeed = 5f;

    [Header("���� ����")]
    [SerializeField]
    int danchuCount;
    [SerializeField]
    int maxDanchuCount;

    [Header("HP �����̴�")]
    [SerializeField]
    Image playerHpSlider;
    //Slider playerHpSlider;
    [SerializeField]
    Image whiteSlider;
    // Slider whiteSlider;
    [SerializeField]
    Slider bossHpSlider;

    [Header("���� UI")]
    [SerializeField]
    Image[] clothesButtonImage;
    [SerializeField]
    GameObject bossCanvas;
    [SerializeField]
    Image halfButtonImage;
    [SerializeField]
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    [SerializeField]
    private Image damageImage;
    [SerializeField]
    private float flashSpeed = 5f;
    [SerializeField]
    private Killed killed;

    [SerializeField]
    private Transform bossHitParticlePos;

    bool damaged = false;
    bool isDead = false;
    bool isHalf = false;
    bool isDamage = false;

    EventParam eventParam = new EventParam();

    private void Awake()
    {
        EventManager.StartListening("Rest", ResetHPDanchu);
        EventManager.StartListening("PLUSCLOTHESBUTTON", PlusClothesButton);
        EventManager.StartListening("DAMAGE", DamageSlider);
        EventManager.StartListening("ResetBossHP", ResetBossHP);
    }
    private void Start()
    {
        ResetClothesButton();
        //playerHpSlider.value = whiteSlider.value = playerHP / maxPlayerHP;
        playerHpSlider.fillAmount = whiteSlider.fillAmount = playerHP / maxPlayerHP;
    }
    private void OnDestroy()
    {
        EventManager.StopListening("Rest", ResetHPDanchu);
        EventManager.StopListening("PLUSCLOTHESBUTTON", PlusClothesButton);
        EventManager.StopListening("DAMAGE", DamageSlider);
        EventManager.StopListening("ResetBossHP", ResetBossHP);
    }

    void Update()
    {
        // ������ ������
        UpdateSlider();

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    // ���� ����
    void ResetClothesButton()
    {
        danchuCount = maxDanchuCount;
        ClothesButtonOnOff(maxDanchuCount);
    }

    // �÷��̾ ������ �Ծ��� �� �� ���̳ʽ�
    public void DamageSlider(EventParam eventParam)
    {
        if (isDead) return;
        if (eventParam.stringParam == "PLAYER")
        {
            playerHP -= eventParam.intParam;
            EventManager.TriggerEvent("CameraShake", eventParam);
            Invoke("SliderHit", 0.5f);
            damaged = true;
        }
        else if (eventParam.stringParam == "BOSS")
        {
            //EventManager.TriggerEvent("AttackParticle", eventParam);
            //StartCoroutine(CreateHitParticiel(0.7f));
            ParticlePool particle = PoolManager.Instance.Pop("CFX3_Hit_SmokePuff") as ParticlePool;
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            particle.transform.position = bossHitParticlePos.position + offset;
            bossHP -= eventParam.intParam;
            Debug.Log(bossHP);
            if (bossHP <= 0)
            {
                // ���� ����
                Debug.Log("���� ����");
                killed.Dead = true;
                bossHpSlider.gameObject.SetActive(false);
                bossCanvas.SetActive(false);
            }
        }

    }

    private IEnumerator CreateHitParticiel(float time)
    {
        yield return new WaitForSeconds(time);
        //ParticlePool particle = PoolManager.Instance.Pop("BossHitParticle") as ParticlePool;
        ParticlePool particle = PoolManager.Instance.Pop("CFX3_Hit_SmokePuff") as ParticlePool;
        Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        particle.transform.position = bossHitParticlePos.position + offset;
    }
    // HP ������ UI Update
    void UpdateSlider()
    {
        float hp = playerHP / maxPlayerHP;
        bossHpSlider.value = bossHP / maxBossHP;
        if (isDead) return;
        //playerHpSlider.value = hp;
        playerHpSlider.fillAmount = hp;
        if (isDamage)
        {
            whiteSlider.fillAmount = Mathf.Lerp(whiteSlider.fillAmount, playerHP / maxPlayerHP, Time.deltaTime * sliderSpeed);
            if (playerHpSlider.fillAmount >= whiteSlider.fillAmount - 0.01f)
            {
                isDamage = false;
                whiteSlider.fillAmount = playerHpSlider.fillAmount;
                if (playerHpSlider.fillAmount <= 0) isDead = true;
                else isDead = false;
                if (isDead) Dead();
            }
        }
    }

    private void SliderHit()
    {
        isDamage = true;
    }

    // �׾��� �� ����
    void Dead()
    {
        isDead = true;
        if (!isHalf)
        {
            eventParam.intParam = danchuCount;
            EventManager.TriggerEvent("DEAD", eventParam);
        }
        MinusClothesButton(isHalf ? 1 : 2);
        Invoke("ResetHP", 2f);

    }

    // ���� �߰��� ���̳ʽ�
    void MinusClothesButton(int minus)
    {
        danchuCount -= minus; // ���� �� ����
        if (danchuCount <= 0)
        {
            RealDead();
        }
        else
        {
            ClothesButtonOnOff(danchuCount);
        }
    }

    void RealDead()
    {
        ResetHP();
        ResetClothesButton();
        EventManager.TriggerEvent("Spawn", eventParam);
        eventParam.intParam = maxDanchuCount;
        EventManager.TriggerEvent("ResetDanchu", eventParam);
        //SceneManager.LoadScene("DeadScene");
    }

    void PlusClothesButton(EventParam eventParam)
    {
        danchuCount++; //���� ���� �� +1
        ClothesButtonOnOff(danchuCount);
    }

    //UI ���� ���� Ű��
    void ClothesButtonOnOff(int index)
    {
        int cIndex = 0;
        isHalf = index % 2 == 0 ? false : true;
        if (index % 2 != 0) cIndex = (index - 1) / 2 - 1;
        else cIndex = index / 2 - 1;

        //���� ����
        for (int i = 0; i < maxDanchuCount/2; i++)
            clothesButtonImage[i].gameObject.SetActive(false);
        //�ε��������� Ű��
        for (int i = 0; i < cIndex + 1; i++)
            clothesButtonImage[i].gameObject.SetActive(true);

        Vector3 pos = clothesButtonImage[cIndex].rectTransform.anchoredPosition;

        if (isHalf) pos.x += 21f;
        else pos.x -= 21f;
        halfButtonImage.gameObject.SetActive(isHalf);
        halfButtonImage.rectTransform.anchoredPosition = pos;

    }

    // HP ���� �Լ�
    void ResetHP()
    {
        whiteSlider.fillAmount = playerHP / maxPlayerHP; // ��� �����̴� �ٽ� ä���
        bossHpSlider.value = bossHP / maxBossHP;
        playerHpSlider.fillAmount = Mathf.Lerp(playerHpSlider.fillAmount, 1, Time.deltaTime * sliderSpeed + 2); //������ ��
        playerHP = maxPlayerHP; // HP�� �ʱ�ȭ
        isDead = false;
    }

    public void ResetBossHP(EventParam eventParam)
    {
        bossHP = maxBossHP;
        bossHpSlider.value = bossHP / maxBossHP;
    }

    private void ResetHPDanchu(EventParam eventParam)
    {
        eventParam.intParam = 6;
        EventManager.TriggerEvent("ResetDanchu", eventParam);
        ResetHP();
        ResetClothesButton();
    }
}
