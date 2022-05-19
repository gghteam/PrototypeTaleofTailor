using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    [Header("HP")]
    [Header("플레이어")]
    [SerializeField]
    float maxPlayerHP = 3000;
    [SerializeField]
    public float playerHP = 3000;
    [Header("보스")]
    [SerializeField]
    float maxBossHP = 3000;
    [SerializeField]
    public float bossHP = 3000;
    [SerializeField, Header("HP 슬라이더 속도")]
    float sliderSpeed = 5f;

    [Header("단추 갯수")]
    [SerializeField]
    int danchuCount;
    [SerializeField]
    int maxDanchuCount;

    [Header("HP 슬라이더")]
    [SerializeField]
    Image playerHpSlider;
    //Slider playerHpSlider;
    [SerializeField]
    Image whiteSlider;
    // Slider whiteSlider;
    [SerializeField]
    Slider bossHpSlider;

    [Header("단추 UI")]
    [SerializeField]
    Image[] clothesButtonImage;
    [SerializeField]
    Image halfButtonImage;
    [SerializeField]
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    [SerializeField]
    private Image damageImage;
    [SerializeField]
    private float flashSpeed = 5f;

    bool damaged = false;
    bool isDead = false;
    bool isHalf = false;
    bool isDamage = false;

    EventParam eventParam = new EventParam();

    private void Awake()
    {
        EventManager.StartListening("PLUSCLOTHESBUTTON", PlusClothesButton);
        EventManager.StartListening("DAMAGE", DamageSlider);
    }
    private void Start()
    {
        ResetClothesButton();
        //playerHpSlider.value = whiteSlider.value = playerHP / maxPlayerHP;
        playerHpSlider.fillAmount = whiteSlider.fillAmount = playerHP / maxPlayerHP;
    }
    private void OnDestroy()
    {
        EventManager.StopListening("PLUSCLOTHESBUTTON", PlusClothesButton);
        EventManager.StopListening("DAMAGE", DamageSlider);
    }

    void Update()
    {
        // 데미지 입히기
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

    // 단추 리셋
    void ResetClothesButton()
    {
        ClothesButtonOnOff(maxDanchuCount);
    }

    // 플레이어가 데미지 입었을 때 피 마이너스
    public void DamageSlider(EventParam eventParam)
    {
        if (isDead) return;
        if (eventParam.stringParam == "PLAYER")
        {
            playerHP -= eventParam.intParam;
            Invoke("SliderHit", 0.5f);
            damaged = true;

        }
        else if (eventParam.stringParam == "BOSS")
        {
            bossHP -= eventParam.intParam;
            Debug.Log(bossHP);
            if (bossHP <= 0)
            {
                // 보스 죽음
                Debug.LogError("보스 죽음");
                bossHpSlider.gameObject.SetActive(false);
            }
        }

    }

    // HP 게이지 UI Update
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

    // 죽었을 때 실행
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

    // 단추 추가와 마이너스
    void MinusClothesButton(int minus)
    {
        danchuCount -= minus; // 단추 수 빼기
        if (danchuCount <= 0)
        {
            SceneManager.LoadScene("DeadScene");
        }
        else
        {
        ClothesButtonOnOff(danchuCount);
        }
        
    }
    void PlusClothesButton(EventParam eventParam)
    {
        danchuCount++; //가진 단추 수 +1
        ClothesButtonOnOff(danchuCount);
    }

    //UI 단추 끄고 키기
    void ClothesButtonOnOff(int index)
    {
        int cIndex = 0;
        isHalf = index % 2 == 0 ? false : true;
        if (index % 2 != 0) cIndex = (index - 1) / 2 - 1;
        else cIndex = index / 2 - 1;

        //전부 끄기
        for (int i = 0; i < maxDanchuCount/2; i++)
            clothesButtonImage[i].gameObject.SetActive(false);
        //인덱스까지만 키기
        for (int i = 0; i < cIndex + 1; i++)
            clothesButtonImage[i].gameObject.SetActive(true);

        Vector3 pos = clothesButtonImage[cIndex].rectTransform.anchoredPosition;

        if (isHalf) pos.x += 21f;
        else pos.x -= 21f;
        halfButtonImage.gameObject.SetActive(isHalf);
        halfButtonImage.rectTransform.anchoredPosition = pos;

    }

    // HP 리셋 함수
    void ResetHP()
    {
        whiteSlider.fillAmount = playerHP / maxPlayerHP; // 흰색 슬라이더 다시 채우기
        bossHpSlider.value = bossHP / maxBossHP;
        playerHpSlider.fillAmount = Mathf.Lerp(playerHpSlider.fillAmount, 1, Time.deltaTime * sliderSpeed + 2); //서서히 참
        playerHP = maxPlayerHP; // HP도 초기화
        isDead = false;
    }

}
