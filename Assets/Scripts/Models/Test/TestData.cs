using System.Collections.Generic;

public static class TestData
{
	public static TestContainer GetSampleTest()
	{
		// Створюємо кілька опцій для питань
		var options = new List<TestOption>
		{
			new TestOption { Text = "Взагалі ні", Score = 0 },
			new TestOption { Text = "Дещо", Score = 1 },
			new TestOption { Text = "Помірно", Score = 2 },
			new TestOption { Text = "Сильно", Score = 3 }
		};

		// Питання тесту депресії Бека
		var questions = new List<TestQuestion>
		{
			new TestQuestion { QuestionText = "Ви відчуваєте сум?", Options = options },
			new TestQuestion { QuestionText = "Майбутнє здається вам безнадійним?", Options = options },
			new TestQuestion { QuestionText = "Ви відчуваєте себе невдахою?", Options = options },
			new TestQuestion { QuestionText = "Ви отримуєте менше задоволення від життя?", Options = options },
			new TestQuestion { QuestionText = "У вас зникли інтереси до інших людей?", Options = options },
			new TestQuestion { QuestionText = "Ви відчуваєте себе винним?", Options = options },
			new TestQuestion { QuestionText = "Ви відчуваєте себе покараним?", Options = options },
			new TestQuestion { QuestionText = "Ви розчаровані собою?", Options = options },
			new TestQuestion { QuestionText = "У вас думки про самогубство?", Options = options },
			new TestQuestion { QuestionText = "Ви більше плачете?", Options = options }
		};

		// Шкала оцінювання
		var scoring = new List<TestScoring>
		{
			new TestScoring { MinScore = 0, MaxScore = 9, Label = "Мінімальний рівень депресії" },
			new TestScoring { MinScore = 10, MaxScore = 18, Label = "Легка депресія" },
			new TestScoring { MinScore = 19, MaxScore = 29, Label = "Помірна депресія" },
			new TestScoring { MinScore = 30, MaxScore = 63, Label = "Тяжка депресія" }
		};

		// Повертаємо контейнер тесту
		return new TestContainer
		{
			Questions = questions,
			Scoring = scoring
		};
	}
}
