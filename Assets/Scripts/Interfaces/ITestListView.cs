public interface ITestListView: IStateHandler
{
	void DisplayQuestion(TestQuestion question);
	void ShowResult(TestResult result);
}
