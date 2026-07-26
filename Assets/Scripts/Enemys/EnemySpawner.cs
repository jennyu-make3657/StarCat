using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private Vector2[] spawnPositions;

    [Header("Difficulty Settings")]
    public float[] spawnIntervals = new float[8]; 
    public float[] difficultyChangeTimes = new float[8]; 

    private float currentSpawnInterval = 1.8f;
    private int currentDifficultyIndex = 0;
    public float timer = 0f;

    private bool isSpawning = true;


    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;

        if (currentDifficultyIndex < difficultyChangeTimes.Length)
        {
            if (timer >= difficultyChangeTimes[currentDifficultyIndex])
            {
                currentSpawnInterval = spawnIntervals[currentDifficultyIndex];
                currentDifficultyIndex++;
                Debug.Log($"현재 스폰 간격: {currentSpawnInterval}초");
            }
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            CreateSpawn();
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    void CreateSpawn()
    {
        int spawnCase = Random.Range(0, 4);
        switch (spawnCase)
        {
            case 0:
                SpawnEnemy1();
                break;
            case 1:
                SpawnEnemy2();
                break;
            case 2:
                SpawnEnemy3();
                break;
            case 3:
                SpawnEnemy4();
                break;
        }
    }

    void SpawnEnemy1()
    {
        int enemyIndex = 1;
        Vector2 spawnPos = Vector2.zero;
        Vector2 dir = Vector2.zero;
        float interval = 4.5f;

        float randomPosition = Random.Range(0f, 4f);

        for (int i = 0; i < 5; i++)
        {
            spawnPos = new Vector2(-9.0f + interval * i + randomPosition, 7.0f);
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
            dir = Vector2.down;

            if (enemy.GetComponent<Enemy>() != null)
                enemy.GetComponent<Enemy>().moveDirection = dir;
            
            Destroy(enemy, 10f);

        }
    }

    void SpawnEnemy2()
    {
        int enemyIndex = 0;
        Vector2 spawnPos = Vector2.zero;
        Vector2 dir = Vector2.zero;
        float interval = 3.0f;

        float randomPosition = Random.Range(0f, 3f);

        for (int i = 0; i < 4; i++)
        {
            spawnPos = new Vector2(10.0f, 4.5f - interval * i + randomPosition);
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
            dir = Vector2.left;

            if (enemy.GetComponent<Enemy>() != null)
                enemy.GetComponent<Enemy>().moveDirection = dir;

            Destroy(enemy, 10f);
        }
    }

    void SpawnEnemy3()
    {
        int enemyIndex = 2;
        Vector2 spawnPos = Vector2.zero;
        Vector2 playerPos = Vector2.zero;
        Vector2 dir = Vector2.zero;

        float spawnX = 6.0f;
        float spawnY = 6.0f;

        spawnPositions = new Vector2[]
        {
            new Vector2(-spawnX, spawnY),
            new Vector2(spawnX, spawnY),
            new Vector2(-spawnX, -spawnY),
            new Vector2(spawnX, -spawnY)
        };

        for (int i = 0; i < 4; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPositions[i], Quaternion.identity);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerPos = player.transform.position;
                spawnPos = spawnPositions[i];

                dir = (playerPos - spawnPos).normalized;

                Enemy3 enemyMove = enemy.GetComponent<Enemy3>();
                if (enemyMove != null)
                {
                    enemyMove.SetDirection(dir);
                }
            }
            Destroy(enemy, 10f);
        }
    }

    void SpawnEnemy4()
    {
        int enemyIndex = 3;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 spawnPos = new Vector2(Random.Range(-8.0f, 8.0f), 6.0f);

            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
            Vector2 dir = (playerPos - spawnPos).normalized;

            Enemy4 enemyMove = enemy.GetComponent<Enemy4>();
            if (enemyMove != null)
            {
                enemyMove.SetDirection(dir);
            }
            Destroy(enemy, 10f);
        }
    }

    public void GameOver()
    {
        isSpawning = false; 
        StopAllCoroutines();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.speed = 0;
            }

            Enemy2 enemy2Script = enemy.GetComponent<Enemy2>();
            if (enemy2Script != null)
            {
                enemy2Script.speed = 0;
            }

            Enemy3 enemy3Script = enemy.GetComponent<Enemy3>();
            if (enemy3Script != null)
            {
                enemy3Script.speed = 0;
            }

            Enemy4 enemy4Script = enemy.GetComponent<Enemy4>();
            if (enemy4Script != null)
            {
                enemy4Script.speed = 0;
            }
        }
    }
}