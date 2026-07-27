using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionMenuPanel;

    public GameObject menuPanel;
    public GameObject graphicPanel;
    public GameObject controlPanel;
    public GameObject audioPanel;

    public Toggle fullscreenToggle;

    public TextMeshProUGUI resolutionText;

    private Resolution[] resolutions;
    private int currentResolutionIndex = 2; //1920*1080 기본값 (2)

    private void Awake()
    {
        SetupResolutions();
    }

    void Start()
    {
        // Option 스스로 화면 상태나 토글을 건드리는 코드를 삭제
        // 설정 적용과 UI 세팅은 SettingManager가 LoadSetting()할 때 완벽하게 처리
        UpdateResolutionText();
    }

    void SetupResolutions()
    {
        if (resolutions != null)
            return;

        resolutions = new Resolution[]
        {
            new Resolution { width = 1280, height = 720 },
            new Resolution { width = 1600, height = 900 },
            new Resolution { width = 1920, height = 1080 }
        };

        //currentResolutionIndex = 2;
        
    }

    public void OpenOption()
    {
        mainMenuPanel.SetActive(false);
        optionMenuPanel.SetActive(true);

        menuPanel.SetActive(true);
        graphicPanel.SetActive(false);
        controlPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void CloseOption()
    {
        optionMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        menuPanel.SetActive(true);
        graphicPanel.SetActive(false);
        controlPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void OpenGraphic()
    {
        menuPanel.SetActive(false);
        graphicPanel.SetActive(true);
        controlPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void BackFromGraphic()
    {
        graphicPanel.SetActive(false);
        menuPanel.SetActive(true);
        controlPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void OpenControl()
    {
        menuPanel.SetActive(false);
        controlPanel.SetActive(true);
        graphicPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void BackFromControl()
    {
        menuPanel.SetActive(true);
        controlPanel.SetActive(false);
        graphicPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void ResetControlKeys()
    {
        if (SettingManager.Instance != null)
        {
            SettingManager.Instance.RequestResetKeys();
        }
    }

    public void OpenAudio()
    {
        menuPanel.SetActive(false);
        audioPanel.SetActive(true);
        controlPanel.SetActive(false);
        graphicPanel.SetActive(false);
    }

    public void BackFromAudio()
    {
        menuPanel.SetActive(true);
        audioPanel.SetActive(false);
        controlPanel.SetActive(false);
        graphicPanel.SetActive(false);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        //PlayerPrefs에 변경된 값을 저장하도록 수정,토글 스위치 상태 업데이트(isOn변수 값을 직접 변경하는 대신 토글만 업데이트)
        Screen.fullScreen = isFullscreen;
        if (fullscreenToggle != null && fullscreenToggle.isOn != isFullscreen)
        {
            fullscreenToggle.SetIsOnWithoutNotify(isFullscreen);
        }
        PlayerPrefs.SetInt("FullScreen",isFullscreen ? 1 : 0);

        PlayerPrefs.Save();

    }

    public void ResolutionLeft()
    {
        SetupResolutions();

        currentResolutionIndex--;

        if (currentResolutionIndex < 0)
            currentResolutionIndex = resolutions.Length - 1;

        ApplyResolution();
    }

    public void ResolutionRight()
    {
        SetupResolutions();

        currentResolutionIndex++;

        if (currentResolutionIndex >= resolutions.Length)
            currentResolutionIndex = 0;

        ApplyResolution();
    }

    void ApplyResolution()
    {
        SetupResolutions();

        Resolution res = resolutions[currentResolutionIndex];

        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

        UpdateResolutionText();
        PlayerPrefs.SetInt("Resolution", currentResolutionIndex); //현재 변경된 해상도 인덱스를 PlayerPrefs에 저장
        PlayerPrefs.Save();
    }

    void UpdateResolutionText()
    {
        if (resolutionText == null)
        {
            Debug.LogError("ResolutionValue 연결 안 됨");
            return;
        }

        SetupResolutions();

        resolutionText.text =
            resolutions[currentResolutionIndex].width + " x " +
            resolutions[currentResolutionIndex].height;
    }
    // SetResolutionIndex 메서드를 추가하여 SettingManager가 PlayerPrefs에서 불러온 해상도 값을 Option.cs에 적용
    public void SetResolutionIndex(int index)
    {
        SetupResolutions();
        currentResolutionIndex = Mathf.Clamp(index, 0, resolutions.Length - 1);
        
        ApplyResolution();
    }
}