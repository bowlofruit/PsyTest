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

	// Submit therapist role request and save it in the database
	public async Task SubmitTherapistRequest(string userId, string contactInfo, string documentsUrl)
	{
		var requestId = _databaseRef.Child("roleRequests").Push().Key;

		// Create request object and set it in Firebase Database
		await _databaseRef.Child("roleRequests").Child(requestId)
			.SetRawJsonValueAsync(JsonUtility.ToJson(new TherapistRoleRequest
			{
				UserId = userId,
				ContactInfo = contactInfo,
				Documents = documentsUrl,
				Status = "Pending",
				RequestDate = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")
			}));

		// Update user status in Firebase Database
		await _databaseRef.Child("users").Child(userId).Child("therapistRequest").SetValueAsync("Pending");
	}

	// Approve or reject a therapist request
	public async Task ApproveOrRejectRequest(string requestId, bool isApproved, string reason = "")
	{
		string status = isApproved ? "Approved" : "Rejected";

		// Update the request status in Firebase Database
		await _databaseRef.Child("roleRequests").Child(requestId).Child("status").SetValueAsync(status);

		// If approved, change the user's role in Firebase
		if (isApproved)
		{
			// Get the userId from the role request
			DataSnapshot snapshot = await _databaseRef.Child("roleRequests").Child(requestId).Child("userId").GetValueAsync();
			string userId = snapshot.Value.ToString(); // Correct conversion from DataSnapshot to string

			// Update the user's role to "therapist"
			await _databaseRef.Child("users").Child(userId).Child("role").SetValueAsync("therapist");
		}
		else
		{
			// Optionally store the rejection reason
			await _databaseRef.Child("roleRequests").Child(requestId).Child("rejectionReason").SetValueAsync(reason);
		}
	}
}