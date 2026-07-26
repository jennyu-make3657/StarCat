using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove: MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical"); */ //기존입력방식(커스텀키 설정을 적용하기 위해 수정)
        float h = 0f;
        float v= 0f; //초기화

        if (SettingManager.Instance != null && SettingManager.Instance.keyManager != null)
        {
            KeyManager keyManager = SettingManager.Instance.keyManager;

            // 좌 / 우 입력 체크
            if (Input.GetKey(keyManager.leftKey) || Input.GetKey(keyManager.leftKey1)) h -= 1f;
            if (Input.GetKey(keyManager.rightKey) || Input.GetKey(keyManager.rightKey1)) h += 1f;

            // 하 / 상 입력 체크
            if (Input.GetKey(keyManager.downKey) || Input.GetKey(keyManager.downKey1)) v -= 1f;
            if (Input.GetKey(keyManager.upKey) || Input.GetKey(keyManager.upKey1)) v += 1f;
        }
        else
        {
            // 혹시라도 SettingManager가 없을 때를 대비한 림보 예외처리 (기본 WASD)
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) h -= 1f;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) h += 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) v -= 1f;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) v += 1f;
        }

        moveInput = new Vector2(h, v).normalized;

        transform.Translate(moveInput * moveSpeed * Time.deltaTime);

        // 화면 밖 제한
        float clampedX = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
        float clampedY = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector2(clampedX, clampedY);
    }
}
