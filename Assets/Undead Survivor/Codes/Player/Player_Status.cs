using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status : MonoBehaviour
{
    public Player_Stat Player;
    public InventoryObject Equipment;
    public InventoryObject Inventory;
    public Player_history PlayerHistory;
    public Daily_history Daily;
    public static Player_Status instance = null;
    public EquipmentData Item;



    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }



    }

    private void OnEnable()
    {
        for (int i = 0; i < Equipment.Container.Count; ++i)
        {
            Player.Damage += Equipment.Container[i].Equipment.Damage;
            Player.AttackSpeed += Equipment.Container[i].Equipment.AttackSpeed;
            Player.Attack_Range += Equipment.Container[i].Equipment.Attack_Range;
            Player.Attack_Duration += Equipment.Container[i].Equipment.Attack_Duration;
            Player.Max_Hp += Equipment.Container[i].Equipment.Max_Hp;
            Player.Defense += Equipment.Container[i].Equipment.Defense;
            Player.Hp_Regen += Equipment.Container[i].Equipment.Hp_Regen;
            Player.Speed += Equipment.Container[i].Equipment.Speed;
            Player.Magnet_Range += Equipment.Container[i].Equipment.Magnet_Range;
            Player.Exp_Up += Equipment.Container[i].Equipment.Exp_Up;
            Player.Gold_Up += Equipment.Container[i].Equipment.Gold_Up;

        }
    }
    private void OnApplicationQuit()
    {
        for (int i = 0; i < Equipment.Container.Count; ++i)
        {
            Player.Damage -= Equipment.Container[i].Equipment.Damage;
            Player.AttackSpeed -= Equipment.Container[i].Equipment.AttackSpeed;
            Player.Attack_Range -= Equipment.Container[i].Equipment.Attack_Range;
            Player.Attack_Duration -= Equipment.Container[i].Equipment.Attack_Duration;
            Player.Max_Hp -= Equipment.Container[i].Equipment.Max_Hp;
            Player.Defense -= Equipment.Container[i].Equipment.Defense;
            Player.Hp_Regen -= Equipment.Container[i].Equipment.Hp_Regen;
            Player.Speed -= Equipment.Container[i].Equipment.Speed;
            Player.Magnet_Range -= Equipment.Container[i].Equipment.Magnet_Range;
            Player.Exp_Up -= Equipment.Container[i].Equipment.Exp_Up;
            Player.Gold_Up -= Equipment.Container[i].Equipment.Gold_Up;
        }

    }

    public void GetSpecialStat()
    {
        for (int i = 0; i < Item.specialStat.Length; i++)
        {
            if (Item.specialStat != null) //B~SS등급
            {
                if (Item.Upgrade_Level == 10)
                {
                    if (Item.grade == ItemGrade.B || Item.grade == ItemGrade.A || Item.grade == ItemGrade.S || Item.grade == ItemGrade.SS)
                    {
                        //특수능력 1번째줄 해금

                    }
                }
                else if (Item.Upgrade_Level == 20)
                {
                    if (Item.grade == ItemGrade.A || Item.grade == ItemGrade.S || Item.grade == ItemGrade.SS)
                    {
                        //특수능력 2번째줄 해금

                    }
                }
                else if (Item.Upgrade_Level == 30)
                {
                    if (Item.grade == ItemGrade.S || Item.grade == ItemGrade.SS)
                    {
                        //특수능력 3번째줄 해금

                    }
                }
                else if (Item.Upgrade_Level == 40)
                {
                    if (Item.grade == ItemGrade.SS)
                    {
                        //특수능력 4번째줄 해금

                    }
                }
                else
                {

                }



            }
        }

    }

    


}
