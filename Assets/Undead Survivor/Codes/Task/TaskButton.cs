using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton : MonoBehaviour
{
    public DailyTaskManager dailyTaskManager;
    public int points;
    public Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CompleteTask);

        // 이전에 버튼이 눌렸었다면 비활성화
        if (PlayerPrefs.GetInt(gameObject.name + "_isCompleted") == 1)
        {
            button.interactable = false;
        }
    }

    public void CompleteTask()
    {
        
        dailyTaskManager.AddPoints(points);

        // 버튼을 비활성화하고 상태 저장
        button.interactable = false;
        PlayerPrefs.SetInt(gameObject.name + "_isCompleted", 1);
    }
}

