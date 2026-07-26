using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public float speed = 3f; 
    public Vector2 moveDirection;


    void Start()
    {

    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    

}
