using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private QuizController quizController;
    private PlayerController playerController;
    private UIManager uiManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiManager = GetComponent<UIManager>();
        quizController = GetComponent<QuizController>();
        playerController = FindAnyObjectByType<PlayerController>();
        Time.timeScale = 1f;
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
        uiManager.UIQ(true);
        Time.timeScale = 0f;
    }

    public void UIQ()
    {
        uiManager.UIQ(false);
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        uiManager.EndQ(true);
        Time.timeScale = 0f;
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
        uiManager.ToggleAnswerButtons(false);
    }
}