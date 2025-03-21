using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSpawner : MonoBehaviour
{
    public List<GameObject> activeTargetObjects; // 액티브 그룹에 이미지를 추가할 객체 목록
    public List<GameObject> passiveTargetObjects;// 패시브 그룹에 이미지를 추가할 객체 목록
    private int activeIndex = 0; // 액티브 그룹에서 이미지를 추가할 현재 객체 인덱스
    private int passiveIndex = 0; // 패시브 그룹에서 이미지를 추가할 현재 객체 인덱스
    private Dictionary<GameObject, GameObject> buttonToTargetObject = new Dictionary<GameObject, GameObject>(); // 버튼과 연관된 대상 객체를 저장하는 딕셔너리

    // 액티브 그룹 버튼을 누를 때 호출되는 메서드
    public void SpawnActiveImage(GameObject button)
    {
        Debug.Log("Button clicked: " + button.name);

        SpawnImage(button, true);
    }

    // 패시브 그룹 버튼을 누를 때 호출되는 메서드
    public void SpawnPassiveImage(GameObject button)
    {
        Debug.Log("Button clicked: " + button.name);

        SpawnImage(button, false);
    }

    // 이미지를 추가하거나 텍스트를 변경하는 메서드
    private void SpawnImage(GameObject button, bool isActive)
    {
        List<GameObject> targetObjects = isActive ? activeTargetObjects : passiveTargetObjects;

        if (buttonToTargetObject.ContainsKey(button))
        {
            GameObject targetObject = buttonToTargetObject[button];
            Text[] childTexts = targetObject.GetComponentsInChildren<Text>();
            Text secondChildText = null;
            foreach (Text childText in childTexts)
            {
                if (childText.text != "Active" && childText.text != "Passive")
                {
                    secondChildText = childText;
                    break;
                }
            }

            if (secondChildText != null)
            {
                int currentCount = int.Parse(secondChildText.text);
                currentCount++;
                secondChildText.text = currentCount.ToString();
            }
            else
            {
                Debug.LogWarning("The target object has no child text objects.");
            }
        }
        else
        {
            int currentIndex = isActive ? activeIndex : passiveIndex;

            if (currentIndex >= targetObjects.Count)
            {
                return;
            }

            Image buttonImage = button.GetComponentInChildren<Image>();

            if (buttonImage != null)
            {
                GameObject targetObject = targetObjects[currentIndex];
                GameObject newImageObject = new GameObject("SpawnedImage");
                Image newImage = newImageObject.AddComponent<Image>();
                newImage.sprite = buttonImage.sprite;
                newImageObject.transform.SetParent(targetObject.transform, false);
                newImageObject.transform.localPosition = Vector3.zero;

                Text firstChildText = targetObject.GetComponentInChildren<Text>();
                if (firstChildText != null)
                {
                    firstChildText.text = "1";
                }
                else
                {
                    Debug.LogWarning("The target object has no child text objects.");
                }

                if (isActive)
                {
                    activeIndex++;
                }
                else
                {
                    passiveIndex++;
                }

                buttonToTargetObject[button] = targetObject;
            }
            else
            {
                Debug.LogWarning("Button has no child Image object.");
            }
        }
    }
}