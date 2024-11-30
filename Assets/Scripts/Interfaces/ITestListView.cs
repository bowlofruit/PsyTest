public interface ITestListView: IStateHandler
{
	void InitPresenter(TestPresenter testPresenter);
	void DisplayQuestion(TestQuestion question);
	void ShowResult(TestResult result);
}
