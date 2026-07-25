using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class menutext : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI text;

    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    public Vector3 clickScale = new Vector3(0.95f, 0.95f, 1f);

    public GameObject chapterPanel;
    public RectTransform chapterRect;
    public LayoutElement chapterLayout;

    public GameObject[] chapterObjects;

    public float openHeight = 180f;
    public float speed = 8f;

    private Coroutine chapterRoutine;
    private Coroutine showRoutine;

    void Start()
    {
        if (text == null)
            text = GetComponentInChildren<TextMeshProUGUI>();

        transform.localScale = normalScale;

        if (chapterPanel != null)
            chapterPanel.SetActive(false);

        if (chapterRect != null)
            chapterRect.sizeDelta = new Vector2(chapterRect.sizeDelta.x, 0);

        if (chapterLayout != null)
            chapterLayout.preferredHeight = 0;

        if (chapterObjects != null)
        {
            foreach (GameObject chapter in chapterObjects)
            {
                chapter.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverScale;
        OpenChapter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = normalScale;
        CloseChapter();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = clickScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = hoverScale;
    }

    void OpenChapter()
    {
        if (chapterPanel == null || chapterRect == null)
            return;

        chapterPanel.SetActive(true);

        foreach (GameObject chapter in chapterObjects)
        {
            chapter.SetActive(false);
        }

        if (showRoutine != null)
            StopCoroutine(showRoutine);

        showRoutine = StartCoroutine(ShowChapters());

        if (chapterRoutine != null)
            StopCoroutine(chapterRoutine);

        chapterRoutine = StartCoroutine(AnimateChapter(openHeight));
    }

    void CloseChapter()
    {
        if (chapterPanel == null || chapterRect == null || chapterLayout == null)
            return;

        if (chapterRoutine != null)
            StopCoroutine(chapterRoutine);

        chapterLayout.preferredHeight = 0;
        chapterRect.sizeDelta = new Vector2(chapterRect.sizeDelta.x, 0);

        foreach (GameObject chapter in chapterObjects)
        {
            chapter.SetActive(false);
        }

        chapterPanel.SetActive(false);

        if (showRoutine != null)
            StopCoroutine(showRoutine);
    }

    IEnumerator AnimateChapter(float targetHeight)
    {
        while (Mathf.Abs(chapterLayout.preferredHeight - targetHeight) > 0.5f)
        {
            float newHeight = Mathf.Lerp(
                chapterLayout.preferredHeight,
                targetHeight,
                Time.unscaledDeltaTime * speed);

            chapterLayout.preferredHeight = newHeight;
            chapterRect.sizeDelta = new Vector2(chapterRect.sizeDelta.x, newHeight);

            yield return null;
        }

        chapterLayout.preferredHeight = targetHeight;
        chapterRect.sizeDelta = new Vector2(chapterRect.sizeDelta.x, targetHeight);

        if (targetHeight == 0)
            chapterPanel.SetActive(false);
    }

    IEnumerator ShowChapters()
    {
        foreach (GameObject chapter in chapterObjects)
        {
            chapter.SetActive(true);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}