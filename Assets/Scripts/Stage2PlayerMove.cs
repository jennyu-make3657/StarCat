using UnityEngine;
using UnityEngine.EventSystems;

public class Stage2PlayerMove : MonoBehaviour
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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(h, v).normalized;

        transform.Translate(moveInput * moveSpeed * Time.deltaTime);

        // 화면 밖 제한
        float clampedX = Mathf.Clamp(transform.position.x, -3.35f, 3.35f);
        float clampedY = Mathf.Clamp(transform.position.y, -2.39f, 4.5f);
        transform.position = new Vector2(clampedX, clampedY);
    }
}
