
public interface ITestPresenterFactory
{
	TestPresenter Create(UserTest userTest);
}

public class TestPresenterFactory : ITestPresenterFactory
{
	private readonly ITestView view;

	public TestPresenterFactory(ITestView view)
	{
		this.view = view;
	}

	public TestPresenter Create(UserTest userTest)
	{
		return new TestPresenter(view, userTest);
	}
}
