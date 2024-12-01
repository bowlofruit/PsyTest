using Presenter.PsyTest;

public interface ITestListView : IStateHandler
{
	public void InitPresenter(TestPresenter testPresenter);
}