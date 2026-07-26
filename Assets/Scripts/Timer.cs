using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject clearPanel;
    public float currentTime = 0f;

    private bool isCleared = false;

    void Start()
    {
        clearPanel.SetActive(false);
    }

    void Update()
    {
        if (isCleared) return;

        currentTime += Time.deltaTime;  

        
    }

    void GameClear()
    {
        if (currentTime >= 180f)
        {
            isCleared = true;
            clearPanel.SetActive(true);

        }
    }
}
