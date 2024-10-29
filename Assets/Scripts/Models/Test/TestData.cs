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

	public static TestContainer GetSelfEsteemTest()
	{
		// Створюємо кілька опцій для питань
		var options = new List<TestOption>
		{
			new TestOption { Text = "Зовсім не погоджуюсь", Score = 0 },
			new TestOption { Text = "Погоджуюсь частково", Score = 1 },
			new TestOption { Text = "Переважно погоджуюсь", Score = 2 },
			new TestOption { Text = "Повністю погоджуюсь", Score = 3 }
		};

		// Питання тесту на самооцінку
		var questions = new List<TestQuestion>
		{
			new TestQuestion { QuestionText = "Я задоволений собою", Options = options },
			new TestQuestion { QuestionText = "Я відчуваю, що маю багато хороших якостей", Options = options },
			new TestQuestion { QuestionText = "Я можу впоратися з більшістю проблем у своєму житті", Options = options },
			new TestQuestion { QuestionText = "Я вартий поваги", Options = options },
			new TestQuestion { QuestionText = "Я впевнений у своїх рішеннях", Options = options },
			new TestQuestion { QuestionText = "Мені легко прийняти себе таким, яким я є", Options = options }
		};

		// Шкала оцінювання для тесту на самооцінку
		var scoring = new List<TestScoring>
		{
			new TestScoring { MinScore = 0, MaxScore = 5, Label = "Низька самооцінка" },
			new TestScoring { MinScore = 6, MaxScore = 10, Label = "Помірна самооцінка" },
			new TestScoring { MinScore = 11, MaxScore = 15, Label = "Висока самооцінка" },
			new TestScoring { MinScore = 16, MaxScore = 18, Label = "Дуже висока самооцінка" }
		};

		// Повертаємо контейнер тесту
		return new TestContainer
		{
			Questions = questions,
			Scoring = scoring
		};
	}

	public static TestContainer GetAnxietyTest()
	{
		// Створюємо кілька опцій для питань
		var options = new List<TestOption>
		{
			new TestOption { Text = "Ніколи", Score = 0 },
			new TestOption { Text = "Інколи", Score = 1 },
			new TestOption { Text = "Часто", Score = 2 },
			new TestOption { Text = "Завжди", Score = 3 }
		};

		// Питання тесту на тривожність
		var questions = new List<TestQuestion>
		{
			new TestQuestion { QuestionText = "Ви відчуваєте занепокоєння?", Options = options },
			new TestQuestion { QuestionText = "Ви важко розслабляєтесь?", Options = options },
			new TestQuestion { QuestionText = "Вас легко дратують дрібниці?", Options = options },
			new TestQuestion { QuestionText = "Ви часто переживаєте про своє здоров'я?", Options = options },
			new TestQuestion { QuestionText = "Ви боїтеся непередбачуваних подій?", Options = options },
			new TestQuestion { QuestionText = "Ви уникаєте соціальних ситуацій?", Options = options }
		};

		// Шкала оцінювання для тесту на тривожність
		var scoring = new List<TestScoring>
		{
			new TestScoring { MinScore = 0, MaxScore = 5, Label = "Мінімальний рівень тривожності" },
			new TestScoring { MinScore = 6, MaxScore = 10, Label = "Легка тривожність" },
			new TestScoring { MinScore = 11, MaxScore = 15, Label = "Помірна тривожність" },
			new TestScoring { MinScore = 16, MaxScore = 18, Label = "Тяжка тривожність" }
		};

		// Повертаємо контейнер тесту
		return new TestContainer
		{
			Questions = questions,
			Scoring = scoring
		};
	}
}
