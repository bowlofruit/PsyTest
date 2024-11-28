using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using PsyTest.Profile;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseAuthService
{
	private FirebaseAuth _auth;
	private FirebaseUser _user;
	private DatabaseReference _databaseRef;

	public FirebaseAuthService()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Result == DependencyStatus.Available)
			{
				FirebaseApp app = FirebaseApp.DefaultInstance;
				_auth = FirebaseAuth.DefaultInstance;
				_databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
			}
		});
	}

	public async Task<FirebaseUser> RegisterUser(string email, string password, string username)
	{
		try
		{
			var userCredential = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
			_user = userCredential.User;

			await _databaseRef.Child("users").Child(_user.UserId)
				.SetRawJsonValueAsync(JsonUtility.ToJson(new FirebaseUserData
				{
					Username = username,
					Email = email,
					Role = "user"
				}));

			await _user.SendEmailVerificationAsync();

			return _user;
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to register user: {e.Message}");
			throw;
		}
	}

	public async Task LoginUser(string email, string password)
	{
		await _auth.SignInWithEmailAndPasswordAsync(email, password);
		_user = _auth.CurrentUser;
	}

	public Task<UserProfileInfo> GetUserProfile()
	{
		FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

		if (user != null)
		{
			UserProfileInfo userProfile = new UserProfileInfo
			{
				UserId = user.UserId,
				Name = user.DisplayName,
				Login = user.Email,
				Email = user.Email,
				Role = "client"
			};

			return Task.FromResult(userProfile);
		}

		return Task.FromResult<UserProfileInfo>(null);
	}

	public bool IsEmailVerified() => _auth.CurrentUser?.IsEmailVerified ?? false;
}