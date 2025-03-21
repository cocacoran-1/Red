using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    //Image progressBar;
    public Slider progressBar;

    public static void LoadingScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); //씬을 불러오는 도중 작업가능한 비동기방식
        op.allowSceneActivation = false; //씬로딩이 끝나면 자동으로 불러온 씬으로 이동할 것인지 설정
                                         //(90%까지만 씬을 로드하고 다음씬으로 넘어가지x)

        float timer = 0f;

        while (!op.isDone)
        {
            yield return null;
            timer += Time.time;

            if (op.progress < 0.9f)
            {
                //progressBar.value = op.progress;
                progressBar.value = Mathf.Lerp(progressBar.value, 0.9f, timer); //Mathf.MoveTowards
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                //progressBar.value = Mathf.Lerp(0.9f, 1f, timer);
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value == 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}
