using UnityEngine;
using UnityEngine.UI;

public class BGMVolume : MonoBehaviour
{
    public Image[] volumenemo;
    public Color fillColor = Color.black;
    public Color emptyColor = Color.white;
    [SerializeField]
    private int currentVolume = 6; //private로 변경하여 외부에서 직접 접근 차단.
                                   //무조건 SetVolume()을 통해서만 변경되도록 유도.

    public GameObject volumepanel;
    public Toggle BGMtoggle;

    private void Start()
    {
        /*currentVolume = PlayerPrefs.GetInt("BGMVolume", currentVolume);
        VolumeNemo();
        */
        // SettingManager가 Start()의 LoadSetting()에서
        // PlayerPrefs의 값을 불러와 SetVolume()을 통해
        // 최종 적용하므로 여기서 중복으로 불러오지 않음.
        //
        // 설정값의 Load와 적용은 SettingManager가 담당
    }

    public void SetVolume(int volume)
    {
        currentVolume = Mathf.Clamp(volume, 0, 10);

        PlayerPrefs.SetInt("BGMVolume", currentVolume);
        PlayerPrefs.Save();

        VolumeNemo();

        //나중에 BGM 파일이 생기면 이 값을 연결하면 됨
        float realVolume = currentVolume / 10f;
    }

    private void VolumeNemo()
    {
        for (int i = 0; i < volumenemo.Length; i++)
        {
            if (i < currentVolume)
            {
                volumenemo[i].color = fillColor;
            }
            else
            {
                volumenemo[i].color = emptyColor;
            }
        }
    }




    public void ToggleVolume()
    {
        volumepanel.SetActive(BGMtoggle.isOn);
    }
}