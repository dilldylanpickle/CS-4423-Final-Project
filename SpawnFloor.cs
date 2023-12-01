using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public GameObject floorPrefab;
    public int numberOfFloors;
    public float minimumSpacing = 50f;

    private void Start()
    {
        SpawnFloors();
    }

    void SpawnFloors()
    {
        Camera cam = Camera.main;
        float screenHeight = 2f * cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;
        HashSet<Vector2> spawnedPositions = new HashSet<Vector2>();

        for (int i = 0; i < numberOfFloors; i++)
        {
            Vector2 randomPosition = Vector2.zero;
            bool positionFound = false;

            for (int attempts = 0; attempts < 100; attempts++)
            {
                float randomX = Random.Range(-screenWidth / 2, screenWidth / 2);
                float randomY = Random.Range(-screenHeight / 2, screenHeight / 2);
                randomPosition = new Vector2(randomX, randomY);

                bool tooClose = false;
                foreach (Vector2 pos in spawnedPositions)
                {
                    if (Vector2.Distance(pos, randomPosition) < minimumSpacing)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    positionFound = true;
                    break;
                }
            }

            if (positionFound)
            {
                Instantiate(floorPrefab, randomPosition, Quaternion.identity);
                spawnedPositions.Add(randomPosition);
            }
            else
            {
                Debug.LogWarning("Failed");
            }
        }
    }
}
