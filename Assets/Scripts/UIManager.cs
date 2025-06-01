using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private GameObject Setting;
    [SerializeField]
    private GameObject Shop;
    [SerializeField]
    private GameObject Play;
    [SerializeField]
    private GameObject Join;
    [SerializeField]
    private GameObject Create;
    [SerializeField]
    private GameObject Coin;
    [SerializeField]
    private TextMeshProUGUI points;

    public void MenuToSetting(bool isTrue)
    {
        Menu.SetActive(isTrue);
        Setting.SetActive(!isTrue);
    }

    public void MenuToShop(bool isTrue)
    {
        Menu.SetActive(isTrue);
        Shop.SetActive(!isTrue);
    }
    public void MenuToPlay(bool isTrue)
    {
        Menu.SetActive(isTrue);
        Play.SetActive(!isTrue);
    }

    public void PlayToJoin(bool isTrue)
    {
        Play.SetActive(isTrue);
        Join.SetActive(!isTrue);
    }

    public void PlayToCreate(bool isTrue)
    {
        Play.SetActive(isTrue);
        Create.SetActive(!isTrue);
    }

    public void GameToSetting(bool isTrue)
    {
        Setting.SetActive(isTrue);
        int i = isTrue ? 0 : 1;
        Time.timeScale = i;
    }

    public void GameToCoin(bool isTrue)
    {
        Coin.SetActive(isTrue);
        int i = isTrue ? 0 : 1;
        Time.timeScale = i;
    }

    public void PointText(string n)
    {
        points.text = n;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LevelScene(string level)
    {
       SceneManager.LoadScene(level);
    }
}
