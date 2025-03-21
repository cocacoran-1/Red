using UnityEngine;

public class Wind : MonoBehaviour
{
    [Header("바람뭉치기")]
    public float Wind_hole_Damage = 1f;
    public float Wind_hole_Range = 1f;
    public float Wind_hole_Speed = 1f;
    [Header("소용돌이")]
    public float Wind_Tornado_Damage = 1f;
    public float Wind_Tornado_Range = 1f;
    public int Wind_Tornado_Count = 1;
    [Header("바람칼날")]
    public float WindBlade_Damage = 1f;
    public float WindBlade_Range = 1f;
    public int WindBlade_Count = 1;
    [Header("바람파장")]
    public float Wind_Wave_Damage = 1f;
    public float Wind_Wave_Speed = 1f;
    public float Wind_Wave_Range = 1f;
    [Header("")]
    Bounds bounds;
    [SerializeField]
    float offsetY;
    [SerializeField]
    float offsetX;
    public Player player;

    float timer;
    public float speed;
    public float damage;
    public int count;
    public int weapon_id;
    public int bulletspeed;
    public float MassPower;
    public float Attack_Range;
    public float Attack_Duration;
    public int cloneCount;

    SkillSounds skillSounds;
    public WeaponPoolManager poolManager;
    Player_Info player_info;
    void Awake()
    {
        skillSounds = GameObject.Find("SkillSounds").GetComponent<SkillSounds>();
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
        
        player = GameObject.Find("Player").GetComponent<Player>();
        if (weapon_id == 0)
        {
            bounds = GetComponent<Collider2D>().bounds;
        }
        poolManager = GetComponentInParent<WeaponPoolManager>();

    }
    private void Start()
    {
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
            case 0:
                {
                    if (timer > speed)
                    {
                        timer = 0f;

                        Wind_hole();
                    }
                    break;
                }
            case 1:
                {
                    if (timer > speed)
                    {
                        timer = 0f;

                        Wind_Tornado();
                    }
                    break;
                }
            case 2:
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        WindBlade();
                    }
                    break;
                }
            case 3:
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        WindWave();
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
            case 0:// 바람 뭉치기
                speed = Wind_hole_Speed + player_info.Get_AttackSpeed();//생성되는 쿨타임
                damage = Wind_hole_Damage + player_info.Get_Damage();
                count = 1;//생성 개수
                MassPower = 1f;
                Attack_Range = Wind_hole_Range + player_info.Get_Attack_Range();
                Attack_Duration = player_info.Get_Attack_Duration()+2f;
                break;
            case 1:// 돌개 바람
                speed = player_info.Get_AttackSpeed()+4f;
                damage = Wind_Tornado_Damage + player_info.Get_Damage();
                count = Wind_Tornado_Count;
                bulletspeed = 5;
                MassPower = 5f;
                Attack_Range = Wind_Tornado_Range + player_info.Get_Attack_Range();
                Attack_Duration = player_info.Get_Attack_Duration();

                break;
            case 2: // 바람 칼날
                count = WindBlade_Count;
                damage = WindBlade_Damage + player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed()+2f;
                Attack_Range = WindBlade_Range + player_info.Get_Attack_Range();
                Attack_Duration = player_info.Get_Attack_Duration();
                bulletspeed = 5;
                break;
            /*
            case 3: // 바람 분열
                count = 1;
                damage = player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed();
                Attack_Range = player_info.Get_Attack_Range();
                Attack_Duration = player_info.Get_Attack_Duration();
                bulletspeed = 5;
                cloneCount = 3;
                break; 
            */

            case 3: //바람 파장 - 공격력, 쿨감만 적용
                count = 1;
                damage = player_info.Get_Damage() + Wind_Wave_Damage;
                speed = player_info.Get_AttackSpeed() + Wind_Wave_Speed;
                Attack_Range = player_info.Get_Attack_Range() + Wind_Wave_Range;
                Attack_Duration = player_info.Get_Attack_Duration();
                break;

            default:

                break;
        }
    }
    void Wind_hole()
    {

        Transform bullet = poolManager.Get().transform;

        bullet.transform.position = bounds.center + new Vector3(offsetX, offsetY);
        bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);

    }

    void Wind_Tornado()
    {
        for (int i = 0; i < count; i++)
        {
            float random = Random.Range(0, 360);//랜덤 범위 저장
            Vector3 direction = new Vector2(Mathf.Cos(random), Mathf.Sin(random));
            skillSounds.SkillSoundPlay(SkillSounds.Sfx.Tornado);
            Transform bullet = poolManager.Get().transform;
            bullet.position = player.transform.position + (direction.normalized * Attack_Range);
            bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletspeed;
        }

    }

    void WindBlade()//원거리무기 투사체를 상대방의 위치로 이동하는 함수
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
                bullet.rotation = Quaternion.FromToRotation(Vector3.right, dir);//프리펩의 회전을 적의 방향으로 회전
                Quaternion rotation = bullet.transform.rotation;
                float angle = rotation.eulerAngles.z;
                bullet.GetComponent<WindBlade>().Init(damage, dir, bulletspeed, angle, Attack_Range); //원거리 무기에서의 count는 관통력을 의미
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
                bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
                bullet.position = player.transform.position;//투사체 위치 플레이어위치로 변경
                bullet.rotation = Quaternion.FromToRotation(Vector3.right, dir);//프리펩의 회전을 적의 방향으로 회전
                Quaternion rotation = bullet.transform.rotation;
                float angle = rotation.eulerAngles.z;
                bullet.GetComponent<WindBlade>().Init(damage, dir, bulletspeed, angle, Attack_Range);
            }
        }
    }
    /*
        void WindSplit()
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
                    bullet.rotation = Quaternion.FromToRotation(Vector3.right, dir);//프리펩의 회전을 적의 방향으로 회전
                    Quaternion rotation = bullet.transform.rotation;
                    float angle = rotation.eulerAngles.z;

                    bullet.GetComponent<WindSplit>().Init(damage, dir, bulletspeed, cloneCount, angle, Attack_Range); //원거리 무기에서의 count는 관통력을 의미
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
                    bullet.transform.localScale = new Vector3(Attack_Range * 2f, Attack_Range * 2f, Attack_Range * 2f);
                    bullet.position = player.transform.position;
                    bullet.rotation = Quaternion.FromToRotation(Vector3.right, dir);
                    Quaternion rotation = bullet.transform.rotation;
                    float angle = rotation.eulerAngles.z;

                    bullet.GetComponent<WindSplit>().Init(damage, dir, bulletspeed, cloneCount, angle, Attack_Range);
                }
            }
        }
    */
    void WindWave()
    {
        Transform bullet = poolManager.Get().transform;
        skillSounds.SkillSoundPlay(SkillSounds.Sfx.Wave);
        bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
        bullet.transform.position = player.transform.position;
        bullet.GetComponent<WindWave>().damage = damage;
    }

}

