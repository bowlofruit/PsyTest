using System.Collections.Generic;

public static class TestData
{
	public static TestContainer GetSampleTest()
	{
		List<TestOption> options = new()
		{
			new() { Text = "Взагалі ні", Score = 0 },
			new() { Text = "Дещо", Score = 1 },
			new() { Text = "Помірно", Score = 2 },
			new() { Text = "Сильно", Score = 3 }
		};

		List<TestQuestion> questions = new()
		{
			new() { QuestionText = "Ви відчуваєте сум?", Options = options },
			new() { QuestionText = "Майбутнє здається вам безнадійним?", Options = options },
			new() { QuestionText = "Ви відчуваєте себе невдахою?", Options = options },
			new() { QuestionText = "Ви отримуєте менше задоволення від життя?", Options = options },
			new() { QuestionText = "У вас зникли інтереси до інших людей?", Options = options },
			new() { QuestionText = "Ви відчуваєте себе винним?", Options = options },
			new() { QuestionText = "Ви відчуваєте себе покараним?", Options = options },
			new() { QuestionText = "Ви розчаровані собою?", Options = options },
			new() { QuestionText = "У вас думки про самогубство?", Options = options },
			new() { QuestionText = "Ви більше плачете?", Options = options }
		};

		List<TestScoring> scoring = new()
		{
			new() { MinScore = 0, MaxScore = 9, Label = "Мінімальний рівень депресії" },
			new() { MinScore = 10, MaxScore = 18, Label = "Легка депресія" },
			new() { MinScore = 19, MaxScore = 29, Label = "Помірна депресія" },
			new() { MinScore = 30, MaxScore = 63, Label = "Тяжка депресія" }
		};

		return new TestContainer
		{
			Questions = questions,
			Scoring = scoring
		};
	}

	public static TestContainer GetSelfEsteemTest()
	{
		List<TestOption> options = new()
		{
			new() { Text = "Зовсім не погоджуюсь", Score = 0 },
			new() { Text = "Погоджуюсь частково", Score = 1 },
			new() { Text = "Переважно погоджуюсь", Score = 2 },
			new() { Text = "Повністю погоджуюсь", Score = 3 }
		};

		List<TestQuestion> questions = new()
		{
			new() { QuestionText = "Я задоволений собою", Options = options },
			new() { QuestionText = "Я відчуваю, що маю багато хороших якостей", Options = options },
			new() { QuestionText = "Я можу впоратися з більшістю проблем у своєму житті", Options = options },
			new() { QuestionText = "Я вартий поваги", Options = options },
			new() { QuestionText = "Я впевнений у своїх рішеннях", Options = options },
			new() { QuestionText = "Мені легко прийняти себе таким, яким я є", Options = options }
		};

		List<TestScoring> scoring = new()
		{
			new() { MinScore = 0, MaxScore = 5, Label = "Низька самооцінка" },
			new() { MinScore = 6, MaxScore = 10, Label = "Помірна самооцінка" },
			new() { MinScore = 11, MaxScore = 15, Label = "Висока самооцінка" },
			new() { MinScore = 16, MaxScore = 18, Label = "Дуже висока самооцінка" }
		};

		return new TestContainer
		{
			Questions = questions,
			Scoring = scoring
		};
	}

	public static TestContainer GetAnxietyTest()
	{
		List<TestOption> options = new()
		{
			new() { Text = "Ніколи", Score = 0 },
			new() { Text = "Інколи", Score = 1 },
			new() { Text = "Часто", Score = 2 },
			new() { Text = "Завжди", Score = 3 }
		};

		List<TestQuestion> questions = new()
		{
			new() { QuestionText = "Ви відчуваєте занепокоєння?", Options = options },
			new() { QuestionText = "Ви важко розслабляєтесь?", Options = options },
			new() { QuestionText = "Вас легко дратують дрібниці?", Options = options },
			new() { QuestionText = "Ви часто переживаєте про своє здоров'я?", Options = options },
			new() { QuestionText = "Ви боїтеся непередбачуваних подій?", Options = options },
			new() { QuestionText = "Ви уникаєте соціальних ситуацій?", Options = options }
		};

		List<TestScoring> scoring = new()
		{
			new() { MinScore = 0, MaxScore = 5, Label = "Мінімальний рівень тривожності" },
			new() { MinScore = 6, MaxScore = 10, Label = "Легка тривожність" },
			new() { MinScore = 11, MaxScore = 15, Label = "Помірна тривожність" },
			new() { MinScore = 16, MaxScore = 18, Label = "Тяжка тривожність" }
		};

		return new TestContainer
		{
			Questions = questions,
			Scoring = scoring
		};
	}
}