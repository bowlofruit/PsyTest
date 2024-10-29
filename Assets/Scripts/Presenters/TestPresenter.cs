using System.Collections.Generic;

public class TestPresenter
{
	private readonly ITestView view;
	private readonly List<Test> testContainer;
	private readonly UserTest userTest;
	private int currentQuestionIndex;
	private TestContainer currentTest;

	public List<Test> TestContainer => testContainer;

	public TestPresenter(ITestView view, List<Test> testContainer, UserTest userTest)
	{
		this.view = view;
		this.testContainer = testContainer;
		this.userTest = userTest;
		currentQuestionIndex = 0;
		currentTest = testContainer[0].Container;
	}

	public void StartTest()
	{
		LoadNextQuestion();
	}

	private void LoadNextQuestion()
	{
		if (currentQuestionIndex < currentTest.Questions.Count)
		{
			view.DisplayQuestion(currentTest.Questions[currentQuestionIndex]);
		}
		else
		{
			ShowResults();
		}
	}

	public void SubmitAnswer(string selectedOptionText)
	{
		TestQuestion currentQuestion = currentTest.Questions[currentQuestionIndex];

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
		foreach (var scoring in currentTest.Scoring)
		{
			if (score >= scoring.MinScore && score <= scoring.MaxScore)
			{
				return scoring.Label;
			}
		}
		return "Unknown";
	}
}
