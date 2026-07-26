using UnityEngine;

public class Berry : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2.5f;          // 떨어지는 속도
    public float gameOverY = -4.5f;      // 게임오버 기준 Y 좌표

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        // 위에서 아래로 이동
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Y 좌표가 -4.5 이하로 내려가면 플레이어가 놓친 것으로 간주 -> 게임오버!
        if (transform.position.y <= gameOverY)
        {
            TriggerGameOver();
        }
    }

    // 플레이어와 충돌(먹었을 때) 처리
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            // 먹었으므로 오브젝트 파괴
            Destroy(gameObject);
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        Debug.Log("필수 오브젝트를 놓쳤습니다! Game Over!");

        // 1. 플레이어 죽음/게임오버 처리 호출
        PlayerMove player = FindAnyObjectByType<PlayerMove>();
        if (player != null)
        {
            // 기존 플레이어의 Die() 함수가 있다면 호출합니다.
            player.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
        }

        // 2. 씬 내의 모든 EnemySpawner 정지
        EnemySpawner[] spawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.GameOver();
        }

        // 3. 씬 내의 모든 SpecialWaveSpawner 정지
        BerrySpawner[] waveSpawners = FindObjectsByType<BerrySpawner>(FindObjectsSortMode.None);
        foreach (BerrySpawner waveSpawner in waveSpawners)
        {
            waveSpawner.GameOver();
        }

        // 4. 떨어지고 있는 다른 WaveMove 오브젝트들도 멈춤
        WaveMove[] waves = FindObjectsByType<WaveMove>(FindObjectsSortMode.None);
        foreach (WaveMove wave in waves)
        {
            wave.GameOver();
        }
    }

    // 플레이어가 죽었을 때 움직임을 멈추는 함수
    public void GameOver()
    {
        BroadcastMessage("GameOver");
    }
}
