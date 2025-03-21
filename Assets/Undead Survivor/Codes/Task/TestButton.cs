using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    public DailyTaskManager dailyTaskManager;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ResetDailyTasks);
    }

    public void ResetDailyTasks()
    {
        dailyTaskManager.ResetPoints();
    }
}
