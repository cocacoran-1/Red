using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [Header("번개")]
    public float Thunder_Damage = 1f;
    public int Thunder_Count = 1;
    [Header("레이저")]
    public float Beam_Damage = 1f;
    public float Beam_Speed = 1f;
    public float Beam_Range = 1f;
    [Header("번개 다발")]
    public float LightningQ_Damage = 1f;
    public float LightningQ_Speed = 1f;
    [Header("체인 라이트닝")]
    public int Lightning_Chain_Count = 3;
    public float Lightning_Chain_Damage = 1f;
    public float chainDuration = 0.5f;
    public int weapon_id;
    public int count;
    public float damage;
    public float Attack_Range;
    public float Attack_Duration;
    public int target_num;
    int randomScan;
    public float timer;
    public float angle;
    public float speed;
    public Player player;
    Player_Info player_info;
    Vector2 spawnOffset;
    CircleCollider2D circle;
    SpriteRenderer spriter;
    float radius;
    Vector3 playerDir;
    Vector3 lastPlayerDir;
    public FloatingJoystick joystick;
    SkillSounds skillSounds;
    public WeaponPoolManager poolManager;
    private void Awake()
    {
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();

        player = GameObject.Find("Player").GetComponentInParent<Player>();
        poolManager = GetComponentInParent<WeaponPoolManager>();
        joystick = (FloatingJoystick)GameObject.Find("FloatingJoystick").GetComponent<Joystick>();
        skillSounds = GameObject.Find("SkillSounds").GetComponent<SkillSounds>();


        if (weapon_id == 1 || weapon_id == 2)
        {
            circle = GetComponent<CircleCollider2D>();
            radius = circle.radius;
        }
    }
    private void Start()
    {
        Init();
    }


    void Update()
    {
        playerDir = player.joystickDirection.normalized;

        timer += Time.deltaTime;
        switch (weapon_id)
        {
            case 0:// 번개
                {
                    if (timer > speed)
                    {
                        timer = 0f;

                        Thunder();
                    }
                    break;
                }
            case 1:// 레이저
                {
                    if (timer > speed)
                    {
                        timer = 0f;

                        Beam();
                    }
                    break;
                }
            case 2:// 스웨인Q
                {

                    if (timer > speed)
                    {
                        timer = 0f;

                        LightningQ();
                    }
                    break;
                }
            case 3:// 체인라이트닝
                {

                    if (timer > speed)
                    {
                        timer = 0f;
                        ChainLightning();


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
            case 0:
                count = Thunder_Count;
                damage = Thunder_Damage + player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed()+2f;

                break;
            case 1:
                damage = Beam_Damage + player_info.Get_Damage();
                speed = Beam_Speed + player_info.Get_AttackSpeed();
                Attack_Duration = player_info.Get_Attack_Duration()+2f;
                Attack_Range = Beam_Range + player_info.Get_Attack_Range();
                break;

            case 2:
                damage = LightningQ_Damage + player_info.Get_Damage();
                speed = LightningQ_Speed + player_info.Get_AttackSpeed();
                Attack_Duration = player_info.Get_Attack_Duration();
                Attack_Range = player_info.Get_Attack_Range();

                break;
            case 3:
                damage = player_info.Get_Damage() + Lightning_Chain_Damage;
                speed = player_info.Get_AttackSpeed()+2f;
                Attack_Duration = player_info.Get_Attack_Duration();
                Attack_Range = player_info.Get_Attack_Range();
                count = Lightning_Chain_Count;
                break;

            default:

                break;
        }
    }
    void Thunder()
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
                bullet.position = randomTarget;
                bullet.rotation = Quaternion.identity;
                bullet.GetComponent<Lightning_Thunder>().Init(damage);
                bullet.GetComponent<Lightning_Thunder>().target_transform(player.scanner.sortedTargets[randomScan]);
                //player.scanner.sortedTargets[randomScan].SendMessage("onDamaged", damage);
            }
        }
    }

    void Beam()
    {
        spawnOffset = Vector2.right * radius;
        Transform bullet = poolManager.Get().transform;
        skillSounds.SkillSoundPlay(SkillSounds.Sfx.Ray);
        bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
        bullet.transform.position = spawnOffset;
        bullet.transform.position += player.transform.position;
        angle = player.angle + 90f;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void LightningQ()   //Forked_Lightning
    {
        Transform bullet = poolManager.Get().transform;
        bullet.GetComponent<LightningQ>().init(damage);
        bullet.transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);

        if (joystick.isMove == true) //움직이면
        {
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, playerDir); //조이스틱 방향벡터로 회전
        }
        else if (joystick.isMove == false)   //움직이지 않으면
        {

            if (!playerDir.Equals(Vector3.zero)) //마지막에 쏜 방향대로 발사
            {
                bullet.rotation = Quaternion.FromToRotation(Vector3.up, playerDir);
            }
            else
            {
                playerDir = Vector3.right;
                bullet.rotation = Quaternion.FromToRotation(Vector3.up, playerDir);
            }


        }
        spawnOffset = (playerDir) * radius;
        bullet.transform.position = spawnOffset;
        bullet.transform.position += player.transform.position;
    }

    private void ChainLightning()
    {
        List<Transform> targetList = FindTargets();

        if (targetList.Count > 0)
        {
            Transform previousTarget = player.transform;
            for (int i = 0; i < targetList.Count; i++)
            {
                if(!targetList[i].gameObject.activeSelf)
                {
                    continue;
                }
                GameObject bullet = poolManager.Get();
                Lightning_Chain lightning_Chain = bullet.GetComponent<Lightning_Chain>();
                lightning_Chain.damage = damage;
                StartCoroutine(lightning_Chain.Connect(previousTarget, targetList[i]));
                previousTarget = targetList[i];
            }
        }
        targetList.Clear();
    }



    private List<Transform> FindTargets()
    {
        List<Transform> targetList = new List<Transform>();
        targetList.Clear();
        for (int i = 0; i < player.scanner.sortedTargets.Length && i < count; i++)
        {
            targetList.Add(player.scanner.sortedTargets[i].transform);
        }

        return targetList;
    }

}
