using System.Collections.Generic;

public class TestPresenter
{
	private readonly ITestView view; // Інтерфейс для взаємодії з View
	private readonly TestContainer testContainer; // Контейнер з тестами
	private readonly UserTest userTest; // Інформація про проходження тесту
	private int currentQuestionIndex; // Індекс поточного питання

	public TestPresenter(ITestView view, UserTest userTest)
	{
		this.view = view;
		testContainer = TestData.GetSampleTest(); // Завантаження тесту з тестових даних
		this.userTest = userTest;
		currentQuestionIndex = 0;

		view.SetPresenter(this);

		LoadNextQuestion();
	}

	private void LoadNextQuestion()
	{
		if (currentQuestionIndex < testContainer.Questions.Count)
		{
			// Завантаження і відображення наступного питання
			view.DisplayQuestion(testContainer.Questions[currentQuestionIndex]);
		}
		else
		{
			// Якщо всі питання пройдені, показати результати
			ShowResults();
		}
	}

	public void SubmitAnswer(string selectedOptionText)
	{
		// Обробка вибраної відповіді
		// Знайдемо вибране питання
		TestQuestion currentQuestion = testContainer.Questions[currentQuestionIndex];

		// Отримаємо вибрану опцію (можна реалізувати логіку для оцінювання)
		int selectedScore = GetScoreForSelectedOption(currentQuestion, selectedOptionText);
		userTest.CurrentScore += selectedScore;

		// Переміститися до наступного питання
		currentQuestionIndex++;
		LoadNextQuestion();
	}

	private int GetScoreForSelectedOption(TestQuestion question, string selectedOptionText)
	{
		foreach (var option in question.Options)
		{
			if (option.Text == selectedOptionText)
			{
				return option.Score; // Повертаємо бал за вибрану відповідь
			}
		}
		return 0; // Якщо опція не знайдена, повертаємо 0
	}

	private void ShowResults()
	{
		// Логіка для обчислення результатів
		TestResult result = CalculateTestResult();

		// Показ результатів через View
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
				return scoring.Label; // Повертаємо етикет для відповідного рахунку
			}
		}
		return "Unknown"; // Якщо результат не відомий
	}
}