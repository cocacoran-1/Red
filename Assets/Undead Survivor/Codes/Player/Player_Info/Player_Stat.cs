using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Info", menuName = "Stat")]
public class Player_Stat : ScriptableObject
{
    [Header("공격 관련")]
   
    public float Damage;// 공격력
  
    public float AttackSpeed;//공격속도
  
    public float Attack_Range;//공격범위
  
    public float Attack_Duration;//공격 지속시간


    [Header("체력 관련")]
  
    public float Max_Hp;//최대체력
   
    public float Defense;//방어력 증가
   
    public float Hp_Regen;//체력 회복

    [Header("기타")]
    
    public float Speed;//이동속도
  
    public float Magnet_Range;//자석 범위
   
    public float Exp_Up;// 경험치 증가
   
    public float Gold_Up;//골드 증가

    [Header("재화")]

    public int Gold;
    public int Cash;
    public int Upgrade_Item;
    public int energy;
    public int Max_energy;
    public int Gacha_Stack;//뽑기 횟수

   



   
}


