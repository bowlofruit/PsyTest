using UnityEngine;
using Zenject;
using System.Linq;
using Presenter.PsyTest;
using System.Collections.Generic;

namespace View.PsyTest
{
	public class TestListView : MonoBehaviour, ITestListView
	{
		[Header("UI Components")]
		[SerializeField] private Canvas _testListCanvas;
		[SerializeField] private PsyTestListPrefab _testPrefab;
		[SerializeField] private Transform _content;

		private TestPresenter _presenter;
		private List<PsyTestListPrefab> _testPrefabs = new List<PsyTestListPrefab>();

		public void InitPresenter(TestPresenter testPresenter)
		{
			_presenter = testPresenter;
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

			int testCount = _presenter.TestContainer.Count;

			if (_testPrefabs.Count < testCount)
			{
				for (int i = _testPrefabs.Count; i < testCount; i++)
				{
					var instance = Instantiate(_testPrefab, _content);
					_testPrefabs.Add(instance);
				}
			}
			else if (_testPrefabs.Count > testCount)
			{
				for (int i = _testPrefabs.Count - 1; i >= testCount; i--)
				{
					Destroy(_testPrefabs[i].gameObject);
					_testPrefabs.RemoveAt(i);
				}
			}

			for (int i = 0; i < testCount; i++)
			{
				var test = _presenter.TestContainer[i];
				var testPrefab = _testPrefabs[i];

				testPrefab.Init(test.Name, test.Description, test.Logo, test.Container);
			}
		}

		public void Activate() => gameObject.SetActive(true);
		public void Deactivate() => gameObject.SetActive(false);
	}
}
