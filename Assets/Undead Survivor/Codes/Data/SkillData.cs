using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "Scriptble Object/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillCategory
    {
        Active,
        Passive,
        Item
    }
    public enum SkillType
    {
        Fire_Ball, Fire_Zone, Fire_Thrower, Fire_Pillar, Wind_Blade, Wind_Hole, Wind_Tornado, Wind_wave,
        Poison_Bomb, Poison_Nova, Poison_Skull, Poison_Swamp, Ice_Ball, Ice_Mine, Ice_Shot, Ice_Spear, Earth_Golem, Earth_Shield, Earth_ThrowRock, Earth_stone,
        Lightning_Beam, Forked_Lightning, Chain_Lightning, Lightning_Thunder, Magnet_Range, Attack_Duration, Attack_Speed, Attack_Range, Hp_Regen, Max_Hp,
        Speed, Damage, Defense, Exp_Up, Gold_Up, Hp, Coin
    }

    [Header("스킬 정보")]
    public SkillCategory skillCategory;
    public SkillType skillType;
    public int skillId;
    public string skillName;

    [TextArea]
    public string DamageDesc;
    public string AddRangeDesc;
    public string AddBulletDesc;
    public string AddSpeedDesc;
    public string AddTimeDesc;
    public string AddCloneDesc;
    public string AddDefenseDesc;

    public string ItemDesc;
    public Sprite SkillICon;

    public int maxLevel;

}
