public interface ITestView
{
	void SetPresenter(TestPresenter presenter);
	void DisplayQuestion(TestQuestion question);
	void ShowResult(TestResult result);
}