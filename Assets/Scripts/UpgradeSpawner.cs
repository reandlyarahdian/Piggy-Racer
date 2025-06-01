using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    [Header("Upgrade Settings")]
    public GameObject upgradePrefabs;
    public float spawnInterval = 7.5f;
    public float spawnY = 3.4f;

    private float minX, maxX;

    void Start()
    {
        StartCoroutine(SpawnUpgradeRoutine());
    }

    IEnumerator SpawnUpgradeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnUpgrade();
        }
    }

    void SpawnUpgrade()
    {
        float X = GameObject.Find("Player").transform.position.x + 5;
        Vector3 spawnPos = new Vector3(X, spawnY, 0f);
        Instantiate(upgradePrefabs, spawnPos, Quaternion.identity);
    }
}
