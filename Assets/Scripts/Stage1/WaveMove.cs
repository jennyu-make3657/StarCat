using System;
using UnityEngine;

public class WaveMove: MonoBehaviour
{
    private Vector3 startPosition; 
    public Vector3 targetPosition;

    public float speed = 10f;

    private float timer = 0f;

    private Boolean stop = false;

    void Start()
    {
        startPosition = transform.position;

    }

    void Update()
    {
        if (!stop)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Lerp(startPosition, targetPosition, timer / speed);

            if (timer >= speed)
            {
                timer = 0f;
                transform.position = startPosition;
            }
        }
        
    }


    public void GameOver()
    {
        stop = true;
    }
}