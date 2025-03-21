using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProperty : MonoBehaviour
{
    Player_Info player_info;

    [Header("FireBall")]
    public float FireBallDamage = 5f;  // 화염구 데미지
    [Header("FirePillar")]
    public float FirePillarDamage = 5f; // 불기둥 데미지
    [Header("FireZone")]
    public float FireZoneDamage = 1f; // 불장판 데미지
    public float nextDamageTime = 1f; // 데미지 간격

    [Header("FireThrower")]
    public float FireThrowerDamage = 1f;
    void Awake()
    {
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
    }

    void Start()
    {
        // 화염구
        FireBallDamage=player_info.Get_Damage();
        
        // 불기둥
        FirePillarDamage = player_info.Get_Damage();

        // 불장판
        FireZoneDamage = player_info.Get_Damage();
        nextDamageTime=player_info.Get_AttackSpeed();

        // 화염방사
        FireThrowerDamage = player_info.Get_Damage();
    }

    public void Init()
    {
        // 화염구
        FireBallDamage=player_info.Get_Damage();
        
        // 불기둥
        FirePillarDamage = player_info.Get_Damage();

        // 불장판
        FireZoneDamage = player_info.Get_Damage();
        nextDamageTime=player_info.Get_AttackSpeed();

        // 화염방사
        FireThrowerDamage = player_info.Get_Damage();
    }

}





