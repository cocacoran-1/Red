using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)   //씬 빌드번호가 0일때 (StartScene일때)
        {
            if (Input.GetMouseButtonDown(0))    //화면을 터치하면
            {
                LoadingSceneController.LoadingScene("main");    //main씬 로드
            }
        }

    }
}
