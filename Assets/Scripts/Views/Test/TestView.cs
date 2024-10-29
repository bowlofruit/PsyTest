using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using System.Linq;

public class TestView : MonoBehaviour, ITestView
{
	[Header("Test List")]
	[SerializeField] private Canvas _testList;
	[SerializeField] private PsychologicalTestPrefab _testPrefab;
	[SerializeField] private Transform _content;

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


	[Inject]
	public void Construct(TestPresenter presenter, OptionsManager optionsManager)
	{
		_presenter = presenter;
		_optionsManager = optionsManager;

		_submitButton.onClick.AddListener(OnSubmit);

		_testList.gameObject.SetActive(true);
		_testQustions.gameObject.SetActive(false);
		_testResult.gameObject.SetActive(false);

		ShowTestList();
	}

	private void ShowTestList()
	{
        foreach (var item in _presenter.TestContainer)
        {
			PsychologicalTestPrefab testInList = Instantiate(_testPrefab, _content);
			testInList.TestLogo = item.Logo;
			testInList.TestName.text = item.Name;
			testInList.TestDescription.text = item.Description;
        }
    }

	public void DisplayQuestion(TestQuestion question)
	{
		if (!_testQustions.gameObject.activeSelf)
		{
			_testList.gameObject.SetActive(false);
			_testQustions.gameObject.SetActive(true);
			_testResult.gameObject.SetActive(false);
		}

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
		_testList.gameObject.SetActive(false);
		_testQustions.gameObject.SetActive(false);
		_testResult.gameObject.SetActive(true);

		_testScore.text = result.TotalScore.ToString();
		_testResultDescription.text = result.ResultLabel;
	}
}
