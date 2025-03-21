using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaPanel : MonoBehaviour
{
    public GameObject panel;
    public bool isTouch = false;
 

    private void OnEnable()
    {
        Invoke("touch_panel", 1.5f);
    }
    private void Update()
    {
        if (Input.touchCount > 0 && panel.activeSelf&&isTouch )
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {

                isTouch = false;
                SetActivefalse(panel, false);
            }
        }
    }

    private void SetActivefalse(GameObject obj, bool active)
    {
        obj.SetActive(active);
        foreach (Transform child in obj.transform)
        {
            SetActivefalse(child.gameObject, active);
        }
    }

    void touch_panel()
    {
        isTouch=true;
    }
    
}
