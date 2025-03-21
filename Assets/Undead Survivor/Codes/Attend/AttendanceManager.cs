using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AttendanceManager : MonoBehaviour
{
    public List<AttendanceData> attendanceRewards;
    public GameObject[] rewardButtons;
    public GameObject[] ClearImage;
    private int currentDay;
    public int resetDays;

    private string savePath;
    public Player_Status status;
    private void Awake()
    {
        savePath = Application.persistentDataPath + "/attendanceData.dat";
    }

    private void Start()
    {
        AttendanceSaveData data = LoadData();

        // 오늘 날짜와 마지막 출석체크 날짜를 확인
        DateTime lastCheckTime = DateTime.FromBinary(LoadData().lastCheckTime);
        DateTime currentDate = DateTime.Now;

        // 하루가 지났으면 출석체크 날짜 증가
        if (currentDate.Date != lastCheckTime.Date && (currentDate - lastCheckTime).TotalDays >= 1)
        {
            currentDay++;
            SaveData(currentDate.ToBinary());
        }

        // 일정 일수가 지났으면 초기화
        if (currentDay >= resetDays)
        {
            currentDay = 0;
            ResetAllClaimedStatus();
            PlayerPrefs.SetInt("CurrentDay", currentDay);
        }

        UpdateButtonStates();
    }

    private void ResetAllClaimedStatus()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            PlayerPrefs.SetInt("Button_" + i + "_Claimed", 0);
            attendanceRewards[i].isClaimed = false;
        }
    }


    // 버튼 클릭 기능 설정
    private void UpdateButtonStates()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            bool buttonInteractable = (i <= currentDay);
            bool buttonClaimed = PlayerPrefs.GetInt("Button_" + i + "_Claimed", 0) == 1;

            if (!buttonClaimed)
            {
                rewardButtons[i].GetComponent<Button>().interactable = buttonInteractable;
                ClearImage[i].SetActive(false);
                attendanceRewards[i].isClaimed = false;
            }
            else
            {
                rewardButtons[i].GetComponent<Button>().interactable = false;
                ClearImage[i].SetActive(true);
                attendanceRewards[i].isClaimed = true;
            }
        }
    }

    public void ResetDateForTesting()
    {
        currentDay = 0;
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            PlayerPrefs.SetInt("Button_" + i + "_Claimed", 0);
        }
        UpdateButtonStates();
    }

    private void SaveData(long lastCheckTime)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);

        AttendanceSaveData data = new AttendanceSaveData
        {
            currentDay = currentDay,
            lastCheckTime = lastCheckTime
        };

        bf.Serialize(file, data);
        file.Close();
    }

    private AttendanceSaveData LoadData()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            AttendanceSaveData data = (AttendanceSaveData)bf.Deserialize(file);
            file.Close();

            currentDay = data.currentDay;
            return data;
        }
        else
        {
            SaveData(DateTime.Now.ToBinary());
            return LoadData();
        }
    }

    public void ClaimReward(int day)
    {
        // 보상 처리를 여기서 수행합니다.
        status.Daily.Attendance_Count++;
        attendanceRewards[day].isClaimed = true;
        PlayerPrefs.SetInt("Button_" + day + "_Claimed", 1); // 버튼이 클릭되었음을 저장합니다.
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        UpdateButtonStates();
    }


    [System.Serializable]
    private class AttendanceSaveData
    {
        public int currentDay;
        public long lastCheckTime;
    }

    public void SkipDay()
    {
        currentDay++;
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        // 일정 일수가 지났으면 초기화
        if (currentDay >= resetDays)
        {
            currentDay = 0;
            ResetAllClaimedStatus();
            PlayerPrefs.SetInt("CurrentDay", currentDay);
        }

        UpdateButtonStates();
    }


}

[System.Serializable]
public class AttendanceData
{
    public string day;
    public string reward;
    public bool isClaimed;
}
