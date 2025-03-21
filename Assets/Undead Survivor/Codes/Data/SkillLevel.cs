using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class SkillLevel : MonoBehaviour
{
    public SkillData data;

    private SkillData lastClickedData;

    public int level = 0;
    public Weapon weapon;

    Image icon;
    Text textLevel;
    public Text textName;
    public Text textDesc;
    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.SkillICon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];

        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.skillName;
    }
    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);
        switch (data.skillType)
        {
            case SkillData.SkillType.Fire_Ball:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                    case 4:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddSpeedDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Fire_Zone:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Fire_Thrower:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                        textDesc.text = string.Format(data.AddTimeDesc);
                        break;
                    case 4:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddSpeedDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Fire_Pillar:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 6:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                    case 4:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Wind_Blade:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Wind_Hole:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddSpeedDesc);
                        break;

                }
                break;
            case SkillData.SkillType.Wind_Tornado:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                }
                break;

            case SkillData.SkillType.Wind_wave:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 5:
                    case 2:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                    case 4:
                        textDesc.text = string.Format(data.AddSpeedDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc) + "\n" + data.DamageDesc;
                        break;
                }
                break;

            case SkillData.SkillType.Ice_Ball:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;

            case SkillData.SkillType.Ice_Mine:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:

                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 5:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;

            case SkillData.SkillType.Ice_Shot:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                    case 4:
                        textDesc.text = string.Format(data.AddCloneDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc) + "\n" + data.AddCloneDesc;
                        break;
                }
                break;

            case SkillData.SkillType.Ice_Spear:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                }
                break;

            case SkillData.SkillType.Poison_Bomb:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                }
                break;

            case SkillData.SkillType.Poison_Nova:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Poison_Skull:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Poison_Swamp:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Earth_Golem:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc) + "\n" + data.AddCloneDesc;
                        break;
                }
                break;
            case SkillData.SkillType.Earth_Shield:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.AddDefenseDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddSpeedDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Earth_stone:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Earth_ThrowRock:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Lightning_Beam:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:

                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;

                    case 3:
                    case 4:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                    case 2:
                    case 6:
                        textDesc.text = string.Format(data.AddSpeedDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Forked_Lightning:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Lightning_Thunder:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                    case 6:
                        textDesc.text = string.Format(data.AddRangeDesc);
                        break;
                }
                break;
            case SkillData.SkillType.Chain_Lightning:
                switch (level)
                {
                    case 0:
                        textDesc.text = string.Format("New !");
                        break;
                    case 1:
                    case 3:
                    case 5:
                        textDesc.text = string.Format(data.DamageDesc);
                        break;
                    case 2:
                    case 4:
                        textDesc.text = string.Format(data.AddBulletDesc);
                        break;
                    case 6:
                        textDesc.text = string.Format(data.AddBulletDesc) + "\n" + data.DamageDesc;
                        break;
                }
                break;


            // 패시브
            case SkillData.SkillType.Attack_Duration:
            case SkillData.SkillType.Attack_Range:
            case SkillData.SkillType.Attack_Speed:
            case SkillData.SkillType.Magnet_Range:
            case SkillData.SkillType.Damage:
            case SkillData.SkillType.Defense:
            case SkillData.SkillType.Speed:
            case SkillData.SkillType.Hp_Regen:
            case SkillData.SkillType.Max_Hp:
            case SkillData.SkillType.Exp_Up:
            case SkillData.SkillType.Gold_Up:
            case SkillData.SkillType.Hp:
            case SkillData.SkillType.Coin:
                textDesc.text = string.Format(data.ItemDesc);
                break;
            default:
                textDesc.text = string.Format(data.ItemDesc);
                break;
        }

    }
    public void OnClick()
    {

        switch (data.skillType)
        {
            //액티브
            case SkillData.SkillType.Fire_Ball:
                break;
            case SkillData.SkillType.Fire_Zone:
                break;
            case SkillData.SkillType.Fire_Thrower:
                break;
            case SkillData.SkillType.Fire_Pillar:
                break;
            case SkillData.SkillType.Wind_Blade:
                break;
            case SkillData.SkillType.Wind_Hole:
                break;
            case SkillData.SkillType.Wind_Tornado:
                break;
            case SkillData.SkillType.Wind_wave:
                break;
            case SkillData.SkillType.Ice_Ball:
                break;
            case SkillData.SkillType.Ice_Mine:
                break;
            case SkillData.SkillType.Ice_Shot:
                break;
            case SkillData.SkillType.Ice_Spear:
                break;
            case SkillData.SkillType.Poison_Bomb:
                break;
            case SkillData.SkillType.Poison_Nova:
                break;
            case SkillData.SkillType.Poison_Skull:
                break;
            case SkillData.SkillType.Poison_Swamp:
                break;
            case SkillData.SkillType.Earth_Golem:
                break;
            case SkillData.SkillType.Earth_Shield:
                break;
            case SkillData.SkillType.Earth_stone:
                break;
            case SkillData.SkillType.Earth_ThrowRock:
                break;
            case SkillData.SkillType.Lightning_Beam:
                break;
            case SkillData.SkillType.Forked_Lightning:
                break;
            case SkillData.SkillType.Lightning_Thunder:
                break;
            case SkillData.SkillType.Chain_Lightning:
                break;

            // 패시브
            case SkillData.SkillType.Attack_Duration:
                break;
            case SkillData.SkillType.Attack_Range:
                break;
            case SkillData.SkillType.Attack_Speed:
                break;
            case SkillData.SkillType.Magnet_Range:
                break;
            case SkillData.SkillType.Damage:
                break;
            case SkillData.SkillType.Defense:
                break;
            case SkillData.SkillType.Speed:
                break;
            case SkillData.SkillType.Hp_Regen:
                break;
            case SkillData.SkillType.Max_Hp:
                break;
            case SkillData.SkillType.Exp_Up:
                break;
            case SkillData.SkillType.Gold_Up:
                break;

            // 아이템
            case SkillData.SkillType.Hp:
                break;
            case SkillData.SkillType.Coin:
                break;
        }
        level++;
    }
}
