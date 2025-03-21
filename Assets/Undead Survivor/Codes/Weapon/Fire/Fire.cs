using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("화염구")]
    public float FireBall_Damage = 1f;
    public int FireBall_Count = 1;
    public float FireBall_Range = 1f;
    public float FireBall_Speed = 1f;
    [Header("화염방사")]
    public float FireThrower_Damage = 1f;
    public float FireThrower_Duration = 1f;
    public float FireThrower_Range = 0f;
    public float FireThrower_Speed = 1f;
    [Header("불장판")]
    public float FireZone_Damage = 1f;
    public float FireZone_Range = 1f;
    [Header("불기둥")]
    public float FirePillar_Damage = 1f;
    public float FirePillar_Range = 1f;
    public int FirePillar_Count = 1;
    public float FirePillar_Speed = 1f;
    [Header(" ")]
    Bounds bounds;
    [SerializeField]
    float offsetY;
    [SerializeField]
    float offsetX;
    public int weapon_id;
    public int count; // 투사체 개수
    public float damage; // 공격력
    public float Attack_Range; // 공격 범위
    public float Attack_Duration;
    public float angle;
    public float timer;
    float radius;
    public float speed; // 공격 대기 시간
    public int bulletspeed; // 총투사체 속도
    public Player player;

    Player_Info player_info;
    public WeaponPoolManager poolManager;
    private Vector3 targetPoint;
    Vector2 spawnOffset;
    CircleCollider2D circle;

    SkillSounds skillSounds;
    private void Awake()
    {
        skillSounds = GameObject.Find("SkillSounds").GetComponent<SkillSounds>();
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
        Init();
        player = GameObject.Find("Player").GetComponentInParent<Player>();
        if (weapon_id == 1)
        {
            circle = GetComponent<CircleCollider2D>();
            radius = circle.radius;
        }
        poolManager = GetComponentInParent<WeaponPoolManager>();
        if (weapon_id == 2)
        {
            FireZone();
        }
    }

    void Update()
    {
        offsetX = Random.Range(-bounds.extents.x, bounds.extents.x) + player.transform.position.x;
        offsetY = Random.Range(-bounds.extents.y, bounds.extents.y) + player.transform.position.y;


        timer += Time.deltaTime;
        switch (weapon_id)
        {
            case 0: // 화염구 
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        FireBall();
                    }
                    break;
                }
            case 1: // 화염방사
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        FireThrower();
                    }
                    break;
                }
            case 2: // 불장판
                {

                    break;
                }
            case 3: // 불기둥
                {
                    if (timer > speed)
                    {
                        FirePillar();
                        timer = 0f;

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
            case 0://화염구
                count = FireBall_Count;
                damage = FireBall_Damage + player_info.Get_Damage();
                speed = FireBall_Speed + player_info.Get_AttackSpeed();
                Attack_Range = FireBall_Range + player_info.Get_Attack_Range();
                bulletspeed = 5;
                break;

            case 1://화염방사

                damage = FireThrower_Damage + player_info.Get_Damage();
                speed = FireThrower_Speed + player_info.Get_AttackSpeed();
                Attack_Range = FireThrower_Range + player_info.Get_Attack_Range();
                Attack_Duration = FireThrower_Duration + player_info.Get_Attack_Duration();

                break;
            case 2://불장판
                damage = FireZone_Damage + player_info.Get_Damage();
                Attack_Range = FireZone_Range + player_info.Get_Attack_Range();


                break;
            case 3://불기둥
                damage = FirePillar_Damage + player_info.Get_Damage();
                count = FirePillar_Count;
                Attack_Range = FirePillar_Range + player_info.Get_Attack_Range();
                speed = FirePillar_Speed + player_info.Get_AttackSpeed();

                break;

            default:

                break;
        }
    }
    public float Get_Attack_Duration()
    {
        return this.Attack_Duration;
    }
    public float Get_Attack_Range()
    {
        return this.Attack_Range;
    }
    public float Get_Damage()
    {
        return this.damage;
    }

    void FireBall()//원거리무기 투사체를 상대방의 위치로 이동하는 함수
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
                skillSounds.SkillSoundPlay(SkillSounds.Sfx.FireCharging);
                bullet.GetComponent<FireBall>().Init(damage, dir, bulletspeed, Attack_Range); //원거리 무기에서의 count는 관통력을 의미
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
                skillSounds.SkillSoundPlay(SkillSounds.Sfx.FireCharging);
                bullet.GetComponent<FireBall>().Init(damage, dir, bulletspeed, Attack_Range);
            }
        }
    }
    void FireThrower()
    {
        spawnOffset = Vector2.right * radius;
        Transform bullet = poolManager.Get().transform;
        bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
        bullet.transform.position = spawnOffset;
        bullet.transform.position += player.transform.position;
        angle = player.angle;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void FireZone()
    {
        Transform bullet = poolManager.Get().transform;
        bullet.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
    }
    public void FireZone_Init()//불장판 크기를 키워주는 함수
    {
        transform.GetChild(0).localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
    }
    void FirePillar()
    {
        int num = Mathf.Min(player.scanner.sortedTargets.Length, count);
        List<int> selectedTargets = new List<int>(); // 이미 선택한 대상을 저장하는 리스트
        for (int i = 0; i < num; ++i)
        {
            int randomScan;
            do
            {
                randomScan = Random.Range(0, player.scanner.sortedTargets.Length); // 무작위로 대상 선택
            } while (selectedTargets.Contains(randomScan)); // 이미 선택된 대상이면 다시 선택
            selectedTargets.Add(randomScan); // 선택한 대상을 리스트에 추가

            Vector3 randomTarget = player.scanner.sortedTargets[randomScan].transform.position;
            if (player.scanner.sortedTargets[randomScan].activeSelf)
            {
                Transform bullet = poolManager.Get().transform;
                skillSounds.SkillSoundPlay(SkillSounds.Sfx.FirePillar);
                bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
                bullet.position = randomTarget;
                bullet.rotation = Quaternion.identity;
                bullet.GetComponent<FirePillar>().target_transform(player.scanner.sortedTargets[randomScan]);

                //player.scanner.sortedTargets[randomScan].SendMessage("onDamaged", damage);
            }
        }
    }
}

