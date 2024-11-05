using Firebase.Auth;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;
using System;
using UnityEngine;
using Firebase.Extensions;

public class FirebaseAuthService
{
	private FirebaseAuth _auth;
	private FirebaseUser _user;
	private DatabaseReference _databaseRef;

	public FirebaseAuthService()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
			if (task.Result == DependencyStatus.Available)
			{
				FirebaseApp app = FirebaseApp.DefaultInstance;

				_auth = FirebaseAuth.DefaultInstance;
				_databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

				Debug.Log("Firebase initialized successfully");
			}
			else
			{
				Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
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

	public bool IsEmailVerified() => _auth.CurrentUser?.IsEmailVerified ?? false;
}
