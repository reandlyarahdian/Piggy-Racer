using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    private QuestionCollection questionCollection;
    private QuizQuestion currentQuestion;
    private GameManager gameManager;

    [SerializeField]
    private float delayBetweenQuestions = 3f;
    private float temp;

    private void Awake()
    {
        questionCollection = FindAnyObjectByType<QuestionCollection>();
        gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(ShowNextQuestionAfterDelay());
    }

    private void PresentQuestion()
    {
        currentQuestion = questionCollection.GetUnaskedQuestion();
        if (currentQuestion != null && temp < 10)
        {
            gameManager.SetupUIForQuestion(currentQuestion);
        }
        else
        {
            gameManager.EndGame();
        }
    }

    public void RestartQuiz()
    {
        questionCollection.ResetQuestionsIfAllHaveBeenAsked();
        temp = 0;
        StartCoroutine(ShowNextQuestionAfterDelay());
    }

    public void SubmitAnswer(int answerNumber)
    {
        bool isCorrect = answerNumber == currentQuestion.CorrectAnswer;
        gameManager.HandleSubmittedAnswer(isCorrect);

        gameManager.UIQ();

        StartCoroutine(ShowNextQuestionAfterDelay());
    }

    private IEnumerator ShowNextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(delayBetweenQuestions);
        temp += 1;
        Debug.Log(temp);
        PresentQuestion();
    }
}