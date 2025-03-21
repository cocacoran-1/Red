using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearUI : MonoBehaviour
{
    CanvasManager canvasManager;

    private void Awake()
    {
        canvasManager = GameObject.Find("GamePlayCanvas").GetComponent<CanvasManager>();
    }
    public void AppearInactive()
    {
        gameObject.SetActive(false);
    }
}
