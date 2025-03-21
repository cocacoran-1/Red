using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    private Image fadeImage;
    public GameObject ResultFadeOut;
    public GameObject GameFadeOut;
    public GameObject Ui;
    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "InGameScene")
        {
            
            if (Ui != null)
            {
                Ui.SetActive(false);
            }
            else
            {
                Debug.LogError("UI Object not found: ResultUI");
            }
        }
    }
    public void ResultFadeEffectStart()
    {
        StartCoroutine(ResultFadeout(0.0f, 0.7f));
    }
    public void GameFadeEffectStart()
    {
        StartCoroutine(GameFadeout(0.0f, 1.0f));
    }
   
    private IEnumerator GameFadeout(float start, float end)
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
        SceneManager.LoadScene("main");
    }

    private IEnumerator ResultFadeout(float start, float end)
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
        Ui.SetActive(true);
        Time.timeScale = 0;

    }
   
}
