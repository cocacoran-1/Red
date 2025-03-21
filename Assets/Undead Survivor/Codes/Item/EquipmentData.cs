using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum basicStatusType //강화할때 기본적으로 오르는 스테이터스 타입
{
    damage,
    health,
    defense
}

public enum specialStatusType   //각 무기별 특수능력 스테이터스
{
    AttackSpeed,
    Attack_Range,
    Attack_Duration,
    Hp_Regen,
    Speed,
    Magnet_Range,
    Exp_Up,
    Gold_Up

}

public enum calculateStatus   //+, -, *, /
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

[System.Serializable]
public class specialStatus  //특수능력
{
    public specialStatusType specialStatusType; //어떤특수능력을
    public calculateStatus calculate;   //어떻게
    public float upgradeStatusAmount;   //얼만큼 계산하는지
    public string description;

}

public enum ItemType
{
    Weapon,
    Helmet,
    Armor,
    Boots,
    Gloves,
    Earring

}

public enum ItemGrade { SS, S, A, B, C, D }
public abstract class EquipmentData : ScriptableObject
{
    public specialStatus[] specialStat; //특수능력

    [Header("강화 관련")]
    public int Upgrade_Level;//강화 레벨
    public int Gold_cost;//강화시 들어가는 골드
    public int UpgradeItem_cost;//강화시 들어가는 재화
    public float upgradeStatusAmount;//강화할때 올라가는 수치량

    [Header(" ")]
    public int Item_index;//아이템 번호
    public int sort_num;//인벤토리 정렬 순서
    public int inventory_index;//현재 인벤토리 순서
    public int Upgrade_item;//무기분해시 나오는 재화
    public bool IsEquipped = false;
    public Sprite texture;
    public GameObject prefab;
    public ItemType type;
    public ItemGrade grade;
    public basicStatusType basicType;
    public specialStatusType specialType;

    [TextArea(15, 20)]
    public string description;
    // 기타 장비 데이터 변수들
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



    public bool isOpenReady = false;

    public void Upgrade()
    {
        Upgrade_Level++;
        Gold_cost += Gold_cost;
        UpgradeItem_cost += UpgradeItem_cost;

    ///////////////////////////////////////////////////////////////////////////

        /* if (Upgrade_Level > 20)    //만약 강화레벨이 20 이상이라면 강화스탯 증가
        {
            upgradeStatusAmount = 3;
        } */


    }

    public void CheckSpecialStat()
    {
        for (int i = 0; i < specialStat.Length; i++)
        {
            if (specialStat[i].specialStatusType == specialStatusType.Attack_Duration)
            {
                durationCalculating();
            }
            else if(specialStat[i].specialStatusType == specialStatusType.Attack_Range)
            {

            }
            else if(specialStat[i].specialStatusType == specialStatusType.AttackSpeed)
            {

            }
            else if(specialStat[i].specialStatusType == specialStatusType.Exp_Up)
            {

            }
            else if(specialStat[i].specialStatusType == specialStatusType.Gold_Up)
            {

            }
            else if(specialStat[i].specialStatusType == specialStatusType.Hp_Regen)
            {

            }
            else if(specialStat[i].specialStatusType == specialStatusType.Magnet_Range)
            {

            }
            else if(specialStat[i].specialStatusType == specialStatusType.Speed)
            {

            }
        }

    }

    public void durationCalculating()
    {
        for (int i = 0; i < specialStat.Length; i++)
        {
            if (specialStat[i].calculate == calculateStatus.Addition)
            {
                calculation(Attack_Duration, upgradeStatusAmount, calculateStatus.Addition);
            }
            else if (specialStat[i].calculate == calculateStatus.Subtraction)
            {
                calculation(Attack_Duration, upgradeStatusAmount, calculateStatus.Subtraction);
            }
            else if (specialStat[i].calculate == calculateStatus.Multiplication)
            {
                calculation(Attack_Duration, upgradeStatusAmount, calculateStatus.Multiplication);
            }
            else if (specialStat[i].calculate == calculateStatus.Division)
            {
                calculation(Attack_Duration, upgradeStatusAmount, calculateStatus.Division);
            }
        }
    }


    public float calculation(float a, float b, calculateStatus op)  //a=초기값, b=강화수치
    {
        float p = 0;
        switch (op)
        {
            case calculateStatus.Addition:
                p = a + b;
                break;
            case calculateStatus.Subtraction:
                p = a - b;
                break;
            case calculateStatus.Multiplication:
                p = a * b;
                break;
            case calculateStatus.Division:
                p = a / b;
                break;
            default:
                break;
        }
        Debug.Log("p:" + p);
        return p;
    }

}
