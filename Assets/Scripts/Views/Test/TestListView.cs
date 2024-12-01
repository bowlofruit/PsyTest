using UnityEngine;
using Zenject;
using System.Linq;
using Presenter.PsyTest;

namespace View.PsyTest
{
	public class TestListView : MonoBehaviour, ITestListView
	{
		[Header("UI Components")]
		[SerializeField] private Canvas _testListCanvas;
		[SerializeField] private PsyTestListPrefab _testPrefab;
		[SerializeField] private Transform _content;

		private TestPresenter _presenter;

		[Inject]
		public void Construct(TestPresenter presenter)
		{
			_presenter = presenter;
		}

		private void OnEnable()
		{
			if (_presenter == null)
			{
				Debug.LogError("Presenter is not initialized yet.");
				return;
			}

			PopulateTestList();
		}

		private void PopulateTestList()
		{
			if (_presenter.TestContainer == null || !_presenter.TestContainer.Any())
			{
				Debug.LogWarning("Test list is empty.");
				return;
			}

			foreach (var test in _presenter.TestContainer)
			{
				var instance = Instantiate(_testPrefab, _content);
				instance.Init(test.Name, test.Description, test.Logo, test.Container);
			}
		}

		public void Activate() => gameObject.SetActive(true);
		public void Deactivate() => gameObject.SetActive(false);
	}
}