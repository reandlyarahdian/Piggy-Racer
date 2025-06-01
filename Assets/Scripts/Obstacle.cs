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
    public int spawnObj = 15;

    private float minX, maxX;

    void Start()
    {
        CalculateColliderBounds();

        for (int i = 0; i < spawnObj; i++)
        {
            SpawnAllObstacles();
        }
    }

    void SpawnAllObstacles()
    {
        if (obstaclePrefabs.Length == 0) return;

        foreach (GameObject prefab in obstaclePrefabs)
        {
            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPos = new Vector3(randomX, spawnY, 0f);
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
