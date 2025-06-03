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
        float availableWidth = maxX - minX;
        int maxPossibleSpawns = Mathf.FloorToInt(availableWidth / spacing);

        int spawnCount = Mathf.Min(spawnObj, maxPossibleSpawns + Mathf.FloorToInt(spacing / 2));
        
        Debug.Log($"{minX} {maxX} {maxPossibleSpawns} {spawnCount}");

        float startX = minX + 20;

        for (int i = 0; i < spawnCount; i++)
        {
            float space = Random.Range(spacing / 2, spacing);

            float xPos = startX + (i * space);

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

        minX = combinedBounds.min.x;
        maxX = combinedBounds.max.x;
    }
}
