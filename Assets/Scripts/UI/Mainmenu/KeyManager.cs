using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    public KeyCode upKey1 = KeyCode.UpArrow;
    public KeyCode upKey = KeyCode.W;

    public KeyCode downKey1 = KeyCode.DownArrow;
    public KeyCode downKey = KeyCode.S;

    public KeyCode leftKey1 = KeyCode.LeftArrow;
    public KeyCode leftKey = KeyCode.A;

    public KeyCode rightKey1 = KeyCode.RightArrow;
    public KeyCode rightKey = KeyCode.D;

    public KeyCode specialActionKey = KeyCode.Space;
    public KeyCode confirmKey = KeyCode.Return;
    public KeyCode skipPauseKey = KeyCode.Escape;
    private void Awake()
    {
        // 싱글톤 패턴 설정
        if (Instance == null)
        {
            Instance = this;
            // 씬이 변경되어도 키 매니저 파괴 방지 (필요 시 주석 해제)
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }


    }
}

    