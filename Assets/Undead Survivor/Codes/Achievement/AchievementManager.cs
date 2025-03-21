using UnityEngine;
using UnityEngine.UI;
public class AchievementManager : MonoBehaviour
{
    public Player_history player;
    [Header("몬스터 처치")]
    public GameObject Monster_Kill;
    public Button Monster_Kill_Btn;
    public Slider Monster_Kill_Slider;
    public Text Monster_Kill_Text;
    [Header("엘리트 몬스터 처치")]
    public GameObject Elite_Kill;
    public Button Elite_Kill_Btn;
    public Slider Elite_Kill_Slider;
    public Text Elite_Kill_Text;
    [Header("보스 처치")]
    public GameObject Boss_Kill;
    public Button Boss_Kill_Btn;
    public Slider Boss_Kill_Slider;
    public Text Boss_Kill_Text;
    [Header("강화 횟수")]
    public GameObject Reinforcement_Count;
    public Button Reinforcement_Count_Btn;
    public Slider Reinforcement_Count_Slider;
    public Text Reinforcement_Count_Text;
    [Header("유료 뽑기")]
    public GameObject Gacha_Count;
    public Button Gacha_Count_Btn;
    public Slider Gacha_Count_Slider;
    public Text Gacha_Count_Text;
    [Header("무기 획득")]
    public GameObject Weapon_Count;
    public Button Weapon_Count_Btn;
    public Slider Weapon_Count_Slider;
    public Text Weapon_Count_Text;
    [Header("방어구 획득")]
    public GameObject Defense_Count;
    public Button Defense_Count_Btn;
    public Slider Defense_Count_Slider;
    public Text Defense_Count_Text;
    [Header("골드 획득")]
    public GameObject Gold_Count;
    public Button Gold_Count_Btn;
    public Slider Gold_Count_Slider;
    public Text Gold_Count_Text;
    [Header("크리스탈 소모")]
    public GameObject Cash_Count;
    public Button Cash_Count_Btn;
    public Slider Cash_Count_Slider;
    public Text Cash_Count_Text;

