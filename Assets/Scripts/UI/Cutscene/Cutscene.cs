using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class Cutscene : MonoBehaviour
{
    public Image cutsceneImage;
    public Image blackImage;
    public Sprite[] cutsceneSprites;

    [Header("자동으로 넘어갈 시간")]
    public float nTime = 5f;
    public float fadeSpeed = 2f;
    private int currentIndex = 0;
    private float timer = 0f; //이미지 나오고 지난 시간
    private bool change = false; //이미지가 넘어가는 중인지 확인

    private void Start()
    {
        ShowCurrentImage();
        blackImage.color = new Color(0f, 0f, 0f, 0f);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EndEnd();
            return;
        }

        if (change)
        {
            return;
        }

        timer += Time.deltaTime;
        if (Keyboard.current.spaceKey.wasPressedThisFrame ||
            timer >= nTime)
        {
            StartCoroutine(BlackNextImage());
        }
    }

    private void ShowCurrentImage()
    {
        cutsceneImage.sprite = cutsceneSprites[currentIndex];
        timer = 0f;
    }

    private void NextImage()
    {
        currentIndex++;
        if (currentIndex >= cutsceneSprites.Length)
        {
            EndEnd();
            return;
        }

        ShowCurrentImage();
    }

    private IEnumerator BlackNextImage()
    {
        change = true;

        for (float alpha = 0f; alpha < 1f; alpha += Time.deltaTime * fadeSpeed)
        {
            blackImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        blackImage.color = new Color(0f, 0f, 0f, 1f);

        NextImage();

        if (!gameObject.activeSelf)
        {
            yield break;
        }

        for (float alpha = 1f; alpha > 0f; alpha -= Time.deltaTime * fadeSpeed)
        {
            blackImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        blackImage.color = new Color(0f, 0f, 0f, 0f);

        change = false;
    }

    private void EndEnd()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}