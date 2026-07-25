using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [Header("스테이지 번호")]
    public int stagenumber = 1;

    [Header("HP")]
    public int maxHP = 3;
    public int currentHP;

    [Header("하트 이미지")]
    public Image[] hearts;

    [Header("각 스테이지 하트 이미지")]
    public Sprite[] heartsprites;

    [Header("회색 하트")]
    public Sprite[] grayheartsprites;

    [Header("하트 흔들리는거")]
    public float shakesecond = 0.4f;
    public float shake = 12f;

    private Sprite currentheart;

    private bool Damage = false; //지금 흔들리고 있는지 확인

    void Start()
    {
        SetStageHeart(); //스테이지 번호 보고 사용할 하트 정하기

        currentHP = maxHP; 

        UpdateHeart();
    }

    void Update()
    {
        // 테스트하려고 해둔것!!
        // 숫자 1을 누르면 HP 1 감소
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(1);
        }

        // 숫자 2를 누르면 HP 2 감소
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TakeDamage(2);
        }
    }

    void SetStageHeart()
    {
        int stageIndex = stagenumber - 1;

        currentheart = heartsprites[stageIndex];
        }

    public void TakeDamage(int damage)
    {
        if (Damage)
        {
            return;
        }

        if (currentHP <= 0)
        {
            return;
        }

        int previousHP = currentHP;

        currentHP = currentHP - damage;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        StartCoroutine(DamageHeartRoutine(previousHP, currentHP));
    }

    IEnumerator DamageHeartRoutine(int previousHP, int newHP) //흔들고 회색하트로 바꾸기
    {
        Damage = true;

        for (int heartNumber = previousHP; heartNumber > newHP; heartNumber--)
        {
            int heartIndex = heartNumber - 1;

            if (heartIndex >= 0 && heartIndex < hearts.Length)
            {
                yield return StartCoroutine(ShakeHeart(hearts[heartIndex]));

                hearts[heartIndex].sprite = grayheartsprites[heartIndex];
            }
        }

        Damage = false;

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    IEnumerator ShakeHeart(Image heart)
    {
        RectTransform heartRect = heart.rectTransform; //원래 하트 위치정보 가져오기

        Vector2 originalPosition = heartRect.anchoredPosition;

        float elapsedTime = 0f; //몇 초 흔들리는지 저장해줌

        while (elapsedTime < shakesecond)
        {
            float randomX = Random.Range(-shake, shake);
            float randomY = Random.Range(-shake, shake);

            heartRect.anchoredPosition =
                originalPosition + new Vector2(randomX, randomY);

            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        heartRect.anchoredPosition = originalPosition;
    }

    void UpdateHeart() //현재 HP 맞게 하트 설정하기
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = true;

            if (i < currentHP)
            {
                hearts[i].sprite = currentheart;
            }
            else
            {
                hearts[i].sprite = grayheartsprites[i];
            }
        }
    }

    void GameOver()
    {
        Debug.Log("끝끝 죽음!!");
    }
}