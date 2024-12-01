using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using System.Linq;
using Presenter.PsyTest;

namespace View.PsyTest
{
	public class TestQuestionsView : MonoBehaviour, IStateHandler
	{
		[SerializeField] private Canvas _testQuestionsCanvas;
		[SerializeField] private TMP_Text _questionText;
		[SerializeField] private Button _submitButton;

		[field: SerializeField] public GameObject OptionsContainer { get; private set; }
		[field: SerializeField] public OptionPrefabView OptionPrefab { get; private set; }

		private ToggleGroup _toggleGroup;
		private TestPresenter _presenter;
		private OptionsManager _optionsManager;

		[Inject]
		public void Construct(TestPresenter presenter, OptionsManager optionsManager)
		{
			_presenter = presenter;
			_optionsManager = optionsManager;
		}

		private void Awake()
		{
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
			var selectedOption = _toggleGroup.ActiveToggles()
											 .Select(toggle => toggle.GetComponentInChildren<TMP_Text>().text)
											 .FirstOrDefault();

			if (string.IsNullOrEmpty(selectedOption))
			{
				Debug.LogWarning("No option selected.");
				return;
			}

			_presenter.SubmitAnswer(selectedOption);
		}

		public void Activate() => gameObject.SetActive(true);
		public void Deactivate() => gameObject.SetActive(false);
	}
}
