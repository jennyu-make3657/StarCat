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
    private int currentResolutionIndex = 2;

    void Start()
    {
        SetupResolutions();

        Screen.fullScreen = true;

        if (fullscreenToggle != null)
            fullscreenToggle.isOn = true;

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

        currentResolutionIndex = 2;
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
        Screen.fullScreen = isFullscreen;
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
}