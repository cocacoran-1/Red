using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{
    public InputField email;
    public InputField password;

    public Text outputText;
    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
    }
private void OnChangedState(bool sign)
{
    outputText.text = sign ? "로그인 :" : "로그아웃 :";  
    outputText.text =FirebaseAuthManager.Instance.UserId;
}
    public void Create()
    {
        FirebaseAuthManager.Instance.Create(email.text, password.text);
    }
    public void Login()
    {
        FirebaseAuthManager.Instance.LogIn(email.text, password.text);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }
}
