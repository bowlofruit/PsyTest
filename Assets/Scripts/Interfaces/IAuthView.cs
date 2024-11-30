public interface IAuthView : IStateHandler
{
	void ShowSuccess(string message);
	void ShowError(string message);
}
