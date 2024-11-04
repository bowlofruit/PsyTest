public class AdminPresenter
{
	private readonly FirebaseDatabaseService _dbService;
	private readonly IAdminView _view;

	public AdminPresenter(FirebaseDatabaseService dbService, IAdminView view)
	{
		_dbService = dbService;
		_view = view;
	}

	public async void OnApproveRequest(string requestId)
	{
		await _dbService.ApproveOrRejectRequest(requestId, true);
		_view.ShowSuccess("Request approved.");
	}

	public async void OnRejectRequest(string requestId, string reason)
	{
		await _dbService.ApproveOrRejectRequest(requestId, false, reason);
		_view.ShowSuccess("Request rejected.");
	}
}

public interface IAdminView
{
	void ShowSuccess(string message);
	void ShowError(string message);
}
