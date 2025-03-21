using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class testing : MonoBehaviour
{
    public Image imageToChange;
    public float initialDelay = 1.0f;
    public float duration = 1.0f;

    private void Start()
    {
        StartCoroutine(FadeToWhiteAndBack());
    }

    private IEnumerator FadeToWhiteAndBack()
    {
        Color originalColor = imageToChange.color;
       
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        imageToChange.color = transparentColor;

        yield return new WaitForSeconds(initialDelay);

        Sequence colorSequence = DOTween.Sequence();

        
        colorSequence.Append(imageToChange.DOColor(originalColor, duration));

        colorSequence.Play();
    }
}
