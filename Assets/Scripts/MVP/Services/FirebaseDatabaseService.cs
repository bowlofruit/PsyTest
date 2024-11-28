using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseDatabaseService
{
	private DatabaseReference _databaseRef;

	public FirebaseDatabaseService()
	{
		_databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
	}

	public async Task SubmitTherapistRequest(string userId, string contactInfo, string documentsUrl)
	{
		var requestId = _databaseRef.Child("roleRequests").Push().Key;

		await _databaseRef.Child("roleRequests").Child(requestId)
			.SetRawJsonValueAsync(JsonUtility.ToJson(new TherapistRoleRequest
			{
				UserId = userId,
				ContactInfo = contactInfo,
				Documents = documentsUrl,
				Status = "Pending",
				RequestDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")
			}));

		await _databaseRef.Child("users").Child(userId).Child("therapistRequest").SetValueAsync("Pending");
	}

	public async Task ApproveOrRejectRequest(string requestId, bool isApproved, string reason = "")
	{
		string status = isApproved ? "Approved" : "Rejected";

		await _databaseRef.Child("roleRequests").Child(requestId).Child("status").SetValueAsync(status);

		if (isApproved)
		{
			DataSnapshot snapshot = await _databaseRef.Child("roleRequests").Child(requestId).Child("userId").GetValueAsync();
			string userId = snapshot.Value.ToString();

			await _databaseRef.Child("users").Child(userId).Child("role").SetValueAsync("therapist");
		}
		else
		{
			await _databaseRef.Child("roleRequests").Child(requestId).Child("rejectionReason").SetValueAsync(reason);
		}
	}
}