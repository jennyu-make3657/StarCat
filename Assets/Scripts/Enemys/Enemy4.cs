using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    
    public float speed = 10f;       
    private Vector2 moveDirection = Vector2.zero;
    private bool isDirectionSet = false;

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
        isDirectionSet = true;
    }

    void Update()
    {
        if (isDirectionSet)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