    public void All_Change()
    {
        Monster_Kill_Change();
        Elite_Kill_Change();
        Boss_Kill_Change();
        Reinforcement_Count_Change();
        Gacha_Count_Change();
        Weapon_Count_Change();
        Defense_Count_Change();
        Gold_Count_Change();
        Cash_Count_Change();    
    }
    public void Monster_Kill_Change()
    {
        if (player.Monster_Kill < player.Monster_Kill_Goal)
        {
            Monster_Kill_Btn.interactable = false;
        }
        else
        {
            Monster_Kill_Btn.interactable = true;
        }
        Monster_Kill_Text.text =player.Monster_Kill + "/" + player.Monster_Kill_Goal;
        Monster_Kill_Slider.value=(float)player.Monster_Kill/ (float)player.Monster_Kill_Goal;
    }
    public void Elite_Kill_Change()
    {
        if (player.Elite_Kill < player.Elite_Kill_Goal)
        {
            Elite_Kill_Btn.interactable = false;
        }
        else
        {
            Elite_Kill_Btn.interactable = true;
        }
        Elite_Kill_Slider.value = (float)player.Elite_Kill / (float)player.Elite_Kill_Goal;
        Elite_Kill_Text.text = player.Elite_Kill + "/" +player.Elite_Kill_Goal;
    }
    public void Boss_Kill_Change()
    {
        if (player.Boss_Kill < player.Boss_Kill_Goal)
        {
            Boss_Kill_Btn.interactable = false;
        }
        else
        {
            Boss_Kill_Btn.interactable = true;
        }
        Boss_Kill_Slider.value = (float)player.Boss_Kill / (float)player.Boss_Kill_Goal;
        Boss_Kill_Text.text = player.Boss_Kill + "/" + player.Boss_Kill_Goal;
    }
    public void Reinforcement_Count_Change()
    {
        if (player.Reinforcement_Count < player.Reinforcement_Count_Goal)
        {
            Reinforcement_Count_Btn.interactable = false;
        }
        else
        {
            Reinforcement_Count_Btn.interactable = true;
        }
        Reinforcement_Count_Slider.value = (float)player.Reinforcement_Count / (float)player.Reinforcement_Count_Goal;
        Reinforcement_Count_Text.text =player.Reinforcement_Count + "/" + player.Reinforcement_Count_Goal;
    }
    public void Gacha_Count_Change()
    {
        if (player.Gacha_Count < player.Gacha_Count_Goal)
        {
            Gacha_Count_Btn.interactable = false;
        }
        else
        {
            Gacha_Count_Btn.interactable = true;
        }
        Gacha_Count_Slider.value = (float)player.Gacha_Count / (float)player.Gacha_Count_Goal;
        Gacha_Count_Text.text =player.Gacha_Count + "/" + player.Gacha_Count_Goal;
    }
    public void Weapon_Count_Change()
    {
        if (player.Weapon_Count < player.Weapon_Count_Goal)
        {
            Weapon_Count_Btn.interactable = false;
        }
        else
        {
            Weapon_Count_Btn.interactable = true;
        }
        Weapon_Count_Slider.value = (float)player.Weapon_Count / (float)player.Weapon_Count_Goal;
        Weapon_Count_Text.text =player.Weapon_Count + "/" + player.Weapon_Count_Goal;
    }
    public void Defense_Count_Change()
    {
        if (player.Defense_Count < player.Defense_Count_Goal)
        {
            Defense_Count_Btn.interactable = false;
        }
        else
        {
            Defense_Count_Btn.interactable = true;
        }
        Defense_Count_Slider.value = (float)player.Defense_Count / (float)player.Defense_Count_Goal;
        Defense_Count_Text.text =player.Defense_Count + "/" + player.Defense_Count_Goal;
    }
    public void Gold_Count_Change()
    {
        if (player.Gold_Count < player.Gold_Count_Goal)
        {
            Gold_Count_Btn.interactable = false;
        }
        else
        {
            Gold_Count_Btn.interactable = true;
        }
        Gold_Count_Slider.value = (float)player.Gold_Count / (float)player.Gold_Count_Goal;
        Gold_Count_Text.text =player.Gold_Count + "/" + player.Gold_Count_Goal;
    }
    public void Cash_Count_Change()
    {
        if (player.Cash_Count < player.Cash_Count_Goal)
        {
            Cash_Count_Btn.interactable = false;
        }
        else
        {
            Cash_Count_Btn.interactable = true;
        }
        Cash_Count_Slider.value = (float)player.Cash_Count / (float)player.Cash_Count_Goal;
        Cash_Count_Text.text =player.Cash_Count + "/" + player.Cash_Count_Goal;
    }
    public void Monster_Kill_Click()// 초기값 10000
    {
        player.Monster_Kill_Goal += 10000;
        //보상 받을꺼 여기에 작성
        Monster_Kill_Change();
    }
    public void Elite_Kill_Click()//초기값 2
    {
        if (player.Elite_Kill_Goal == 2)
        {
            player.Elite_Kill_Goal = 20;
        }
        else
        {
            player.Elite_Kill_Goal += 20;
        }
        //보상 받을꺼 여기에 작성
        Elite_Kill_Change();
    }
    public void Boss_Kill_Click()//초기값 1
    {
        if (player.Boss_Kill_Goal == 1)
        {
            player.Boss_Kill_Goal = 10;
        }
        else
        {
            player.Boss_Kill_Goal += 10;
        }
        //보상 받을꺼 여기에 작성
        Boss_Kill_Change();
    }
    public void Reinforcement_Count_Click()//초기값 1
    {
        if (player.Reinforcement_Count_Goal == 1)
        {
            player.Reinforcement_Count_Goal = 10;
        }
        else if (player.Reinforcement_Count_Goal == 10)
        {
            player.Reinforcement_Count_Goal = 50;
        }
        else
        {
            player.Reinforcement_Count_Goal += 50;
        }
        //보상 받을꺼 여기에 작성
        Reinforcement_Count_Change();
    }
    public void Gacha_Count_Click()//초기값 10
    {

        player.Gacha_Count_Goal += 20;

        //보상 받을꺼 여기에 작성
        Gacha_Count_Change();
    }
    public void Weapon_Count_Click()//초기값 5
    {

        player.Weapon_Count_Goal += 5;

        //보상 받을꺼 여기에 작성
        Weapon_Count_Change();
    }
    public void Defense_Count_Click()//초기값 10
    {

        player.Defense_Count_Goal += 10;

        //보상 받을꺼 여기에 작성
        Defense_Count_Change();
    }
    public void Gold_Count_Click()//초기값 1000
    {

        player.Gold_Count_Goal += 1000;

        //보상 받을꺼 여기에 작성
        Gold_Count_Change();
    }
    public void Cash_Count_Click()//초기값 1000
    {

        player.Cash_Count_Goal += 1000;

        //보상 받을꺼 여기에 작성
        Cash_Count_Change();
    }




}



