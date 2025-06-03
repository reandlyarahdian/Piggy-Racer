using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
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
    private GameObject Skin;
    [SerializeField]
    private GameObject Item;
    [SerializeField]
    private GameObject Coin;
    [SerializeField]
    private GameObject QuizUI;
    [SerializeField]
    private GameObject EndUI;
    [SerializeField]
    private TextMeshProUGUI points;
    [SerializeField]
    private TextMeshProUGUI points2;
    [SerializeField]
    private TextMeshProUGUI shopPoint;

    [Space]
    [SerializeField]
    private GameObject questionText;
    [SerializeField]
    private Button[] answerButtons;

    [Space]
    [SerializeField]
    private Button claimButtons;
    [SerializeField]
    private Button noClaimButtons;

    private QuizController quizController;

    private void Start()
    {
        quizController = GetComponent<QuizController>();

        claimButtons.onClick.AddListener(() => {
            CoinKeeper.Instance.TakePoint();
            LevelScene("Menu");
        });
        noClaimButtons.onClick.AddListener(() => {
            LevelScene("Menu");
        });
    }

    private void Update()
    {
        shopPoint.text = PlayerPrefs.GetInt("Point").ToString();
    }

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

    public void PlayToShop(bool isTrue)
    {
        Play.SetActive(isTrue);
        Shop.SetActive(!isTrue);
    }

    public void UIQ(bool isTrue)
    {
        QuizUI.SetActive(isTrue);
    }

    public void EndQ(bool isTrue)
    {
        EndUI.SetActive(isTrue);
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

    public void SkinShow(bool isTrue)
    {
        Skin.SetActive(isTrue);
        Item.SetActive(!isTrue);
        Coin.SetActive(!isTrue);
    }

    public void CoinShow(bool isTrue)
    {
        Coin.SetActive(isTrue);
        Item.SetActive(!isTrue);
        Skin.SetActive(!isTrue);
    }

    public void ItemShow(bool isTrue)
    {
        Item.SetActive(isTrue);
        Skin.SetActive(!isTrue);
        Coin.SetActive(!isTrue);
    }

    public void PointText(string n)
    {
        points.text = n;
        points2.text = n;
    }

    public void SetupUIForQuestion(QuizQuestion question)
    {
        questionText.GetComponentInChildren<TextMeshProUGUI>().text = question.Question;

        for (int i = 0; i < question.Answers.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.Answers[i];
            answerButtons[i].GetComponent<Image>().color = Color.white;
            var a = answerButtons[i].transform.GetChild(0);
            a.gameObject.SetActive(true);
            answerButtons[i].gameObject.SetActive(true);
        }
    }

    public void ToggleAnswerButtons(bool value)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(value);
        }
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
