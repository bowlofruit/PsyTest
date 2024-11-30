using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestListView : MonoBehaviour, ITestListView
{
	[Header("Test List")]
	[SerializeField] private Canvas _testList;
	[SerializeField] private PsychologicalTestPrefab _testPrefab;
	[SerializeField] private Transform _content;
	[field: SerializeField] public Sprite TestSprite { get; set; }

	[Header("Test Question")]
	[SerializeField] private Canvas _testQustions;
	[SerializeField] private TMP_Text _questionText;
	[SerializeField] private Button _submitButton;

	private OptionsManager _optionsManager;
	private ToggleGroup _toggleGroup;
	private TestPresenter _presenter;

	[field: SerializeField] public GameObject OptionsContainer { get; set; }
	[field: SerializeField] public OptionPrefabView OptionPrefab { get; set; }

	[Header("Test Result")]
	[SerializeField] private Canvas _testResult;
	[SerializeField] private TMP_Text _testScore;
	[SerializeField] private TMP_Text _testResultDescription;

	[Inject]
	public void Construct(OptionsManager optionsManager)
	{
		_optionsManager = optionsManager;
		_submitButton.onClick.AddListener(OnSubmit);
		ShowTestList(true);
	}

	public void InitPresenter(TestPresenter testPresenter)
	{
		_presenter = testPresenter;
		ShowTestList();
	}

	private void ShowTestList(bool isTestList)
	{
		_testList.gameObject.SetActive(isTestList);
		_testQustions.gameObject.SetActive(!isTestList);
		_testResult.gameObject.SetActive(!isTestList);
	}

	private void ShowTestList()
	{
		foreach (Test item in _presenter.TestContainer)
		{
			PsychologicalTestPrefab testInList = Instantiate(_testPrefab, _content);
			testInList.Init(item.Name, item.Description, item.Logo, item.Container, _presenter);
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
		Toggle selectedToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
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

	public void Activate()
	{
		gameObject.SetActive(true);
	}

	public void Deactivate()
	{
		gameObject.SetActive(true);
	}
}