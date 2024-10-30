using System.Collections.Generic;

public class TestPresenter
{
	private readonly ITestView _view;
	private readonly List<Test> _testContainer;
	private readonly UserTest _userTest;
	private int _currentQuestionIndex;
	private TestContainer _currentTest;

	public List<Test> TestContainer => _testContainer;

	public TestPresenter(ITestView view, List<Test> testContainer, UserTest userTest)
	{
		_view = view;
		_testContainer = testContainer;
		_userTest = userTest;
		_currentQuestionIndex = 0;
	}

	public void StartTest(TestContainer container)
	{
		_currentTest = container;
		LoadNextQuestion();
	}

	private void LoadNextQuestion()
	{
		if (_currentQuestionIndex < _currentTest.Questions.Count)
		{
			_view.DisplayQuestion(_currentTest.Questions[_currentQuestionIndex]);
		}
		else
		{
			ShowResults();
		}
	}

	public void SubmitAnswer(string selectedOptionText)
	{
		TestQuestion currentQuestion = _currentTest.Questions[_currentQuestionIndex];

		int selectedScore = GetScoreForSelectedOption(currentQuestion, selectedOptionText);
		_userTest.CurrentScore += selectedScore;

		_currentQuestionIndex++;
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
		_view.ShowResult(result);
	}

	private TestResult CalculateTestResult()
	{
		TestResult result = new TestResult
		{
			TestId = _userTest.TestId,
			TotalScore = _userTest.CurrentScore,
			ResultLabel = GetResultLabel(_userTest.CurrentScore)
		};

		return result;
	}

	private string GetResultLabel(int score)
	{
		foreach (var scoring in _currentTest.Scoring)
		{
			if (score >= scoring.MinScore && score <= scoring.MaxScore)
			{
				return scoring.Label;
			}
		}
		return "Unknown";
	}
}
