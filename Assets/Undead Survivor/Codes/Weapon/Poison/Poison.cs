using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [Header("독 폭발")]
    public float Poison_Explosion_Damage=1f;
    public float Poison_Explosion_Range = 1f;
    public int Poison_Explosion_Count = 1;
    [Header("독 늪")]
    public float Poison_swamp_Damage = 1f;
    public int Poison_swamp_Count = 1;
    public float Poison_swamp_Range = 1f;
    [Header("유령발사")]
    public float Poison_skull_Damage = 1f;
    public int Poison_skull_Count = 1;
    public float Poison_skull_Range = 1f;
    [Header("포이즌 노바")]
    public int Poison_nova_Count=6;
    public float Poison_nova_Damage = 1f;

    [Header("")]
    public int fireCount = 0;
    Bounds bounds;
    [SerializeField]
    float offsetY;
    [SerializeField]
    float offsetX;
    public Player player;
    Vector2 offset;
    float timer;
    public float speed;
    public int bulletspeed;
    public float damage;
    public float dot_damage;
    public float Attack_Range;
    public float Attack_Duration;
    public int count;
    public int weapon_id;
    Player_Info player_info;
    SpriteRenderer spriter;
    public WeaponPoolManager poolManager;
    Vector2 spawnOffset;
    float radius;
    CircleCollider2D circle;
    Rigidbody2D rigid;

    void Start()
    {
        if (weapon_id == 0 || weapon_id == 1)
        {
            bounds = GetComponent<Collider2D>().bounds;
        }

        if (weapon_id == 3)
        {
            circle = GetComponent<CircleCollider2D>();
            radius = circle.radius;
        }
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        poolManager = GetComponent<WeaponPoolManager>();
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        offsetX = Random.Range(-bounds.extents.x, bounds.extents.x) + player.transform.position.x;
        offsetY = Random.Range(-bounds.extents.y, bounds.extents.y) + player.transform.position.y;



        timer += Time.deltaTime;
        switch (weapon_id)
        {
            case 0://독늪
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        Poison_swamp();
                    }
                    break;
                }
            case 1://독폭발
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        Poison_Explosion();
                    }
                    break;
                }
            case 2://유령발사
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        Poison_skull();
                    }
                    break;
                }
            case 3://포이즌 노바
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        Poison_nova();
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
            case 0://독 늪
                speed = player_info.Get_AttackSpeed() + 4f;//생성되는 쿨타임
                Attack_Duration = player_info.Get_Attack_Duration()+2f;
                Attack_Range = Poison_swamp_Range+player_info.Get_Attack_Range();
                count = Poison_swamp_Count;//생성 개수
                dot_damage = ((Poison_swamp_Damage + player_info.Get_Damage()) * 0.1f) + 0.2f;

                break;
            case 1://독 폭파
                speed = player_info.Get_AttackSpeed()+3f;
                Attack_Range = Poison_Explosion_Range+player_info.Get_Attack_Range();
                damage = Poison_Explosion_Damage+player_info.Get_Damage();//폭파 데미지
                count = Poison_Explosion_Count;
                dot_damage = ((Poison_Explosion_Damage + player_info.Get_Damage()) * 0.1f) + 0.2f;//독 데미지

                break;
            case 2: //관통유령발사
                count = Poison_skull_Count;
                bulletspeed = 5;
                damage = Poison_skull_Damage+player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed()+4f;
                Attack_Range = Poison_skull_Range+ player_info.Get_Attack_Range();
                Attack_Duration = player_info.Get_Attack_Duration();
                dot_damage = ((Poison_skull_Damage + player_info.Get_Damage()) * 0.1f) + 0.2f;//독 데미지

                break;
            case 3: //포이즌노바
                count = Poison_nova_Count;
                bulletspeed = 5;
                speed = player_info.Get_AttackSpeed()+2f;
                Attack_Range = player_info.Get_Attack_Range();
                damage = Poison_nova_Damage+player_info.Get_Damage();
                dot_damage = ((Poison_nova_Damage + player_info.Get_Damage()) * 0.1f) + 0.2f;//독 데미지

                break;
            default:

                break;
        }
    }

    void Poison_swamp()//독늪
    {
        for(int i=0; i<count; i++)
        {
            Transform bullet = poolManager.Get().transform;
            bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
            bullet.transform.position = bounds.center + new Vector3(offsetX, offsetY);
        }
    }
    void Poison_Explosion()//독폭파 
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet = poolManager.Get().transform;
            bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
            bullet.transform.position = bounds.center + new Vector3(offsetX, offsetY);
        }
    }

    void Poison_skull()
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
                bullet.position = player.transform.position;//투사체 위치 플레이어위치로 변경
                bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);

                bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//프리펩의 회전을 적의 방향으로 회전
                bullet.GetComponent<Poison_skull>().Init(damage, dir, bulletspeed, Attack_Range); //원거리 무기에서의 count는 관통력을 의미

            }
        }
        else
        {
            for (int i = 0; i < count; ++i)
            {
                Vector3 targetPos = player.scanner.sortedTargets[i].transform.position;//스캐너에서 감지한 배열에서 위치 정보가져옴
                Vector3 dir = targetPos - player.transform.position;//타겟과 플레이어의 방향
                dir.Normalize();//벡터길이 1로 변경
                Transform bullet = poolManager.Get().transform;//투사체 생성
                //bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
                bullet.position = player.transform.position;//투사체 위치 플레이어위치로 변경
                bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);

                bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//프리펩의 회전을 적의 방향으로 회전
                bullet.GetComponent<Poison_skull>().Init(damage, dir, bulletspeed, Attack_Range);
            }
        }
    }

    void Poison_nova()
    {
        float angleStep = 360f / (count + fireCount); // 총알 간의 각도 계산하기

        for (int i = 0; i < (count + fireCount); i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // 해당 총알의 각도 계산하기
            Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정

            Transform bullet = poolManager.Get().transform; // 총알 생성하기

            bullet.rotation = Quaternion.FromToRotation(Vector3.left, -quaternion);// 각도를 기반으로 회전값 계산하기

            spawnOffset = quaternion * radius;
            bullet.transform.position = spawnOffset;
            bullet.transform.position += player.transform.position;

            bullet.GetComponent<Rigidbody2D>().velocity = quaternion * bulletspeed; // 총알 속도 적용하기

            bullet.GetComponent<Poison_nova>().Init(damage, quaternion, bulletspeed, Attack_Range);
        }
    }


}
