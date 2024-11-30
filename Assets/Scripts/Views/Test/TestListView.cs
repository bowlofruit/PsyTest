using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestListView : MonoBehaviour, ITestListView
{
	[Header("Test List")]
	[SerializeField] private Canvas _testListCanvas;
	[SerializeField] private PsychologicalTestPrefab _testPrefab;
	[SerializeField] private Transform _content;
	[field: SerializeField] public Sprite TestSprite { get; set; }

	[Header("Test Question")]
	[SerializeField] private Canvas _testQuestionsCanvas;
	[SerializeField] private TMP_Text _questionText;
	[SerializeField] private Button _submitButton;

	[field: SerializeField] public GameObject OptionsContainer { get; set; }
	[field: SerializeField] public OptionPrefabView OptionPrefab { get; set; }

	[Header("Test Result")]
	[SerializeField] private Canvas _testResultCanvas;
	[SerializeField] private TMP_Text _testScoreText;
	[SerializeField] private TMP_Text _testResultDescriptionText;

	private OptionsManager _optionsManager;
	private ToggleGroup _toggleGroup;
	private TestPresenter _presenter;

	[Inject]
	public void Construct(OptionsManager optionsManager)
	{
		_optionsManager = optionsManager;
		_submitButton.onClick.AddListener(OnSubmit);
		ShowCanvas(_testListCanvas);
	}

	public void InitPresenter(TestPresenter presenter)
	{
		_presenter = presenter;
		PopulateTestList();
	}

	private void PopulateTestList()
	{
		foreach (var test in _presenter.TestContainer)
		{
			var testPrefabInstance = Instantiate(_testPrefab, _content);
			testPrefabInstance.Init(test.Name, test.Description, test.Logo, test.Container, _presenter);
		}
	}

	public void DisplayQuestion(TestQuestion question)
	{
		ShowCanvas(_testQuestionsCanvas);
		_questionText.text = question.QuestionText;
		_toggleGroup = _optionsManager.CreateOptions(question);
		_submitButton.interactable = true;
	}

	private void OnSubmit()
	{
		var selectedToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
		if (selectedToggle != null)
		{
			var selectedOption = selectedToggle.GetComponentInChildren<TMP_Text>().text;
			_presenter.SubmitAnswer(selectedOption);
		}
		else
		{
			Debug.LogWarning("No option selected.");
		}
	}

	public void ShowResult(TestResult result)
	{
		ShowCanvas(_testResultCanvas);
		_testScoreText.text = result.TotalScore.ToString();
		_testResultDescriptionText.text = result.ResultLabel;
	}

	private void ShowCanvas(Canvas targetCanvas)
	{
		_testListCanvas.gameObject.SetActive(targetCanvas == _testListCanvas);
		_testQuestionsCanvas.gameObject.SetActive(targetCanvas == _testQuestionsCanvas);
		_testResultCanvas.gameObject.SetActive(targetCanvas == _testResultCanvas);
	}

	public void Activate() => gameObject.SetActive(true);

	public void Deactivate() => gameObject.SetActive(false);
}