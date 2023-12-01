using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 1;
    public float spawnInterval = 2f;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            spawnedEnemies.RemoveAll(enemy => enemy == null);

            if (spawnedEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
        
        if (floors.Length > 0)
        {
            GameObject floor = floors[Random.Range(0, floors.Length)];
            Vector3 floorPosition = floor.transform.position;
            Vector3 floorSize = floor.GetComponent<Renderer>().bounds.size;

            float enemySpawnX = Random.Range(floorPosition.x - floorSize.x / 2, floorPosition.x + floorSize.x / 2);
            float enemySpawnY = floorPosition.y + floorSize.y / 2 + 0.5f;
            
            Vector3 spawnPosition = new Vector3(enemySpawnX, enemySpawnY, floorPosition.z);
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            
            spawnedEnemies.Add(newEnemy);
        }
    }
}
