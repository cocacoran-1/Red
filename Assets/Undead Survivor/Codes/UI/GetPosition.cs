using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetPosition : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        Vector3 buttonPosition = button.transform.position;
        Debug.Log("Button clicked at position: " + buttonPosition);
    }
}
