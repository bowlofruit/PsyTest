using System.Collections.Generic;
using UnityEngine;

public class TestPresenter
{
	private readonly ITestView view;
	private readonly TestContainer testContainer;
	private readonly UserTest userTest;
	private int currentQuestionIndex;

	public TestPresenter(ITestView view, TestContainer testContainer, UserTest userTest)
	{
		this.view = view;
		this.testContainer = testContainer;
		this.userTest = userTest;
		currentQuestionIndex = 0;

		view.SetPresenter(this);
	}

	public void StartTest()
	{
		Debug.Log("Start Test");
		LoadNextQuestion();
	}

	private void LoadNextQuestion()
	{
		if (currentQuestionIndex < testContainer.Questions.Count)
		{
			view.DisplayQuestion(testContainer.Questions[currentQuestionIndex]);
		}
		else
		{
			ShowResults();
		}
	}

	public void SubmitAnswer(string selectedOptionText)
	{
		TestQuestion currentQuestion = testContainer.Questions[currentQuestionIndex];

		int selectedScore = GetScoreForSelectedOption(currentQuestion, selectedOptionText);
		userTest.CurrentScore += selectedScore;

		currentQuestionIndex++;
		LoadNextQuestion();
	}

	private int GetScoreForSelectedOption(TestQuestion question, string selectedOptionText)
	{
		foreach (var option in question.Options)
		{
			if (option.Text == selectedOptionText)
			{
				return option.Score;
			}
		}
		return 0;
	}

	private void ShowResults()
	{
		TestResult result = CalculateTestResult();
		view.ShowResult(result);
	}

	private TestResult CalculateTestResult()
	{
		TestResult result = new TestResult
		{
			TestId = userTest.TestId,
			TotalScore = userTest.CurrentScore,
			ResultLabel = GetResultLabel(userTest.CurrentScore)
		};

		return result;
	}

	private string GetResultLabel(int score)
	{
		foreach (var scoring in testContainer.Scoring)
		{
			if (score >= scoring.MinScore && score <= scoring.MaxScore)
			{
				return scoring.Label;
			}
		}
		return "Unknown";
	}
}
