using Firebase.Auth;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ECS.Services.Firebase
{
	public class FirebaseAuthService : MonoBehaviour
	{
		private FirebaseAuth _firebaseAuth;

		void Awake()
		{
			_firebaseAuth = FirebaseAuth.DefaultInstance;
		}

		public async Task<bool> LoginAsync(string email, string password)
		{
			try
			{
				var result = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
				return result != null;
			}
			catch (Exception ex)
			{
				Debug.LogError($"Login failed: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> RegisterAsync(string email, string password)
		{
			try
			{
				var result = await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
				return result != null;
			}
			catch (Exception ex)
			{
				Debug.LogError($"Registration failed: {ex.Message}");
				return false;
			}
		}

		public async Task<FirebaseUser> GetCurrentUserAsync()
		{
			return _firebaseAuth.CurrentUser;
		}

		public void Logout()
		{
			_firebaseAuth.SignOut();
		}
	}
}
