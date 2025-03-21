using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;
using UnityEngine.SceneManagement;

public class FirebaseAuthManager
{
    private static FirebaseAuthManager instance = null;

    public static FirebaseAuthManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FirebaseAuthManager();
            }

            return instance;
        }
    }

    private FirebaseAuth auth;
    private FirebaseUser user;

    public string UserId => user.UserId;
    public Action<bool> LoginState;
    public void Init()
    {
        {
            auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                LogOut();
            }
            auth.StateChanged += OnChanged;
        }
    }
    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user != null)
            {
                Debug.Log("로그아웃");
                LoginState?.Invoke(false);
            }
            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("로그인");
                LoginState?.Invoke(true);
            }
        }
    }
    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("회원가입 취소");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("회원가입 실패");
                return;
            }
            FirebaseUser newUser = task.Result;
            Debug.Log("회원가입 완료");
        });
    }
    public void LogIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("로그인 실패");
                return;
            }
            FirebaseUser newUser = task.Result;
            Debug.Log("로그인 완료");
            SceneManager.LoadScene("main");
        });
    }
    public void LogOut()
    {
        auth.SignOut();
    }
}
