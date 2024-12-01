using Presenter.PsyTest;
using TMPro;
using UnityEngine;
using Zenject;

namespace View.PsyTest
{
	public class TestResultView : MonoBehaviour, IStateHandler
	{
		[Header("UI Components")]
		[SerializeField] private Canvas _testResultCanvas;
		[SerializeField] private TMP_Text _testScoreText;
		[SerializeField] private TMP_Text _testResultDescriptionText;

		private TestPresenter _presenter;

		[Inject]
		public void Construct(TestPresenter presenter)
		{
			_presenter = presenter;
		}

		public void ShowResult(TestResult result)
		{
			_testScoreText.text = result.TotalScore.ToString();
			_testResultDescriptionText.text = result.ResultLabel;
		}

		public void Activate() => gameObject.SetActive(true);
		public void Deactivate() => gameObject.SetActive(false);
	}
}