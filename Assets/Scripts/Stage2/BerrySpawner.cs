using UnityEngine;
using System.Collections;

public class BerrySpawner : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject BerryPrefab; // 반드시 먹어야 하는 오브젝트 프리팹

    [Header("Spawn Timing")]
    public float firstSpawnTime = 20f;   // 첫 스폰 시점 (플레이 20초 후)
    public float spawnInterval = 30f;   // 스폰 간격 (30초)
    public int maxSpawnCount = 5;       // 총 떨어질 개수 (5개)

    [Header("Spawn Position")]
    public float spawnY = 6.0f;          // 화면 위쪽 스폰 Y 좌표
    public float minX = -3.0f;           // X 최소 범위
    public float maxX = 3.0f;            // X 최대 범위

    private int currentSpawnCount = 0;

    void Start()
    {
        // 지정된 시간(20초) 후부터 30초 간격으로 스폰 루틴 시작
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // 1. 첫 스폰 시간(20초)까지 대기
        yield return new WaitForSeconds(firstSpawnTime);

        // 2. 총 5개가 떨어질 때까지 반복
        while (currentSpawnCount < maxSpawnCount)
        {
            SpawnSpecialWave();
            currentSpawnCount++;

            // 마지막 오브젝트를 낳은 후에는 대기하지 않고 루프 종료
            if (currentSpawnCount < maxSpawnCount)
            {
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    void SpawnSpecialWave()
    {
        // -3 ~ 3 사이의 랜덤 X 좌표 생성
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        Instantiate(BerryPrefab, spawnPos, Quaternion.identity);
    }

    // 게임 오버 시 남아있는 생성 루틴 정지용
    public void GameOver()
    {
        StopAllCoroutines();
    }
}