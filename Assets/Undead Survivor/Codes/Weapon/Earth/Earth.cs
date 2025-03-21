using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [Header("돌던지기")]
    public float Stone_Damage;//돌던지기 기본 데미지
    public int Stone_Count;  //돌던지기 개수
    public int Stone_Clone_Count; //돌던지기 복제 개수
    [Header("돌회전")]
    public float Rock_Damage ;
    public int Rock_Count;
    [Header("쉴드")]
    public float Shield ;
    public float Shield_Speed ;
    [Header("골렘")]
    public bool GolemeSummon =false;
    public int Golem_Damage ;
    public float Golem_SkillTime ;
    public int Golem_Count ;
    Golem golem;
    public Player player;
    public int weapon_id;
    public int count;
    public int bulletspeed;

    public int fireCount ;
    public int cloneCount;
    public float damage;
    float timer;
    public float speed;
    public float Attack_Range;
    public float Attack_Duration;
    public WeaponPoolManager poolManager;
    Player_Info player_info;
    void Awake()
    {

        player = GameObject.Find("Player").GetComponent<Player>();
        poolManager = GetComponent<WeaponPoolManager>();
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
        
    }
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        switch (weapon_id)
        {
            case 0:// 쉴드
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        shield();
                    }
                    break;
                }
            case 1:// 돌
                {
                    transform.Rotate(Vector3.back * speed * Time.deltaTime);
                    break;
                }
            case 2:// 돌팔매
                {
                    if (timer > speed)
                    {
                        timer = 0f;
                        Throw_rock();
                    }
                    break;
                }
            case 3:// 골램
                {
                    if (!GolemeSummon)
                    {
                        GolemeSummon = true;
                        SummonGolem();
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
            case 0://쉴드
                speed = Shield_Speed + (player_info.Get_AttackSpeed());//생성되는 쿨타임
                damage = (Shield + player_info.Get_Damage());//추가체력
                Attack_Range = player_info.Get_Attack_Range();
                Attack_Duration = player_info.Get_Attack_Duration()+2f;

                break;
            case 1://돌
                speed = player_info.Get_AttackSpeed() + 100f;
                count = Rock_Count;
                damage = Rock_Damage + player_info.Get_Damage();
                Attack_Range = player_info.Get_Attack_Range();
                Batch();
                break;
            case 2:// 돌팔매

                count = Stone_Count;
                damage = Stone_Damage + player_info.Get_Damage();
                speed = player_info.Get_AttackSpeed()+1f;
                Attack_Range = player_info.Get_Attack_Range();
                bulletspeed = 5;
                cloneCount = Stone_Clone_Count;

                break;
            case 3:// 골렘
               
                break;

            default:

                break;
        }
    }
    void shield()
    {
        Transform bullet = poolManager.Get().transform;
        bullet.transform.position = player.transform.position;
        bullet.GetComponent<Earth_shield>().Init(damage*2, Attack_Duration);
        bullet.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
    }

    public void Batch()//플레이어 주위에서 회전하는 무기 배치하는 함수
    {
        for (int index = 0; index < (count + fireCount); index++)//무기 개수와 탄개수만큼 생성
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);//무기 위치를 하위 오브젝트로 생성되게 위치 변경
            }
            else
            {
                bullet = poolManager.Get().transform;//무기 생성
                bullet.parent = transform;//무기의 상위 오브젝트 설정
            }

            bullet.localPosition = Vector3.zero;//위치 초기화
            bullet.localScale = new Vector3(Attack_Range , Attack_Range , Attack_Range );
            bullet.localRotation = Quaternion.identity;// 회전이 없음을 의미
            Vector3 rotVec = Vector3.forward * 360 * index / (count + fireCount);//생성된 무기를 일정한 각도로 나눔
            bullet.Rotate(rotVec);//회전
            bullet.Translate(bullet.up * (Attack_Range*1.8f), Space.World);//위치 변경


        }
    }

    public void Throw_rock()
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
                                                                               //Quaternion rotation = bullet.transform.rotation;


                bullet.GetComponent<Earth_ThrowRock>().Init(damage, dir, bulletspeed, cloneCount, Attack_Range); //원거리 무기에서의 count는 관통력을 의미
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
                //Quaternion rotation = bullet.transform.rotation;


                bullet.GetComponent<Earth_ThrowRock>().Init(damage, dir, bulletspeed, cloneCount, Attack_Range);
            }
        }
    }

    public void SummonGolem()
    {
        Transform bullet = poolManager.Get().transform;
        golem=bullet.GetComponent<Golem>();
        golem.Init((int)player_info.Get_Damage() + Golem_Damage, Golem_Count);
    }
    public void Golem_level_up()
    {
        golem.Init((int)player_info.Get_Damage() + Golem_Damage, Golem_Count);
    }
}
