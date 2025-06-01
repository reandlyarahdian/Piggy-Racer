using System;
using System.Linq;
using UnityEngine;

public class QuestionCollection : MonoBehaviour
{
    private QuizQuestion[] allQuestions;

    private void Awake()
    {
        LoadAllQuestions();
        ResetQuestionsIfAllHaveBeenAsked();
    }

    private void LoadAllQuestions()
    {
        allQuestions = Resources.LoadAll<QuizQuestion>("Questions");
    }

    public QuizQuestion GetUnaskedQuestion()
    {
        var rnd = new System.Random();

        var question = allQuestions
            .Where(t => t.Asked == false)
            .OrderBy(x => Guid.NewGuid())
            .ToList()
            .FirstOrDefault();

        if (question != null)
        {
            question.Asked = true;
            return question;
        }
        else
            return null;
    }

    public void ResetQuestionsIfAllHaveBeenAsked()
    {
        if (allQuestions.Any(t => t.Asked == true) == true)
        {
            ResetQuestions();
        }
    }

    private void ResetQuestions()
    {
        foreach (var question in allQuestions)
            question.Asked = false;
    }
}