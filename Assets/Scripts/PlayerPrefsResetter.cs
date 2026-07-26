#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class PlayerPrefsResetter
{
    
    [MenuItem("Tools/Reset PlayerPrefs")]
    public static void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("🧹 [PlayerPrefs] 모든 저장 데이터가 초기화되었습니다!");
    }
}
#endif