using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using System.Linq;

public class TestView : MonoBehaviour, ITestView
{
	[Header("Test Question")]
	[SerializeField] private Canvas _testQustions;
	[SerializeField] private TMP_Text _questionText;
	[SerializeField] private Button _submitButton;

	private TestPresenter _presenter;
	private OptionsManager _optionsManager;
	private ToggleGroup _toggleGroup;

	[field: SerializeField] public GameObject OptionsContainer { get; set; }
	[field: SerializeField] public OptionPrefabView OptionPrefab { get; set; }

	[Header("Test Result")]
	[SerializeField] private Canvas _testResult;
	[SerializeField] private TMP_Text _testScore;
	[SerializeField] private TMP_Text _testResultDescription;

	private bool _isResult = false;

	[Inject]
	public void Construct(TestPresenter presenter, OptionsManager optionsManager)
	{
		_presenter = presenter;
		_optionsManager = optionsManager;

		_presenter.StartTest();
		_submitButton.onClick.AddListener(OnSubmit);
		SwitchCanvas(_isResult);
	}

	private void SwitchCanvas(bool isResult)
	{
		_testQustions.gameObject.SetActive(!isResult);
		_testResult.gameObject.SetActive(isResult);
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
		}
		else
		{
			Debug.LogWarning("No option selected.");
		}
	}

	public void ShowResult(TestResult result)
	{
		SwitchCanvas(_isResult = true);

		_testScore.text = result.TotalScore.ToString();
		_testResultDescription.text = result.ResultLabel;
	}
}