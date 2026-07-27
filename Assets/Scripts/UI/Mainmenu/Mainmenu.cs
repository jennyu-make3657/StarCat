using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    public GameObject chapterPanel;
    public TextMeshProUGUI[] chapterTexts;

    public int selectChapter = 1;
    public int unlockChapter = 1; //현재 열린 챕터 저장

    public GameObject optionPanel;

    void Start()
    {
        chapterPanel.SetActive(false);
        optionPanel.SetActive(false);
        AA();
    }

    void Update()
    {
    
    }

    public void ClickStart()
    {
        
    }

    public void ClickContinue()
    {
        chapterPanel.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void ClickOption()
    {
        chapterPanel.SetActive(false);
        optionPanel.SetActive(true);
    }


    public void ClickChapter1() { SelectChapter(1); }
    public void ClickChapter2() { SelectChapter(2); }
    public void ClickChapter3() { SelectChapter(3); }
    public void ClickChapter4() { SelectChapter(4); }
    public void ClickChapter5() { SelectChapter(5); }

    public void SelectChapter(int chapterNumber)
    {
        if(chapterNumber > unlockChapter)
        {
            return;
        }

        selectChapter = chapterNumber;
        AA();
        LoadSelectChapeterScene(chapterNumber);

    }

    void LoadSelectChapeterScene(int chapterNumber)
    {
        // Load the scene corresponding to the selected chapter
        // Example: SceneManager.LoadScene("Chapter" + chapterNumber);
        string sceneName = "Stage" + chapterNumber; //챕터 씬 이름 Stage1, Stage2, Stage3... 
        SceneManager.LoadScene(sceneName);
    }

    void AA()
    {
        for(int i = 0; i < chapterTexts.Length; i++)
        {
            int chapterNumber = i + 1;
            chapterTexts[i].alpha = chapterNumber > unlockChapter ? 0.3f : 1f;
        }
    }
}
