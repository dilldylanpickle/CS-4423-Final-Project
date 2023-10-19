using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform floorTransform;
    public float spawnInterval = 2f;
    private float floorWidth;

    private void Start()
    {
        floorWidth = floorTransform.localScale.x;

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float randomX = floorTransform.position.x + Random.Range(-floorWidth / 2, floorWidth / 2);
        float spawnY = floorTransform.position.y + 1;

        Vector2 spawnPosition = new Vector2(randomX, spawnY);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
