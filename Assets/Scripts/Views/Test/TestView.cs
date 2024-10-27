using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestView : MonoBehaviour, ITestView
{
	[SerializeField] private TMP_Text _questionText; // Текстове поле для відображення питання
	[SerializeField] private GameObject _optionsContainer; // Контейнер для опцій
	[SerializeField] private OptionPrefabView _optionPrefab; // Префаб для створення опцій
	[SerializeField] private Button _submitButton; // Кнопка для підтвердження вибору

	private TestPresenter presenter;

	public void SetPresenter(TestPresenter presenter)
	{
		this.presenter = presenter;
		_submitButton.onClick.AddListener(OnSubmit); // Підключення події кнопки
	}

	public void DisplayQuestion(TestQuestion question)
	{
		// Очищення попередніх опцій
		foreach (Transform child in _optionsContainer.transform)
		{
			Destroy(child.gameObject);
		}

		// Відображення тексту питання
		_questionText.text = question.QuestionText;

		// Додавання опцій
		foreach (var option in question.Options)
		{
			var optionObject = Instantiate(_optionPrefab, _optionsContainer.transform);
			optionObject.Answer.text = option.Text;
			optionObject.RadioButton.group = _optionPrefab.GetComponent<ToggleGroup>();
		}

		// Включення кнопки підпису
		_submitButton.interactable = true;
	}

	private void OnSubmit()
	{
		// Отримання вибраної опції
		Toggle selectedToggle = _optionsContainer.GetComponentInChildren<Toggle>(true);
		if (selectedToggle != null)
		{
			presenter.SubmitAnswer(selectedToggle.GetComponentInChildren<Text>().text);
			_submitButton.interactable = false; // Вимкнення кнопки після подачі
		}
	}

	public void ShowResult(TestResult result)
	{
		Debug.Log($"Test Completed! Score: {result.TotalScore}, Result: {result.ResultLabel}");
	}
}
