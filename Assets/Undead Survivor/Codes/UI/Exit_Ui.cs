using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Ui : MonoBehaviour
{
    public GameObject Exit_UI;
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(Exit_UI.activeSelf)
                {
                    Exit_UI.SetActive(false);
                }
                else
                {
                    Exit_UI.SetActive(true);
                }
            }
        }
    }

    public void Exit_game()
    {
        Exit_UI.SetActive(false);
        Application.Quit();
    }
}
