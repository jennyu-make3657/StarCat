using System.Collections;
using UnityEngine;
using TMPro;

public class KeyUI : MonoBehaviour
{
    public enum KeyType
    {
        Up, Down, Left, Right, SpecialAction, Confirm, SkipPause
    }

    [Header("원래꺼")]
    public GameObject keyText;

    [Header("입력 안내")]
    public GameObject guideText;

    [Header("변경할 키")]
    public KeyType keyType;

    private CanvasGroup guideCanvasGroup; //투명도 조절
    private Coroutine blink; //깜빡임

    private bool waitingForKey = false; //새로운 키 입력 기다리는지 확인

    private TextMeshProUGUI kkey;



    private void Start()
    {

        guideCanvasGroup = guideText.GetComponent<CanvasGroup>();
        kkey = keyText.GetComponentInChildren<TextMeshProUGUI>(true);

        guideCanvasGroup.alpha = 1f;


        guideText.SetActive(false);

        keyText.SetActive(true);
        
    }

    private void Update()
    {
        if(!waitingForKey)
            return;
        
        foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode))) //키 확인
        {
            //새로 선택한 키 찾으면 변수로 전달
            if(Input.GetKeyDown(key))
            {
                SaveNewKey(key);
                break;
            }
        }
    }

    private void SaveNewKey(KeyCode newKey)
    {
        switch(keyType)
        {
            case KeyType.Up:
                KeyManager.Instance.upKey = newKey;
                break;

            case KeyType.Down:
                KeyManager.Instance.downKey=newKey;
                break;
            
            case KeyType.Left:
                KeyManager.Instance.leftKey=newKey;
                break;

            case KeyType.Right:
                KeyManager.Instance.rightKey=newKey;
                break;
            
            case KeyType.SpecialAction:
                KeyManager.Instance.specialActionKey=newKey;
                break;
            
            case KeyType.Confirm:
                KeyManager.Instance.confirmKey=newKey;
                break;
            
            case KeyType.SkipPause:
                KeyManager.Instance.skipPauseKey=newKey;
                break;
            

        }
        if (newKey == KeyCode.Escape)
    {
        kkey.text = "ESC";
    }
    else if (newKey == KeyCode.Return)
    {
        kkey.text = "ENTER";
    }
    else if (newKey == KeyCode.Space)
    {
        kkey.text = "SPACE BAR";
    }
    else
    {
        kkey.text = newKey.ToString();
    }
        waitingForKey=false;
        HideGuideText();
    }

    // 버튼 누르면 실행
    public void ShowGuideText()
    {
        keyText.SetActive(false);
        guideText.SetActive(true);

        if (blink != null)
        {
            StopCoroutine(blink);
            
        }

        blink = StartCoroutine(BlinkGuideText());
        waitingForKey = true;
    }

    // 안내 문구를 다시 끌 때 실행
    public void HideGuideText()
    {
        if(blink!=null)
        {
            StopCoroutine(blink);
            blink = null;
        }
        
        
        guideCanvasGroup.alpha = 1f;
        guideText.SetActive(false);
        
        keyText.SetActive(true);
        
    }

    private IEnumerator BlinkGuideText()
    {
        while (true)
        {
            guideCanvasGroup.alpha = 1f;
            yield return new WaitForSecondsRealtime(0.9f);

            guideCanvasGroup.alpha = 0.5f;
            yield return new WaitForSecondsRealtime(0.9f);
        }
    }

    private void OnDisable()
    {
        waitingForKey = false;

        if (guideCanvasGroup != null)
        {
            HideGuideText();
        }
    }
}