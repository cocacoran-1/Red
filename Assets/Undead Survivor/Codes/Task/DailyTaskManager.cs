using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;
public class DailyTaskManager : MonoBehaviour
{
    public int dailyPoints;
    public TaskButton[] taskButtons;
    private DateTime lastResetTime;
    public Player_Status player;

    [Header("슬라이더,텍스트")]
    public Button Monster_Kill_Btn;
    public Text Monster_Kill_Text;
    public Slider Monster_Kill_Slider;

    public Button Elite_Kill_Btn;
    public Text Elite_Kill_Text;
    public Slider Elite_Kill_Slider;

    public Button Boss_Kill_Btn;
    public Text Boss_Kill_Text;
    public Slider Boss_Kill_Slider;

    public Button Reinforcement_Count_Btn;
    public Text Reinforcement_Count_Text;
    public Slider Reinforcement_Count_Slider;

    public Button Gacha_Count_Btn;
    public Text Gacha_Count_Text;
    public Slider Gacha_Count_Slider;

    public Button Game_Start_Count_Btn;
    public Text Game_Start_Count_Text;
    public Slider Game_Start_Count_Slider;

    public Button Attendance_Count_Btn;
    public Text Attendance_Count_Text;
    public Slider Attendance_Count_Slider;

    public Button Payment_Count_Btn;
    public Text Payment_Count_Text;
    public Slider Payment_Count_Slider;

    public Button Advertisement_Count_Btn;
    public Text Advertisement_Count_Text;
    public Slider Advertisement_Count_Slider;

    public Button Cash_Gacha_Count_Btn;
    public Text Cash_Gacha_Count_Text;
    public Slider Cash_Gacha_Count_Slider;




    private void Start()
    {
        dailyPoints = PlayerPrefs.GetInt("DailyPoints", 0);
        long lastResetTimeBinary = PlayerPrefs.GetInt("LastResetTimeLow", 0) != 0 || PlayerPrefs.GetInt("LastResetTimeHigh", 0) != 0 ? Combine(PlayerPrefs.GetInt("LastResetTimeLow"), PlayerPrefs.GetInt("LastResetTimeHigh")) : DateTime.Now.ToBinary();
        lastResetTime = DateTime.FromBinary(lastResetTimeBinary);
        ResetPointsIfNeeded();
    }
    public void All_Change()
    {
        Monster_Kill_Change();
        Elite_Kill_Change();
        Boss_Kill_Change();
        Reinforcement_Count_Change();
        Gacha_Count_Change();
        Game_Start_Count_Change();
        Attendance_Count_Change();
        Payment_Count_Change();
        Advertisement_Count_Change();
        Cash_Gacha_Count_Change();
        for(int i=0;i<10;i++)
        {
            if(PlayerPrefs.GetInt(taskButtons[i].gameObject.name+ "_isCompleted") == 1)
            {

                taskButtons[i].button.interactable = false;
            }
        }
    }
    public void AddPoints(int points)
    {
        dailyPoints += points;
        dailyPoints = Mathf.Clamp(dailyPoints, 0, 100);
        PlayerPrefs.SetInt("DailyPoints", dailyPoints);
    }

    public void ResetPointsIfNeeded()
    {
        if (DateTime.Now.Date != lastResetTime.Date)
        {
            ResetPoints();
            lastResetTime = DateTime.Now;
            PlayerPrefs.SetInt("LastResetTimeLow", Split(lastResetTime.ToBinary(), true));
            PlayerPrefs.SetInt("LastResetTimeHigh", Split(lastResetTime.ToBinary(), false));
        }
    }
    public void ResetPoints()
    {
        dailyPoints = 0;
        PlayerPrefs.SetInt("DailyPoints", dailyPoints);

        // 모든 버튼을 다시 활성화하고 상태 저장
        foreach (TaskButton taskButton in taskButtons)
        {
            taskButton.GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt(taskButton.gameObject.name + "_isCompleted", 0);
            for (int i = 0; i < taskButtons.Length; i++)
            {
                PlayerPrefs.SetInt($"RewardButton_{i}_Claimed", 0);
            }
        }
    }

    private int Split(long value, bool low)
    {
        if (low)
        {
            return (int)(value & 0xFFFFFFFF);
        }
        else
        {
            return (int)(value >> 32);
        }
    }

    private long Combine(int lowBits, int highBits)
    {
        return ((long)highBits << 32) | (uint)lowBits;
    }

