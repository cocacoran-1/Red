using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Daily", menuName = "history/Daily")]

public class Daily_history : ScriptableObject
{
    [Header("몬스터 처치")]
    public int Monster_Kill;
    public int Elite_Kill;
    public int Boss_Kill;

    [Header("메인 메뉴 관련")]
    public int Reinforcement_Count;
    public int Gacha_Count;
    public int Game_Start_Count;
    public int Attendance_Count;
    [Header("크리스탈,광고")]
    public int Payment_Count;
    public int Advertisement_Count;
    public int Cash_Gacha_Count;

}
