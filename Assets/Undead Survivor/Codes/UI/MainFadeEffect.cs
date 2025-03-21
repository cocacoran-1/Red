using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainFadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    public GameObject FadeOut;
    private Image fadeImage;
    private void OnEnable()
    {
        fadeImage = GetComponent<Image>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Color color = fadeImage.color;
        color.a = 0.0f;
        fadeImage.color = color;
    }
    public void StartFadeEffectStart()
    {
        StartCoroutine(StartFadeout(0.0f, 1.0f));
    }
    private IEnumerator StartFadeout(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        LoadingSceneController.LoadingScene("InGameScene");
    }
}
