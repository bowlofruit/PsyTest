using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestView : MonoBehaviour, ITestView
{
	[SerializeField] private TMP_Text _questionText;
	[SerializeField] private Button _submitButton;

	private TestPresenter _presenter;
	private OptionsManager _optionsManager;
	private ToggleGroup _toggleGroup;

	[field: SerializeField] public GameObject OptionsContainer { get; set; }
	[field: SerializeField] public OptionPrefabView OptionPrefab { get; set; }

	[Inject]
	public void Construct(OptionsManager optionsManager)
	{
		_optionsManager = optionsManager;
	}

	public void SetPresenter(TestPresenter presenter)
	{
		_presenter = presenter;
		_submitButton.onClick.AddListener(OnSubmit);
	}

	public void DisplayQuestion(TestQuestion question)
	{
		_questionText.text = question.QuestionText;
		_toggleGroup = _optionsManager.CreateOptions(question);
		_submitButton.interactable = true;
	}

	private void OnSubmit()
	{
		var selectedToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
		if (selectedToggle != null)
		{
			_presenter.SubmitAnswer(selectedToggle.GetComponentInChildren<TMP_Text>().text);
			_submitButton.interactable = false;
		}
		else
		{
			Debug.LogWarning("No option selected.");
		}
	}

	public void ShowResult(TestResult result)
	{
		Debug.Log($"Test Completed! Score: {result.TotalScore}, Result: {result.ResultLabel}");
	}
}