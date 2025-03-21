using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    [Header("얼음송곳")]
    public float Shot_Damage = 10f;
    public int Shot_Count = 1;
    public int Shot_Clone_Count = 3;
    [Header("얼음지뢰")]
    public float Mine_Damage = 100f;
    public int Mine_Count = 1;
    public float Mine_Range = 1f;
    [Header("눈덩이")]
    public float Ball_Damage = 40f;
    public float Ball_Range = 1f;
    [Header("얼음창")]
    public float Ice_spear_Damage = 30f;
    public int Ice_spear_Count = 1;
    [Header("")]
    Bounds bounds;
    [SerializeField]
    float offsetY;
    [SerializeField]
    float offsetX;
    public int weapon_id;
    public int count;
    public float damage;
    public float Attack_Range;
    public float Attack_Duration;
    public float timer;
    public float speed;
    public int bulletspeed;
    public int cloneCount;
    public Player player;
    public Ice_ball ice_Ball;
    Player_Info player_info;
    public WeaponPoolManager poolManager;
    private void Awake()
    {
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
        player = GameObject.Find("Player").GetComponentInParent<Player>();
        if (weapon_id == 1 || weapon_id == 3)
        {
            bounds = GetComponent<Collider2D>().bounds;
        }
      //  if(weapon_id == 2)
        //{
         //   ice_Ball=GetComponentInChildren<Ice_ball>();
        //}
        poolManager = GetComponentInParent<WeaponPoolManager>();

    }
    private void Start()
    {
        Init();
    }
    private void OnEnable() {
        if(weapon_id == 2)//얼음 공
        {
            Init();
            Ice_ball();
        }  
    }

    void Update()
    {
        timer += Time.deltaTime;
        switch (weapon_id)
        {
            case 0://얼음송곳
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        Ice_shot();
                    }
                    break;
                }
            case 1://얼음 지뢰
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        Ice_mine();
                    }
                    break;
                }
            case 2://얼음 공
                break;

            case 3://얼음 창
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        Ice_spear();
                    }
                    break;
                }
            default:
                break;
        }


    }

    public void Init()//각 무기 번호에 따라서 무기 기본정보 입력하는 함수
    {

        switch (weapon_id)
        {
            case 0://얼음송곳
                count = Shot_Count;
                damage = Shot_Damage+player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed()+1f;
                Attack_Range = player_info.Get_Attack_Range();
                Attack_Duration = player_info.Get_Attack_Duration();
                bulletspeed = 5;
                cloneCount = Shot_Clone_Count;
                break;
            case 1://얼음 지뢰
                count = Mine_Count;
                damage = Mine_Damage+player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed()+6f;
                Attack_Duration = player_info.Get_Attack_Duration()+3f;
                Attack_Range = (Mine_Range+player_info.Get_Attack_Range());
                break;

            case 2://눈덩이
                count = 1;
                damage = Ball_Damage+player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed();
                Attack_Duration = player_info.Get_Attack_Duration();
                Attack_Range = Ball_Range+player_info.Get_Attack_Range();
                bulletspeed = (int)speed * 10;
               
                break;

            case 3://얼음창
                count = Ice_spear_Count;
                damage = Ice_spear_Damage+player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed()+2f;
                Attack_Duration = player_info.Get_Attack_Duration();
                Attack_Range = player_info.Get_Attack_Range();
                break;
            default:

                break;
        }
    }
    void Ice_shot()//원거리무기 투사체를 상대방의 위치로 이동하는 함수
    {
        if (player.scanner.sortedTargets.Length == 0)//스케너 배열이 비어있으면 리턴
            return;

        if (player.scanner.sortedTargets.Length < count)// 배열이 탄 개수 보다 적으면 배열의 크기만큼만 발사
        {
            for (int i = 0; i < player.scanner.sortedTargets.Length; i++)
            {
                Vector3 targetPos = player.scanner.sortedTargets[i].transform.position;//스캐너에서 감지한 배열에서 위치 정보가져옴
                Vector3 dir = targetPos - player.transform.position;//타겟과 플레이어의 방향
                dir.Normalize();//벡터길이 1로 변경
                Transform bullet = poolManager.Get().transform;//투사체 생성
                bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
                bullet.position = player.transform.position;//투사체 위치 플레이어위치로 변경
                bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);//프리펩의 회전을 적의 방향으로 회전
                Quaternion rotation = bullet.transform.rotation;
                float angle = rotation.eulerAngles.z;

                bullet.GetComponent<Ice_Shot>().Init(damage, dir, bulletspeed, cloneCount, angle, Attack_Range); //원거리 무기에서의 count는 관통력을 의미
            }
        }
        else
        {
            for (int i = 0; i < count; ++i)
            {
                Vector3 targetPos = player.scanner.sortedTargets[i].transform.position;
                Vector3 dir = targetPos - player.transform.position;
                dir.Normalize();
                Transform bullet = poolManager.Get().transform;
                bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
                bullet.position = player.transform.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
                Quaternion rotation = bullet.transform.rotation;
                float angle = rotation.eulerAngles.z;

                bullet.GetComponent<Ice_Shot>().Init(damage, dir, bulletspeed, cloneCount, angle, Attack_Range);
            }
        }
    }

    void Ice_mine()//얼음지뢰 카메라 범위내에 무작위 위치로 소환
    {
        for (int i = 0; i < count; ++i)
        {
            offsetX = Random.Range(-bounds.extents.x, bounds.extents.x) + player.transform.position.x;
            offsetY = Random.Range(-bounds.extents.y, bounds.extents.y) + player.transform.position.y;
            Transform bullet = poolManager.Get().transform;
            bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
            bullet.transform.position = bounds.center + new Vector3(offsetX, offsetY);
        }

    }

    void Ice_ball()
    {
        float random = Random.Range(0, 360);
        Vector2 dir = new Vector2(Mathf.Cos(random), Mathf.Sin(random));
        
        dir.Normalize();//벡터길이 1로 변경

        Transform bullet = poolManager.Get().transform;
        ice_Ball = bullet.GetComponent<Ice_ball>();
        bullet.position = player.transform.position;
        bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
        bullet.rotation = Quaternion.FromToRotation(Vector3.left, -dir);//프리펩의 회전을 적의 방향으로 회전
        Quaternion rotation = bullet.transform.rotation;

        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletspeed;
    }

    void Ice_spear()
    {
        offsetX = Random.Range(-bounds.extents.x, bounds.extents.x) + player.transform.position.x;
        offsetY = Random.Range(-bounds.extents.y, bounds.extents.y) + player.transform.position.y;

        for (int i = 0; i < count; ++i)
        {
            Transform bullet = poolManager.Get().transform;
            bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
            bullet.transform.position = bounds.center + new Vector3(offsetX, offsetY);
        }
    }

}
