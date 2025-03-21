using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New history", menuName = "history/history")]
public class Player_history : ScriptableObject
{
    [Header("몬스터 처치")]
    public int Monster_Kill;
    public int Elite_Kill;
    public int Boss_Kill;

    [Header("메인 메뉴 관련")]
    public int Reinforcement_Count;
    public int Gacha_Count;
    public int Weapon_Count;
    public int Defense_Count;
    public int Gold_Count;
    public int Cash_Count;

    [Header("달성률")]
    public int Reinforcement_Count_Goal;
    public int Gacha_Count_Goal;
    public int Weapon_Count_Goal;
    public int Defense_Count_Goal;
    public int Gold_Count_Goal;
    public int Cash_Count_Goal;
    public int Monster_Kill_Goal;
    public int Elite_Kill_Goal;
    public int Boss_Kill_Goal;

}