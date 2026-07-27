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
    private KeyManager keyManager;
    private TextMeshProUGUI kkey;
    private void Start()
    {

        guideCanvasGroup = guideText.GetComponent<CanvasGroup>();
        kkey = keyText.GetComponentInChildren<TextMeshProUGUI>(true);

        guideCanvasGroup.alpha = 1f;


        guideText.SetActive(false);

        keyText.SetActive(true);
        StartCoroutine(InitKeyUI());

    }
    private IEnumerator InitKeyUI()
    {
        yield return null;
        if (SettingManager.Instance != null)
        {
            keyManager = SettingManager.Instance.keyManager;
        }
        UpdateUITextFromKeyManager();
    }

    private void UpdateUITextFromKeyManager()
    {
        if (SettingManager.Instance == null || SettingManager.Instance.keyManager == null)
        {
            Debug.LogError("[KeyUI] SettingManager 또는 KeyManager를 찾을 수 없습니다!");
            return;
        }
        KeyManager keyManager = SettingManager.Instance.keyManager;
        KeyCode currentKey = KeyCode.None;
        switch (keyType)
        {
            case KeyType.Up: currentKey = keyManager.upKey; break;
            case KeyType.Down: currentKey = keyManager.downKey; break;
            case KeyType.Left: currentKey = keyManager.leftKey; break;
            case KeyType.Right: currentKey = keyManager.rightKey; break;
            case KeyType.SpecialAction: currentKey = keyManager.specialActionKey; break;
            case KeyType.Confirm: currentKey = keyManager.confirmKey; break;
            case KeyType.SkipPause: currentKey = keyManager.skipPauseKey; break;
        }

        UpdateKeyTextDisplay(currentKey);
    }

    

    private void Update()
    {
        if(!waitingForKey)
            return;

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                // 마우스 클릭(Mouse0, Mouse1 등) 및 None 입력 제외
                if (key >= KeyCode.Mouse0 && key <= KeyCode.Mouse6)
                    continue;

                SaveNewKey(key);
                break;
            }
        }
    }

    private void SaveNewKey(KeyCode newKey)
    {
        if (keyManager == null)
        {
            Debug.LogError("[KeyUI] KeyManager를 찾을 수 없습니다!");
            waitingForKey = false;
            HideGuideText();
            return;
        }

       


        switch (keyType)
        {

            case KeyType.Up:
                keyManager.upKey = newKey;
                PlayerPrefs.SetString("UP", newKey.ToString());
                break;

            case KeyType.Down:
                keyManager.downKey = newKey;
                PlayerPrefs.SetString("DOWN", newKey.ToString());
                break;

            case KeyType.Left:
                keyManager.leftKey = newKey;
                PlayerPrefs.SetString("LEFT", newKey.ToString());
                break;

            case KeyType.Right:
                keyManager.rightKey = newKey;
                PlayerPrefs.SetString("RIGHT", newKey.ToString());
                break;

            case KeyType.SpecialAction:
                keyManager.specialActionKey = newKey;
                PlayerPrefs.SetString("SPECIALACTION", newKey.ToString());
                break;

            case KeyType.Confirm:
                keyManager.confirmKey = newKey;
                PlayerPrefs.SetString("CONFIRM", newKey.ToString());
                break;

            case KeyType.SkipPause:
                keyManager.skipPauseKey = newKey;
                PlayerPrefs.SetString("SKIPPAUSE", newKey.ToString());
                break;


        }
        PlayerPrefs.Save();
        UpdateKeyTextDisplay(newKey);
        waitingForKey = false;
        HideGuideText();
    }

    private void UpdateKeyTextDisplay(KeyCode key)
    {
        if (kkey == null) return;

        switch (key)
        {
            case KeyCode.UpArrow: kkey.text = "↑"; break; 
            case KeyCode.DownArrow: kkey.text = "↓"; break; 
            case KeyCode.LeftArrow: kkey.text = "←"; break; 
            case KeyCode.RightArrow: kkey.text = "→"; break; 
            case KeyCode.Escape: kkey.text = "ESC"; break;
            case KeyCode.Return: kkey.text = "ENTER"; break;
            case KeyCode.Space: kkey.text = "SPACE"; break;
            default: kkey.text = key.ToString(); break;
        }
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
    public void RefreshKeyUI()
    {
        if (SettingManager.Instance != null)
        {
            keyManager = SettingManager.Instance.keyManager;
        }
        UpdateUITextFromKeyManager();
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