using System.Collections.Generic;
using View.PsyTest;

namespace Presenter.PsyTest
{
	public class TestPresenter
	{
		public List<Test> TestContainer { get; private set; }

		private readonly TestQuestionsView _testQuestionsView;
		private readonly TestResultView _testResultView;
		private readonly TestSelectionMediator _selectionMediator;

		public TestPresenter(
			TestContainer testContainer,
			TestQuestionsView testQuestionsView,
			TestResultView testResultView,
			TestSelectionMediator testSelectionMediator)
		{
			
			_testQuestionsView = testQuestionsView;
			_testResultView = testResultView;

			_selectionMediator = testSelectionMediator;
			_selectionMediator.OnTestSelected += StartTest;
		}

		public void StartTest(TestContainer selectedTest)
		{
			
		}

		public void DisplayQuestion(TestQuestion question)
		{
			_testQuestionsView.DisplayQuestion(question);
		}

		public void SubmitAnswer(string selectedOption)
		{

		}

		public void ShowResult(TestResult result)
		{
			_testResultView.ShowResult(result);
		}
	}
}
