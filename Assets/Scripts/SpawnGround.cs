using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGround : MonoBehaviour
{
    public GameObject ground;

    public GameObject parentG;

    private string End = "End";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnNextGround();
        }   
    }

    void SpawnNextGround()
    {
        float x = parentG.transform.position.x + 1000;
        Vector3 spawnPos = new Vector3(x, 0, 0f);
        GameObject g = Instantiate(ground, spawnPos, Quaternion.identity);
        int a = UnityEngine.Random.Range(0, 10);
        if (a == 0)
        {
            g.transform.GetChild(0).tag = End;
        }
        Debug.Log(a);
        Destroy(parentG);
    }
}
