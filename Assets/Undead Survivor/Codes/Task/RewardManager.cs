using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    DailyTaskManager dailyTaskManager;
    public Slider rewardSlider;
    public Button[] rewardButtons;
    public float[] rewardThresholds;

    private void Awake()
    {
        InitializeRewardSlider();
        dailyTaskManager = GetComponent<DailyTaskManager>();
    }

    private void Update()
    {
        dailyTaskManager.ResetPointsIfNeeded();
        UpdateRewardSlider();
        UpdateRewardButtons();
    }

    private void InitializeRewardSlider()
    {
        rewardSlider.minValue = 0;
        rewardSlider.maxValue = 1;
        rewardSlider.value = 0;
    }

    private void UpdateRewardSlider()
    {
        float sliderValue = (float)dailyTaskManager.dailyPoints / 100f;
        rewardSlider.value = sliderValue;
    }

    private void UpdateRewardButtons()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            if (rewardSlider.value >= rewardThresholds[i] && PlayerPrefs.GetInt($"RewardButton_{i}_Claimed", 0) == 0)
            {
                rewardButtons[i].interactable = true;
            }
            else
            {
                rewardButtons[i].interactable = false;
            }
        }
    }

    public void ClaimReward(int index)
    {
        // 보상을 받고 버튼 비활성화
        PlayerPrefs.SetInt($"RewardButton_{index}_Claimed", 1);
        rewardButtons[index].interactable = false;

        // 보상 처리 (여기에 보상 관련 코드 추가)
    }


}
