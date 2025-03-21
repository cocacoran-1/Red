using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnaffectedByPause : MonoBehaviour
{
    private float previousTimeScale;

    void OnEnable()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 1;
    }

    void OnDisable()
    {
        Time.timeScale = previousTimeScale;
    }
}
