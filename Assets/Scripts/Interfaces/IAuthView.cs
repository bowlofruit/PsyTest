public interface IAuthView : IStateHandler
{
	void InitPresenter(AuthPresenter authPresenter);
	void ShowSuccess(string message);
	void ShowError(string message);
}