    public void Monster_Kill_Change()
    {
        if (player.Daily.Monster_Kill >= 1000)
        {
            Monster_Kill_Btn.interactable = true;
        }
        else
        {
            Monster_Kill_Btn.interactable = false;
        }
        Monster_Kill_Slider.value=(float)player.Daily.Monster_Kill/1000;
        Monster_Kill_Text.text=player.Daily.Monster_Kill+"/"+"1000";
    }
    public void Elite_Kill_Change()
    {
        if (player.Daily.Elite_Kill >= 4)
        {
            Elite_Kill_Btn.interactable = true;
        }
        else
        {
            Elite_Kill_Btn.interactable = false;
        }
        Elite_Kill_Slider.value = (float)player.Daily.Elite_Kill / 4;
        Elite_Kill_Text.text = player.Daily.Elite_Kill + "/" + "4";
    }
    public void Boss_Kill_Change()
    {
        if (player.Daily.Boss_Kill >= 2)
        {
            Boss_Kill_Btn.interactable = true;
        }
        else
        {
            Boss_Kill_Btn.interactable = false;
        }
        Boss_Kill_Slider.value = (float)player.Daily.Boss_Kill / 2;
        Boss_Kill_Text.text = player.Daily.Boss_Kill + "/" + "2";
    }
    public void Reinforcement_Count_Change()
    {
        if (player.Daily.Reinforcement_Count >= 1)
        {
            Reinforcement_Count_Btn.interactable = true;
        }
        else
        {
            Reinforcement_Count_Btn.interactable = false;
        }
        Reinforcement_Count_Slider.value = (float)player.Daily.Reinforcement_Count / 1;
        Reinforcement_Count_Text.text = player.Daily.Reinforcement_Count + "/" + "1";
    }
    public void Gacha_Count_Change()
    {
        if (player.Daily.Gacha_Count >= 1)
        {
            Gacha_Count_Btn.interactable = true;
        }
        else
        {
            Gacha_Count_Btn.interactable = false;
        }
        Gacha_Count_Slider.value = (float)player.Daily.Gacha_Count / 1;
        Gacha_Count_Text.text = player.Daily.Gacha_Count + "/" + "1";
    }
    public void Game_Start_Count_Change()
    {
        if (player.Daily.Game_Start_Count >= 1)
        {
            Game_Start_Count_Btn.interactable = true;
        }
        else
        {
            Game_Start_Count_Btn.interactable = false;
        }
        Game_Start_Count_Slider.value = (float)player.Daily.Game_Start_Count / 1;
        Game_Start_Count_Text.text = player.Daily.Game_Start_Count + "/" + "1";
    }
    public void Attendance_Count_Change()
    {
        if (player.Daily.Attendance_Count >= 1)
        {
            Attendance_Count_Btn.interactable = true;
        }
        else
        {
            Attendance_Count_Btn.interactable = false;
        }
        Attendance_Count_Slider.value = (float)player.Daily.Attendance_Count / 1;
        Attendance_Count_Text.text = player.Daily.Attendance_Count + "/" + "1";
    }
    public void Payment_Count_Change()
    {
        if (player.Daily.Payment_Count >= 1)
        {
            Payment_Count_Btn.interactable = true;
        }
        else
        {
            Payment_Count_Btn.interactable = false;
        }
        Payment_Count_Slider.value = (float)player.Daily.Payment_Count / 1;
        Payment_Count_Text.text = player.Daily.Payment_Count + "/" + "1";
    }
    public void Advertisement_Count_Change()
    {
        if (player.Daily.Advertisement_Count >= 1)
        {
            Advertisement_Count_Btn.interactable = true;
        }
        else
        {
            Advertisement_Count_Btn.interactable = false;
        }
        Advertisement_Count_Slider.value = (float)player.Daily.Advertisement_Count / 1;
        Advertisement_Count_Text.text = player.Daily.Advertisement_Count + "/" + "1";
    }
    public void Cash_Gacha_Count_Change()
    {
        if (player.Daily.Cash_Gacha_Count >= 1)
        {
            Cash_Gacha_Count_Btn.interactable = true;
        }
        else
        {
            Cash_Gacha_Count_Btn.interactable = false;
        }
        Cash_Gacha_Count_Slider.value = (float)player.Daily.Cash_Gacha_Count / 1;
        Cash_Gacha_Count_Text.text = player.Daily.Cash_Gacha_Count + "/" + "1";
    }

    
}
