using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float spawnInterval;

    [Header("Enemy Prefabs")]
    public GameObject zombieType01Prefab;

    [Header("Spawn Area")]
    public Transform[] spawnArea;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        int idx = Random.Range(0, spawnArea.Length);
        Vector3 randomPosition = spawnArea[idx].position;
        GameObject enemy = ObjectPoolManager.Instance.GetEnemyObject(EnemyType.ZombieType01);
        enemy.transform.position = randomPosition;
    }
}
