using System;
using UnityEngine;

public class DailyResetManager : MonoBehaviour
{
    private const string LastSavedDateKey = "LastSavedDate";

    public Daily_history Daily;

    private void Start()
    {
        if (IsNewDay())
        {
            ResetData();
            SaveTodayDate();
        }
    }

    private bool IsNewDay()
    {
        if (!PlayerPrefs.HasKey(LastSavedDateKey))
        {
            return true;
        }

        DateTime lastSavedDate = DateTime.Parse(PlayerPrefs.GetString(LastSavedDateKey));
        DateTime today = DateTime.Today;

        return lastSavedDate.Date != today.Date;
    }

    private void ResetData()
    {
        Daily.Monster_Kill = 0;
        Daily.Boss_Kill = 0;
        Daily.Elite_Kill = 0;
        Daily.Advertisement_Count = 0;
        Daily.Cash_Gacha_Count = 0;
        Daily.Gacha_Count= 0;
        Daily.Attendance_Count = 0;
        Daily.Game_Start_Count = 0;
        Daily.Reinforcement_Count = 0;
        Daily.Payment_Count = 0;
    }

    private void SaveTodayDate()
    {
        DateTime today = DateTime.Today;
        PlayerPrefs.SetString(LastSavedDateKey, today.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();
    }

   
}