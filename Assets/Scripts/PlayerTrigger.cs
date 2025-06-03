using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private PlayerController controller;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        controller = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            controller.OnHit();
            StartCoroutine(waitObject(collision.gameObject));
        }

        if (collision.CompareTag("Upgrade"))
        {
            GameManager.instance.StartGame();
            StartCoroutine(waitObject(collision.gameObject));
        }

        if (collision.CompareTag("End"))
        {
            GameManager.instance.EndGame();
        }

        if (collision.CompareTag("Coin"))
        {
            controller.Point(1);
            uiManager.PointText(controller.Points.ToString());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator waitObject(GameObject go)
    {
        yield return new WaitForSeconds(1f);
        Destroy(go);
    }
}
