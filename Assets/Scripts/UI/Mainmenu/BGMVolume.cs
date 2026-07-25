using UnityEngine;
using UnityEngine.UI;

public class BGMVolume : MonoBehaviour
{
    public Image[] volumenemo;
    public Color fillColor = Color.black;
    public Color emptyColor = Color.white;
    public int currentVolume = 6;

    public GameObject volumepanel;
    public Toggle BGMtoggle;

    private void Start()
    {
        currentVolume = PlayerPrefs.GetInt("BGMVolume", currentVolume);
        VolumeNemo();
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