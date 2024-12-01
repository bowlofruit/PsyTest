using System;

public class TestSelectionMediator
{
	public event Action<TestContainer> OnTestSelected;

	public void SelectTest(TestContainer test)
	{
		OnTestSelected?.Invoke(test);
	}
}