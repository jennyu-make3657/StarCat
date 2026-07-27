using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }
    [Header("Option Script")]
    public Option option; //전체화면,해상도 담당 스크립트
    public KeyManager keyManager; //키 설정 담당
    public BGMVolume bgmVolume;//배경음 설정 담당

    //PlayerPrefs에 저장한 키 이름들을 상수로 정의,사용한 KEY이름을 쉽게 확인 가능
    //--------------------
    //그래픽 설정 Key
    //--------------------

    private const string FULLSCREEN_KEY = "FullScreen";
    private const string RESOLUTION_KEY = "Resolution";


    //--------------------
    //오디오 설정 Key
    //--------------------

    private const string BGM_VOLUME_KEY = "BGMVolume";


    //--------------------
    //조작 설정 Key
    //--------------------

    private const string UP_KEY = "UP";
    private const string DOWN_KEY = "DOWN";
    private const string LEFT_KEY = "LEFT";
    private const string RIGHT_KEY = "RIGHT";
    private const string SPECIAL_KEY = "SPECIALACTION";

    private const string CONFIRM_KEY = "CONFIRM";

    private const string SKIP_KEY = "SKIPPAUSE";



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadSetting();
    }




    /* public SaveSetting() { }*/
    
    public void LoadSetting() 
    {
        LoadGraphics();
        LoadAudio();
        LoadControl();


    }

    private void LoadGraphics() 
    {
        if (option == null)
        {
            Debug.LogError("Option이 연결되지 않았습니다.");
            return;
        }
        bool isFullscreen = PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;
        int resolutionIndex = PlayerPrefs.GetInt(RESOLUTION_KEY, 2); // 기본 해상도 인덱스는 2로 설정
        option.SetFullscreen(isFullscreen);
        option.SetResolutionIndex(resolutionIndex);
    }
    private void LoadAudio() 
    {
        if (bgmVolume == null)
        {
            Debug.LogError("BGMVolume이 연결되지 않았습니다.");
            return;
        }
        int volume= PlayerPrefs.GetInt(BGM_VOLUME_KEY, 6); // 기본 볼륨은 6
        bgmVolume.SetVolume(volume);
    }
    private void LoadControl() 
    {
        if (keyManager == null)
        {
            Debug.LogError("KeyManager가 연결되지 않았습니다.");
            return;
        }
        keyManager.upKey = ParseKey(UP_KEY, KeyCode.W);
        keyManager.downKey = ParseKey(DOWN_KEY, KeyCode.S);
        keyManager.leftKey = ParseKey(LEFT_KEY, KeyCode.A);
        keyManager.rightKey = ParseKey(RIGHT_KEY, KeyCode.D);
        keyManager.specialActionKey = ParseKey(SPECIAL_KEY, KeyCode.Space);
        keyManager.confirmKey = ParseKey(CONFIRM_KEY, KeyCode.Return);
        keyManager.skipPauseKey = ParseKey(SKIP_KEY, KeyCode.Escape);
    }
    private void ResetKeySetting()
    {
        PlayerPrefs.DeleteKey(UP_KEY);
        PlayerPrefs.DeleteKey(DOWN_KEY);
        PlayerPrefs.DeleteKey(LEFT_KEY);
        PlayerPrefs.DeleteKey(RIGHT_KEY);
        PlayerPrefs.DeleteKey(SPECIAL_KEY);
        PlayerPrefs.DeleteKey(CONFIRM_KEY);
        PlayerPrefs.DeleteKey(SKIP_KEY);
        PlayerPrefs.Save();
        LoadControl();
        KeyUI[] keyUIs = FindObjectsByType<KeyUI>(FindObjectsSortMode.None);
        foreach (KeyUI ui in keyUIs)
        {
            // KeyUI의 Start 코루틴 처럼 UI 텍스트 다시 불러오기
            ui.StartCoroutine("InitKeyUI");
        }
    }
    public void RequestResetKeys()
    {
        ResetKeySetting();
    }
    private KeyCode ParseKey(string keyName, KeyCode defaultKey)
    {
        string keyString = PlayerPrefs.GetString(keyName, defaultKey.ToString());
        if (System.Enum.TryParse(keyString, out KeyCode parsedKey))
        {
            return parsedKey;
        }
        return defaultKey;
    }

    /*string up =
PlayerPrefs.GetString("UP","W");

keyManager.upKey =
(KeyCode)System.Enum.Parse(
typeof(KeyCode),up);
*/
}


//BGMVolume스크립트에서  PlayerPrefs.SetInt("BGMVolume", currentVolume); 키 이름을 BGMVolume으로 설정,value는 currentVolume 변수로 설정하여 PlayerPrefs에 저장하고 있음.
//"SettingManager가 값을 가져와서 관리한다." 보다는 "SettingManager가 여러 설정값들을 총괄해서 불러오고 적용한다."
//"게임에 필요한 설정값들을 전부 가져와서 각 담당자에게 전달
//public SaveSetting()
/*SaveSetting()을 구현하지 않은 이유

현재 구조에서는 Option,
KeyUI, BGMVolume 등의
각 스크립트가 자신의 설정값을
PlayerPrefs에 직접 저장하고 있음.

따라서 SettingManager에서
중복으로 저장할 필요가 없어
Load와 최종 적용만 담당하도록 설계함.
*/
//LoadSetting() : 게임 설정 불러오기 PlayerPrefs에서 불러오기 

//currentResolutionIndex는
//Option.cs에서만 접근 가능(private)

//따라서 SettingManager에서
//직접 접근할 수 없음.

//public 메서드를 만들어
//PlayerPrefs에서 불러온 값을
//Option에게 전달하여 적용하도록 함.