using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class WeaponSelect : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] descriptionObjects;
    Player player;
    Player_Info player_info;
    GameManager gameManager;
    [Header("땅 무기 레벨")]
    int Earth_ThrowRock_Level;//돌던지기 레벨
    int Earth_Rock_Level;//돌 회전 레벨
    int Earth_Shield_Level;//쉴드 레벨
    int Earth_Golem_Level;//골렘 레벨

    [Header("전기 무기 레벨")]
    int Lightning_Thunder_Level;//번개 레벨
    int Lightning_Beam_Level;//레이저 레벨
    int LightningQ_Level;//번개다발 레벨
    int Lightning_Chain_Level;//체인라이트닝

    [Header("얼음 무기 레벨")]
    int Ice_Shot_Level;//얼음송곳 레벨
    int Ice_Mine_Level;//얼음지뢰 레벨
    int Ice_Ball_Level;//눈덩이 레벨
    int Ice_spear_Level;//얼음창 레벨
    [Header("독 무기 레벨")]
    int Poison_Explosion_Level;//독 폭발
    int Poison_swamp_Level;//독 늪
    int Poison_skull_Level;//유령발사
    int Poison_nova_Level;//포이즌 노바
    [Header("바람 무기 레벨")]
    int Wind_Blade_Level;//바람칼날
    int Wind_Hole_Level;//바람뭉치기
    int Wind_Tornado_Level;//소용돌이
    int Wind_Wave_Level;//바람파장 

    [Header("화염 무기 레벨")]
    int FireBall_Level;//화염구
    int FireZone_Level;//불장판
    int FireThrower_Level;//화염방사
    int FirePillar_Level;//불기둥
    [Header("패시브 레벨")]
    int Damage_Level=0;
    int Attack_Speed_Level = 0;
    int Max_Hp_Level = 0;
    int Speed_Level = 0;
    int Attack_Range_Level = 0;
    int Attack_Duration_Level = 0;
    int Hp_Regen_Level = 0;
    int Magnet_Range_Level = 0;
    int Exp_Up_Level = 0;
    int Gold_Up_Level = 0;
    int Defense_Level = 0;

    private void Awake()
    {
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    //-------------------------------------------
    //------------------Earth--------------------
    //-------------------------------------------

    
    public void Earth_ThrowRock()//돌던지기
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Earth_ThrowRock");//게임 오브젝트 위치 찾기
        if (obj.gameObject.activeSelf == false)//비활성화 상태이면 활성화
        {
            obj.gameObject.SetActive(true);
            Earth_ThrowRock_Level = 1;
        }
        else//활성화 상태인데 한번더 버튼 을 눌렀을시 레벨업함수 호출
        {
            Earth weapon = obj.GetComponent<Earth>();
            ++Earth_ThrowRock_Level;
            
            if (Earth_ThrowRock_Level==2 || Earth_ThrowRock_Level == 4 || Earth_ThrowRock_Level == 6)
            {
                weapon.Stone_Damage += 4f;
                weapon.Init();
            }
            else if (Earth_ThrowRock_Level == 3)
            {
                weapon.Stone_Count += 1;
                weapon.Init();
            }
            else if(Earth_ThrowRock_Level == 5)
            {
                weapon.Stone_Clone_Count += 1;
                weapon.Init();
            }
            else if(Earth_ThrowRock_Level == 7)
            {
                weapon.Stone_Count += 1;
                weapon.Stone_Clone_Count += 1;
                weapon.Init();
            }
        }


    }
    public void Earth_stone()//돌 회전무기
    {
        Transform obj = GameObject.Find("Weapon").transform.Find("Earth_stone");//게임 오브젝트 위치 찾기
        if (obj.gameObject.activeSelf == false)//비활성화 상태이면 활성화
        {
            obj.gameObject.SetActive(true);
            Earth_Rock_Level = 1;

        }
        else//활성화 상태인데 한번더 버튼 을 눌렀을시 레벨업함수 호출
        {
            Earth weapon = obj.GetComponent<Earth>();
            ++Earth_Rock_Level;
            if(Earth_Rock_Level==2 || Earth_Rock_Level == 4 || Earth_Rock_Level == 6 )
            {
                weapon.Rock_Damage += 4f;
                weapon.Init();
            }
            else if (Earth_Rock_Level == 3 || Earth_Rock_Level == 5 || Earth_Rock_Level == 7)
            {
                weapon.Rock_Count += 1;
                weapon.Init();
            }


        }


    }
    public void Earth_Shield()//쉴드
    {
        Transform obj = GameObject.Find("Weapon").transform.Find("Earth_shield");//게임 오브젝트 위치 찾기
        if (obj.gameObject.activeSelf == false)//비활성화 상태이면 활성화
        {
            obj.gameObject.SetActive(true);
            Earth_Shield_Level = 1;

        }
        else//활성화 상태인데 한번더 버튼 을 눌렀을시 레벨업함수 호출
        {
            Earth weapon = obj.GetComponent<Earth>();
            ++Earth_Shield_Level;
            if (Earth_Shield_Level == 2 || Earth_Shield_Level == 4 || Earth_Shield_Level == 6)
            {
                weapon.Shield += 7f;
                weapon.Init();
            }
            else if (Earth_Shield_Level == 3 || Earth_Shield_Level == 5 || Earth_Shield_Level == 7)
            {
                weapon.Shield_Speed -= 1;
                weapon.Init();
            }
            else if (Earth_Shield_Level == 7)
            {
                weapon.Shield_Speed -= 2;
                weapon.Init();
            }


        }
    }
    public void Earth_Golem()//골램
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Earth_Golem");//게임 오브젝트 위치 찾기
        if (obj.gameObject.activeSelf == false)//비활성화 상태이면 활성화
        {
           obj.gameObject.SetActive(true);
           Earth_Golem_Level = 1;

        }
        else//활성화 상태인데 한번더 버튼 을 눌렀을시 레벨업함수 호출
        {
            Earth weapon = obj.GetComponent<Earth>();
            ++Earth_Golem_Level;
            if (Earth_Golem_Level == 2 || Earth_Golem_Level == 4 || Earth_Golem_Level == 6)
            {
                weapon.Golem_Damage += 2;
                weapon.Golem_level_up();
            }
            else if (Earth_Golem_Level == 3 || Earth_Golem_Level == 5 || Earth_Golem_Level == 7)
            {
                weapon.Golem_Damage += 2;
                weapon.Golem_Count += 1;
                weapon.Golem_level_up();
            }

        }
    }
    //-------------------------------------------
    //------------------Lightning----------------
    //-------------------------------------------
    public void Lightning_Thunder()//번개
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Lightning_Thunder");
        if (obj.gameObject.activeSelf == false)
        {
            
            obj.gameObject.SetActive(true);
            Lightning_Thunder_Level = 1;
        }
        else
        {
           
            Lightning weapon = obj.GetComponent<Lightning>();
            ++Lightning_Thunder_Level;
            if (Lightning_Thunder_Level == 2 || Lightning_Thunder_Level == 4 || Lightning_Thunder_Level == 6)
            {
                weapon.Thunder_Damage += 7f;
                weapon.Init();
            }
            else if (Lightning_Thunder_Level == 3 || Lightning_Thunder_Level == 5 || Lightning_Thunder_Level == 7)
            {
                weapon.Thunder_Count += 1;
                weapon.Init();
            }
        }
    }
    public void Lightning_Beam()//전방레이저
    {
        Transform obj = GameObject.Find("Weapon").transform.Find("Lightning_Ray");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Lightning_Beam_Level = 1;
        }
        else
        {
            Lightning weapon = obj.GetComponent<Lightning>();
            ++Lightning_Beam_Level;
            if (Lightning_Beam_Level == 2  || Lightning_Beam_Level == 6)
            {
                weapon.Beam_Damage += 0.7f;
                weapon.Init();
            }
            else if (Lightning_Beam_Level == 3 ||  Lightning_Beam_Level == 7)
            {
                weapon.Beam_Speed *= 0.9f;
                weapon.Init();
            }
            else if (Lightning_Beam_Level == 4 || Lightning_Beam_Level == 5)
            {
                weapon.Beam_Range *= 1.1f;
                weapon.Init();
            }
        }
    }
    public void Chain_Lightning()//체인라이트닝
    {
        Transform obj = GameObject.Find("Weapon").transform.Find("Lightning_Chain");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Lightning_Chain_Level = 1;
        }
        else
        {
            Lightning weapon = obj.GetComponent<Lightning>();
            ++Lightning_Chain_Level;
            if (Lightning_Chain_Level == 2 || Lightning_Chain_Level == 4 || Lightning_Chain_Level == 6)
            {
                weapon.Lightning_Chain_Damage += 5f;
                weapon.Init();
            }
            else if (Lightning_Chain_Level == 3 || Lightning_Chain_Level == 5)
            {
                weapon.Lightning_Chain_Count += 1;
                weapon.Init();
            }
            else if (Lightning_Chain_Level == 7)
            {
                weapon.Lightning_Chain_Count += 1;
                weapon.Lightning_Chain_Damage += 5f;
                weapon.Init();
            }
        }
    }
    public void Lightning_Q()//스웨인Q 
    {
        Transform obj = GameObject.Find("Weapon").transform.Find("Lightning_Forked");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            LightningQ_Level = 1;
        }
        else
        {
            Lightning weapon = obj.GetComponent<Lightning>();
            ++LightningQ_Level;
            if (LightningQ_Level == 2 || LightningQ_Level == 4 || LightningQ_Level == 6)
            {
                weapon.LightningQ_Damage += 8f;
                weapon.Init();
            }
            else if (LightningQ_Level == 3 || LightningQ_Level == 5 || LightningQ_Level == 7)
            {
                weapon.LightningQ_Speed *= 0.9f;
                weapon.Init();
            }
        }
    }
    //-------------------------------------------
    //------------------Ice----------------------
    //-------------------------------------------
    public void Ice_Shot() // 얼음송곳
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Ice_shot");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Ice_Shot_Level = 1;
        }
        else
        {
            Ice weapon = obj.GetComponent<Ice>();
            ++Ice_Shot_Level;
            if (Ice_Shot_Level == 2 || Ice_Shot_Level == 4 || Ice_Shot_Level == 6)
            {
                weapon.Shot_Damage += 4f;
                weapon.Init();
            }
            else if (Ice_Shot_Level == 3 )
            {
                weapon.Shot_Count += 1;
                weapon.Init();
            }
            else if (Ice_Shot_Level == 5)
            {
                weapon.Shot_Clone_Count += 2;
                weapon.Init();
            }
            else if (Ice_Shot_Level == 7)
            {
                weapon.Shot_Count += 1;
                weapon.Shot_Clone_Count += 2;
                weapon.Init();
            }
        }
    }
    public void Ice_Ball() // 눈덩이
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Ice_Ball");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Ice_Ball_Level = 1;
        }
        else
        {
            Ice weapon = obj.GetComponent<Ice>();
            ++Ice_Ball_Level;
            if (Ice_Ball_Level == 2 || Ice_Ball_Level == 4 || Ice_Ball_Level == 6)
            {
                weapon.Ball_Damage += 4f;
                weapon.Init();
            }
            else if (Ice_Ball_Level == 3 || Ice_Ball_Level == 5 || Ice_Ball_Level == 7)
            {
                weapon.Ball_Range *= 1.1f;
                weapon.Init();
                weapon.ice_Ball.iceballRange();
            }
           
        }
    }
    public void Ice_Mine() // 얼음지뢰
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Ice_mine");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Ice_Mine_Level = 1;
        }
        else
        {
            Ice weapon = obj.GetComponent<Ice>();
            ++Ice_Mine_Level;
            if (Ice_Mine_Level == 2 || Ice_Mine_Level == 4)
            {
                weapon.Mine_Damage += 10f;
                weapon.Init();
            }
            else if (Ice_Mine_Level == 3 || Ice_Mine_Level == 6)
            {
                weapon.Mine_Count+= 1;
                weapon.Init();
            }
            else if (Ice_Mine_Level == 5 || Ice_Mine_Level == 7)
            {
                weapon.Mine_Range *= 1.1f;
                weapon.Init();
            }
        }
    }
    public void Ice_Spear() // 얼음창
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Ice_Spear");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Ice_spear_Level = 1;
        }
        else
        {
            Ice weapon = obj.GetComponent<Ice>();
            ++Ice_spear_Level;
            if (Ice_spear_Level == 2 || Ice_spear_Level == 4 || Ice_spear_Level == 6)
            {
                weapon.Ice_spear_Damage += 7f;
                weapon.Init();
            }
            else if (Ice_spear_Level == 3 || Ice_spear_Level == 5 || Ice_spear_Level == 7)
            {
                weapon.Ice_spear_Count += 1;
                weapon.Init();
            }
        }
    }

    //-------------------------------------------
    //------------------Poison-------------------
    //-------------------------------------------
    public void Poison_Bomb() // 독 폭발
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Poison_Bomb");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Poison_Explosion_Level = 1;
        }
        else
        {
            Poison weapon = obj.GetComponent<Poison>();
            ++Poison_Explosion_Level;
            if (Poison_Explosion_Level == 2 || Poison_Explosion_Level == 4 || Poison_Explosion_Level == 6)
            {
                weapon.Poison_Explosion_Damage += 5f;
                weapon.Init();
            }
            else if (Poison_Explosion_Level == 3 || Poison_Explosion_Level == 5)
            {
                weapon.Attack_Range *= 1.1f;
                
            }
            else if (Poison_Explosion_Level == 7)
            {
                weapon.Poison_Explosion_Count += 1;
                weapon.Init();
            }
        }
    }
    public void Poison_Swamp() // 독 늪
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Poison_swamp");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Poison_swamp_Level = 1;
        }
        else
        {
            Poison weapon = obj.GetComponent<Poison>();
            ++Poison_swamp_Level;
            if (Poison_swamp_Level == 2 || Poison_swamp_Level == 4 || Poison_swamp_Level == 6)
            {
                weapon.Poison_swamp_Damage += 17f;
                weapon.Init();
            }
            else if (Poison_swamp_Level == 3 || Poison_swamp_Level == 5)
            {
                weapon.Poison_swamp_Count += 1;
                weapon.Init();
            }
            else if (Poison_swamp_Level == 7)
            {
                weapon.Poison_swamp_Range *= 1.1f;
                weapon.Init();
            }
        }
    }
    public void Poison_Nova() // 포이즌 노바
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Poison_Nova");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Poison_nova_Level = 1;
        }
        else
        {
            Poison weapon = obj.GetComponent<Poison>();
            ++Poison_nova_Level;
            if (Poison_nova_Level == 2 || Poison_nova_Level == 4 || Poison_nova_Level == 6)
            {
                weapon.Poison_nova_Damage += 5f;
                weapon.Init();
            }
            else if (Poison_nova_Level == 3 || Poison_nova_Level == 5 || Poison_nova_Level == 7)
            {
                weapon.Poison_nova_Count += 2;
                weapon.Init();
            }
           
        }
    }
    public void Poison_Skull() // 유령발사
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Poison_Launch");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Poison_skull_Level = 1;
        }
        else
        {
            Poison weapon = obj.GetComponent<Poison>();
            ++Poison_skull_Level;
            if (Poison_skull_Level == 2 || Poison_skull_Level == 4 || Poison_skull_Level == 6)
            {
                weapon.Poison_skull_Damage += 7f;
                weapon.Init();
            }
            else if (Poison_skull_Level == 3 || Poison_skull_Level == 5)
            {
                weapon.Poison_skull_Count += 1;
                weapon.Init();
            }
            else if (Poison_skull_Level == 7)
            {
                weapon.Poison_skull_Range *= 1.2f;
                weapon.Init();
            }
        }
    }
    //-------------------------------------------
    //------------------Wind---------------------
    //-------------------------------------------
    public void Wind_Blade() // 바람칼날
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Wind_Blade");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Wind_Blade_Level = 1;
        }
        else
        {
            Wind weapon = obj.GetComponent<Wind>();
            ++Wind_Blade_Level;
            if (Wind_Blade_Level == 2 || Wind_Blade_Level == 4 || Wind_Blade_Level == 6)
            {
                weapon.WindBlade_Damage += 4f;
                weapon.Init();
            }
            else if (Wind_Blade_Level == 3 || Wind_Blade_Level == 5)
            {
                weapon.WindBlade_Range *= 1.1f;
                weapon.Init();
            }
            else if (Wind_Blade_Level == 7)
            {
                weapon.WindBlade_Count += 1;
                weapon.Init();
            }
        }
    }
    public void Wind_Hole() // 바람뭉치기
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Wind_hole");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Wind_Hole_Level = 1;
        }
        else
        {
            Wind weapon = obj.GetComponent<Wind>();
            ++Wind_Hole_Level;
            if (Wind_Hole_Level == 2 || Wind_Hole_Level == 4 || Wind_Hole_Level == 6)
            {
                weapon.Wind_hole_Damage += 1f;
                weapon.Init();
            }
            else if (Wind_Hole_Level == 3 || Wind_Hole_Level == 5)
            {
                weapon.Wind_hole_Range *= 1.1f;
                weapon.Init();
            }
            else if (Wind_Hole_Level == 7)
            {
                weapon.Wind_hole_Speed -= 1f;
                weapon.Init();
            }
        }
    }
    public void Wind_Tornado() // 소용돌이
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Wind_Tornado");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Wind_Tornado_Level = 1;
        }
        else
        {
            Wind weapon = obj.GetComponent<Wind>();
            ++Wind_Tornado_Level;
            if (Wind_Tornado_Level == 2 || Wind_Tornado_Level == 4 || Wind_Tornado_Level == 6)
            {
                weapon.Wind_Tornado_Damage += 4f;
                weapon.Init();
            }
            else if (Wind_Tornado_Level == 3 || Wind_Tornado_Level == 5)
            {
                weapon.Wind_Tornado_Range *= 1.1f;
                weapon.Init();
            }
            else if (Wind_Tornado_Level == 7)
            {
                weapon.Wind_Tornado_Count += 1;
                weapon.Init();
            }
        }
    }
    public void Wind_() // 바람파장(미완성)
    {
        Transform obj = GameObject.Find("Weapon").transform.Find("Wind_Wave");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            Wind_Wave_Level = 1;
        }
        else
        {
            Wind weapon = obj.GetComponent<Wind>();
            ++Wind_Wave_Level;
            if (Wind_Wave_Level == 2 || Wind_Wave_Level == 4)
            {
                weapon.Wind_Wave_Damage += 4f;
                weapon.Init();
            }
            else if (Wind_Wave_Level == 3 || Wind_Wave_Level == 6)
            {
                weapon.Wind_Wave_Range *= 1.1f;
                weapon.Init();
            }
            else if (Wind_Wave_Level == 5)
            {
                weapon.Wind_Wave_Speed *= 0.8f;
                weapon.Init();
            }
            else if (Wind_Wave_Level == 7)
            {
                weapon.Wind_Wave_Damage += 4f;
                weapon.Wind_Wave_Range *= 1.1f;
                weapon.Init();
            }
        }
    }
    //-------------------------------------------
    //------------------Fire---------------------
    //-------------------------------------------
    public void FireBall() // 화염구
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Fire_Ball");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            FireBall_Level = 1;
        }
        else
        {
            Fire weapon = obj.GetComponent<Fire>();
            ++FireBall_Level;
            if (FireBall_Level == 2 || FireBall_Level == 4 || FireBall_Level == 6)
            {
                weapon.FireBall_Damage += 4f;
                weapon.Init();
            }
            else if (FireBall_Level == 3 )
            {
                weapon.FireBall_Count += 1;
                weapon.Init();
            }
            else if (FireBall_Level == 5)
            {
                weapon.FireBall_Range *= 1.1f;
                weapon.Init();
            }
            else if (FireBall_Level == 7)
            {
                weapon.FireBall_Speed -= 1f;
                weapon.Init();
            }
        }
    }
    public void FireZone() // 장판
    {
        Transform obj = GameObject.Find("Weapon").transform.Find("Fire_Zone");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            FireZone_Level = 1;
        }
        else
        {
            Fire weapon = obj.GetComponent<Fire>();
            ++FireZone_Level;
            if (FireZone_Level == 2 || FireZone_Level == 4 || FireZone_Level == 6)
            {
                weapon.FireZone_Damage += 1f;
                weapon.Init();
            }
            else if (FireZone_Level == 3 || FireZone_Level == 5 || FireZone_Level == 7)
            {
                weapon.FireZone_Range *= 1.1f;
                weapon.Init();
                weapon.FireZone_Init();
            }
            
        }
    }
    public void FireThrower() // 화염방사
    {
        Transform obj = GameObject.Find("Weapon").transform.Find("Fire_Thrower");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            ++FireThrower_Level;
        }
        else
        {
            Fire weapon = obj.GetComponent<Fire>();
            ++FireThrower_Level;
            if (FireThrower_Level == 2 || FireThrower_Level == 4 || FireThrower_Level == 6)
            {
                weapon.FireThrower_Damage += 1f;
                weapon.Init();
            }
            else if (FireThrower_Level == 3)
            {
                weapon.FireThrower_Duration += 1f;
                weapon.Init();
            }
            else if (FireThrower_Level == 5)
            {
                weapon.FireThrower_Range *= 1.2f;
                weapon.Init();
            }
            else if (FireThrower_Level == 7)
            {
                weapon.FireThrower_Speed *= 0.8f;
                weapon.Init();
            }
        }
    }
    public void FirePillar() // 불기둥
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("FirePillarRange");
        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            ++FirePillar_Level;
        }
        else
        {
            Fire weapon = obj.GetComponent<Fire>();
            ++FirePillar_Level;
            if (FirePillar_Level == 2 || FirePillar_Level == 4 || FirePillar_Level == 6)
            {
                weapon.FirePillar_Damage += 9f;
                weapon.Init();
            }
            else if (FirePillar_Level == 3 || FirePillar_Level == 7)
            {
                weapon.FirePillar_Count += 1;
                weapon.Init();
            }
            else if (FirePillar_Level == 5)
            {
                weapon.FirePillar_Range *= 1.2f;
                weapon.Init();
            }
        }
    }
   
    
    //=================================================================================================================================================
    //=================================================================================================================================================
    //=================================================================================================================================================
    // 패시브 스킬
 
 
    public void Damage()//데미지 증가 함수
    {
        ++Damage_Level;
        player_info.Init_Damage(1.2f, '*');

        Ice[] IceScripts = FindObjectsOfType<Ice>();
        for (int i = 0; i < IceScripts.Length; i++)
        {
            IceScripts[i].Init();
        }
        Earth[] EarthScripts = FindObjectsOfType<Earth>();
        for (int i = 0; i < EarthScripts.Length; i++)
        {
            EarthScripts[i].Init();
        }
        Poison[] PoisonScripts = FindObjectsOfType<Poison>();
        for (int i = 0; i < PoisonScripts.Length; i++)
        {
            PoisonScripts[i].Init();
        }
        Wind[] WindScripts = FindObjectsOfType<Wind>();
        for (int i = 0; i < WindScripts.Length; i++)
        {
            WindScripts[i].Init();
        }
        Lightning[] LightningScripts = FindObjectsOfType<Lightning>();
        for (int i = 0; i < LightningScripts.Length; i++)
        {
            LightningScripts[i].Init();
        }
        Fire[] FireScripts = FindObjectsOfType<Fire>();
        for (int i = 0; i < FireScripts.Length; i++)
        {
            FireScripts[i].Init();
        }


    }
    public void AttackSpeed()//무기 공격 속도
    {
        ++Attack_Speed_Level;
        player_info.Init_AttackSpeed(0.8f, '*');

        Ice[] IceScripts = FindObjectsOfType<Ice>();
        for (int i = 0; i < IceScripts.Length; i++)
        {
            IceScripts[i].Init();
        }
        Earth[] EarthScripts = FindObjectsOfType<Earth>();
        for (int i = 0; i < EarthScripts.Length; i++)
        {
            EarthScripts[i].Init();
        }
        Poison[] PoisonScripts = FindObjectsOfType<Poison>();
        for (int i = 0; i < PoisonScripts.Length; i++)
        {
            PoisonScripts[i].Init();
        }
        Wind[] WindScripts = FindObjectsOfType<Wind>();
        for (int i = 0; i < WindScripts.Length; i++)
        {
            WindScripts[i].Init();
        }
        Lightning[] LightningScripts = FindObjectsOfType<Lightning>();
        for (int i = 0; i < LightningScripts.Length; i++)
        {
            LightningScripts[i].Init();
        }
        Fire[] FireScripts = FindObjectsOfType<Fire>();
        for (int i = 0; i < FireScripts.Length; i++)
        {
            FireScripts[i].Init();
        }


    }
    public void Attack_Range()//무기 공격 범위
    {
        ++Attack_Range_Level;
        player_info.Init_Attack_Range(1.2f, '*');

        Ice[] IceScripts = FindObjectsOfType<Ice>();
        for (int i = 0; i < IceScripts.Length; i++)
        {
            IceScripts[i].Init();
            if(IceScripts[i].ice_Ball!=null)
            {
                IceScripts[i].ice_Ball.iceballRange();
            }
        }
        Earth[] EarthScripts = FindObjectsOfType<Earth>();
        for (int i = 0; i < EarthScripts.Length; i++)
        {
            EarthScripts[i].Init();
        }
        Poison[] PoisonScripts = FindObjectsOfType<Poison>();
        for (int i = 0; i < PoisonScripts.Length; i++)
        {
            PoisonScripts[i].Init();
        }
        Wind[] WindScripts = FindObjectsOfType<Wind>();
        for (int i = 0; i < WindScripts.Length; i++)
        {
            WindScripts[i].Init();
        }
        Lightning[] LightningScripts = FindObjectsOfType<Lightning>();
        for (int i = 0; i < LightningScripts.Length; i++)
        {
            LightningScripts[i].Init();
        }
        Fire[] FireScripts = FindObjectsOfType<Fire>();
        for (int i = 0; i < FireScripts.Length; i++)
        {
            FireScripts[i].Init();
            if(FireScripts[i].weapon_id==2)
            {
                FireScripts[i].FireZone_Init();
            }
        }


    }
    public void Attack_Duration()//무기 공격 지속시간
    {
        ++Attack_Duration_Level;
        player_info.Init_Attack_Duration(1.2f, '*');

        Ice[] IceScripts = FindObjectsOfType<Ice>();
        for (int i = 0; i < IceScripts.Length; i++)
        {
            IceScripts[i].Init();
        }
        Earth[] EarthScripts = FindObjectsOfType<Earth>();
        for (int i = 0; i < EarthScripts.Length; i++)
        {
            EarthScripts[i].Init();
        }
        Poison[] PoisonScripts = FindObjectsOfType<Poison>();
        for (int i = 0; i < PoisonScripts.Length; i++)
        {
            PoisonScripts[i].Init();
        }
        Wind[] WindScripts = FindObjectsOfType<Wind>();
        for (int i = 0; i < WindScripts.Length; i++)
        {
            WindScripts[i].Init();
        }
        Lightning[] LightningScripts = FindObjectsOfType<Lightning>();
        for (int i = 0; i < LightningScripts.Length; i++)
        {
            LightningScripts[i].Init();
        }
        Fire[] FireScripts = FindObjectsOfType<Fire>();
        for (int i = 0; i < FireScripts.Length; i++)
        {
            FireScripts[i].Init();
        }


    }
    public void Max_Hp()//최대 체력
    {
        ++Max_Hp_Level;
        player_info.Init_Max_Hp(1.2f, '*');
        gameManager.Init();



    }
    public void Speed()//플레이어 이동속도
    {
        ++Speed_Level;
        player_info.Init_Speed(1.2f, '*');

        player.Init();

    }
    public void Hp_Regen()//체력 재생 속도
    {
        ++Hp_Regen_Level;
        player_info.Init_Hp_Regen(0.5f, '+');

        gameManager.Init();


    }
    public void Exp_Up()//경험치 증가
    {
        ++Exp_Up_Level;
        player_info.Init_Exp_Up(1.2f, '*');

        player.Init();


    }
    public void Gold_Up()//골드 증가
    {
        ++Gold_Up_Level;
        player_info.Init_Gold_Up(1.2f, '*');

        player.Init();


    }
    public void Defense()//방어력
    {
        ++Defense_Level;
        player_info.Init_Defense(1.2f, '*');

        player.Init();

    }
    public void Magnet_Range()//자석범위

    {
        ++Magnet_Range_Level;
        player_info.Init_Magnet_Range(1.2f, '*');

        player.Init();

    }

    // 버튼 선택하면 설명이 나오도록 하는 함수
    public void ShowDescription(Button clickedButton)
    {
        int index = Array.IndexOf(buttons, clickedButton);
        for (int i = 0; i < descriptionObjects.Length; i++)
        {
            if (i == index)
            {
                descriptionObjects[i].SetActive(true);
            }
            else
            {
                descriptionObjects[i].SetActive(false);
            }
        }
        OnWeaponButtonClick(clickedButton);
    }

    // 선택한 버튼의 기능을 가져오는 함수
    public void OnWeaponButtonClick(Button clickedButton)
    {
        int index = Array.IndexOf(buttons, clickedButton);
        /* switch (index)
         {
             case 0:
                 SelectWeapon_0();
                 break;
             case 1:
                 SelectWeapon_1();
                 break;
             case 2:
                 SelectWeapon_2();
                 break;
                 
    }*/

}
}
