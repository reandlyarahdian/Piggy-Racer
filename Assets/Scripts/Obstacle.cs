using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstaclePrefabs;

    [Header("Spawn Height")]
    public float spawnY = 3.4f;

    [Header("Spawn Object")]
    public int spawnObj = 7;

    [Header("Spacing")]
    public float spacing = 20f;

    private float minX, maxX;

    void Start()
    {
        CalculateColliderBounds();

        SpawnAllObstacles();
    }
    void SpawnAllObstacles()
    {
        if (obstaclePrefabs.Length == 0) return;

        float space = Random.Range(10, spacing);
        List<float> usedPositions = new List<float>();

        float availableWidth = maxX - minX;
        int maxPossibleSpawns = Mathf.FloorToInt(availableWidth / spacing);

        int spawnCount = Mathf.Min(spawnObj, maxPossibleSpawns);
        float startX = minX;

        for (int i = 0; i < spawnCount; i++)
        {
            float xPos = startX + (i * space);

            // Choose a random prefab
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            Vector3 spawnPos = new Vector3(xPos, spawnY, 0f);
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }


    void CalculateColliderBounds()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        if (colliders.Length == 0)
        {
            Debug.LogWarning("No 2D colliders found on this GameObject.");
            minX = maxX = transform.position.x;
            return;
        }

        Bounds combinedBounds = colliders[0].bounds;
        for (int i = 1; i < colliders.Length; i++)
        {
            combinedBounds.Encapsulate(colliders[i].bounds);
        }

        minX = combinedBounds.min.x + 20;
        maxX = combinedBounds.max.x - 10;
    }
}
