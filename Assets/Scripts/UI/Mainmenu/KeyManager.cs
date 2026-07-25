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
    public KeyCode skipPauseKey= KeyCode.Escape;
    private void Awake()
    {
        Instance=this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
