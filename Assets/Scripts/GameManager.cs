using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Space]
    [SerializeField]
    private GameObject questionText;
    [SerializeField]
    private Button[] answerButtons;

    [Space]
    [SerializeField]
    private GameObject QuizUI;
    [SerializeField]
    private GameObject EndUI;

    private QuizController quizController;
    private PlayerController playerController;
    private UIManager uiManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        quizController = GetComponent<QuizController>();
        playerController = FindAnyObjectByType<PlayerController>();
        uiManager = FindAnyObjectByType<UIManager>();
        Time.timeScale = 1f;
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

    public void HandleSubmittedAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            ShowCorrectAnswerPopup();
        }
        else
        {
            ShowWrongAnswerPopup();
        }

        StartCoroutine(ShowNextQuestionAfterDelay());
    }

    public void StartGame()
    {
        QuizUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UIQ()
    {
        QuizUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        EndUI.SetActive(true);
        Time.timeScale = 0f;
        uiManager.PointText(playerController.Points.ToString());
    }

    private void ToggleAnswerButtons(bool value)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(value);
        }
    }

    private void ShowCorrectAnswerPopup()
    {
        StartCoroutine(SpeedWait());
    }

    private void ShowWrongAnswerPopup()
    {
        playerController.OnHit();
    }

    private IEnumerator SpeedWait()
    {
        playerController.RSpeed(20);
        yield return new WaitForSeconds(2);
        playerController.RSpeed(10);
    }

    private IEnumerator ShowNextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        ToggleAnswerButtons(false);
    }
}