using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Cutscene : MonoBehaviour
{
    public Image cutsceneImage;
    public Sprite[] cutsceneSprites;

    [Header("자동으로 넘어갈 시간")]
    public float nTime = 5f;

    private int currentIndex = 0;
    private float timer = 0f;

    private void Start()
    {
        ShowCurrentImage();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EndEnd();
            return;
        }

        timer += Time.deltaTime;

        if (Keyboard.current.spaceKey.wasPressedThisFrame || timer >= nTime)
        {
            NextImage();
        }
    }

    private void ShowCurrentImage()
    {
        cutsceneImage.sprite = cutsceneSprites[currentIndex];
        cutsceneImage.SetNativeSize(); 
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

    private void EndEnd()
    {
        gameObject.SetActive(false);
    }
}