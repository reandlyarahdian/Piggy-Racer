using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinKeeper : MonoBehaviour
{
    public static CoinKeeper Instance;

    private int PointTotal;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PointTotal = PlayerPrefs.GetInt("Point");
    }

    public void Point(int n)
    {
        PointTotal += n;
    }

    public void TakePoint()
    {
        PlayerPrefs.SetInt("Point", PointTotal);
    }
}
