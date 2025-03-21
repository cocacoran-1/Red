using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

[Serializable]
public class EnergyData
{
    public int energy;
    public string lastUpdateTime;
}

public class EnergySystem : MonoBehaviour
{
    public Player_Stat Player;
    public TMP_Text energyText;
    public TMP_Text energyRechargeText;
    private DateTime lastUpdateTime;

  

    void Start()
    {
        LoadEnergy();
        UpdateEnergy();
    }

    void Update()
    {
        UpdateEnergy();
        UpdateEnergyRechargeText();
    }

    private void LoadEnergy()
    {
        string path = Application.persistentDataPath + "/energyData.json";
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            EnergyData loadedData = JsonUtility.FromJson<EnergyData>(jsonString);
            Player.energy = loadedData.energy;
            lastUpdateTime = DateTime.Parse(loadedData.lastUpdateTime);
        }
        else
        {
            Player.energy = Player.Max_energy;
            DateTime currentTime = DateTime.Now;
            lastUpdateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, 0, 0); // 정각을 기준으로 설정
            SaveEnergy();
        }
    }

    private void OnApplicationQuit()
    {
        SaveEnergy();
    }

    public void SaveEnergy()
    {
        EnergyData energyData = new EnergyData
        {
            energy = Player.energy,
            lastUpdateTime = lastUpdateTime.ToString()
        };

        string jsonString = JsonUtility.ToJson(energyData);
        File.WriteAllText(Application.persistentDataPath + "/energyData.json", jsonString);
    }

    private void UpdateEnergy()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan timePassed = currentTime - lastUpdateTime;
        int energyToAdd = (int)(timePassed.TotalMinutes / 10); // 시간 간격을 10분으로 변경

        if (energyToAdd > 0)
        {
            int num = Player.energy + energyToAdd;
            if(Player.energy>=Player.Max_energy)
            { }
            else if (num<Player.Max_energy)
            {
                Player.energy += energyToAdd;
            }
            else
            {
                Player.energy=Player.Max_energy;
            }
            lastUpdateTime = lastUpdateTime.AddMinutes(energyToAdd*10); // 지난 갱신 시간에 경과된 시간만큼 더합니다.
            SaveEnergy();
        }
        energyText.text = Player.energy.ToString() + "/" + Player.Max_energy.ToString();
    }

    private void UpdateEnergyRechargeText()
    {
        if (Player.energy < Player.Max_energy)
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan timeToNextCharge = lastUpdateTime.AddMinutes(10) - currentTime; // 시간 간격을 1분으로 변경
            energyRechargeText.text = timeToNextCharge.ToString(@"mm\:ss");
        }
        else
        {
            energyRechargeText.text = "";
        }
    }
}
