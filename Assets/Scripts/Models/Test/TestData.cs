using System.Collections.Generic;

public static class TestData
{
	public static TestContainer GetSampleTest()
	{
		// Створюємо кілька опцій для питань
		var options1 = new List<TestOption>
		{
			new TestOption { Text = "Завжди", Score = 3 },
			new TestOption { Text = "Часто", Score = 2 },
			new TestOption { Text = "Інколи", Score = 1 },
			new TestOption { Text = "Ніколи", Score = 0 }
		};

		var options2 = new List<TestOption>
		{
			new TestOption { Text = "Так", Score = 2 },
			new TestOption { Text = "Ні", Score = 0 }
		};

		// Створюємо питання
		var questions = new List<TestQuestion>
		{
			new TestQuestion { QuestionText = "Як часто ви відчуваєте себе щасливими?", Options = options1 },
			new TestQuestion { QuestionText = "Чи відчуваєте ви стрес?", Options = options2 }
		};

		// Створюємо шкалу оцінювання
		var scoring = new List<TestScoring>
		{
			new TestScoring { MinScore = 0, MaxScore = 2, Label = "Низький рівень депресії" },
			new TestScoring { MinScore = 3, MaxScore = 4, Label = "Середній рівень депресії" },
			new TestScoring { MinScore = 5, MaxScore = 6, Label = "Високий рівень депресії" }
		};

		// Повертаємо контейнер тесту
		return new TestContainer
		{
			Questions = questions,
			Scoring = scoring
		};
	}
}
