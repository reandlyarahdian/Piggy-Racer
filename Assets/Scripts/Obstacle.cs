using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 2f;

    [Header("Spawn Offset")]
    public float xOffsetFromCamera = 2f; // How far off-screen to the right obstacles spawn

    private float timer;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefabs.Length == 0) return;

        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Get the right edge of the camera in world units
        float camRightEdge = mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        // Final spawn position
        Vector3 spawnPos = new Vector3(camRightEdge + xOffsetFromCamera, -3.4f, 0f);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
