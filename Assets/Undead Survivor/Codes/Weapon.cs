using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static int fireCount = 1;    //탄 개수
    public int num;
    public FloatingJoystick Joystick;
    public int randomScan;
    public int prefabId;
    public int damage;
    public int count;//근접무기 일때는 무기의 개수,원거리 무기일때는 관통력의미
    public float speed;//근접무기 일때는 무기속도,원거리 무기 일때는 연사속도
    public float bulletSpeed;//총알이 날아가는 속도
    public int cloneCount;
    public float particlesize = 1f;
    public GameObject particlePrefab;
    ParticleSystem Ps;
    Transform targetTransform;
    Transform targetTransform_next;

    float timer;
    public int per;

    Vector3 targetPos;  //타겟위치
    Vector3 dir;    //방향
    Vector3 Randomtarget;
    public Player player;
    public WeaponPoolManager poolManager;
    GameManager gameManager;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponentInParent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        poolManager = GetComponentInParent<WeaponPoolManager>();

    }
    void Start()
    {
        Init();
    }
    void Update()
    {
        if (!gameManager.isLive)
        {
            return;
        }
        num = fireCount;
        switch (prefabId)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);//프리펩이 0일때 계속 회전

                break;
            case 1:
                //transform.Rotate(Vector3.back * speed * Time.deltaTime);

                break;
            case 2:
                timer += Time.deltaTime;

                if (timer > speed)//타이머의 시간이 스피드 보다 많아지면 초기화후 Fire함수 호출
                {
                    timer = 0f;
                    Fire();
                }
                break;
            case 3:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            case 4:

                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            case 5:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Thunder();
                }
                break;
            case 6:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                }
                break;
            default:

                break;
        }

    }


    public void LevelUp(int damage, int count)//레벨업 테스트 코드 
    {
        this.damage += damage;
        if (prefabId == 3)
            this.cloneCount += count;
        else
            this.count += count;

        if (prefabId == 0)
            Batch();
    }

    public void Init()//각 무기 번호에 따라서 무기 기본정보 입력하는 함수
    {

        switch (prefabId)
        {
            case 0:
                speed = 150;
                damage = 5;
                //prefabId = 0;
                count = 0;
                bulletSpeed = 0f;
                cloneCount = 0;
                Batch();

                break;
            case 1:
                speed = 150;
                damage = 5;
                //  prefabId = 1;
                bulletSpeed = 0f;
                cloneCount = 0;
                // Filp();
                break;
            case 2:
                speed = 0.3f;
                damage = 5;
                //  prefabId = 2;
                count = 2;
                bulletSpeed = 5f;
                cloneCount = 0;
                break;
            case 3:
                speed = 1.5f;
                damage = 5;
                //   prefabId = 3;
                count = 0;
                bulletSpeed = 15f;
                cloneCount = 4;
                break;
            case 4:
                speed = 1.5f;
                damage = 5;
                //  prefabId = 4;
                count = 0;
                bulletSpeed = 3f;
                cloneCount = 0;
                break;
            case 5:
                speed = 1.5f;
                damage = 5;
                //  prefabId = 4;
                count = 3;
                bulletSpeed = 0f;
                cloneCount = 0;
                break;
            case 6:
                speed = 1.5f;
                damage = 5;
                //  prefabId = 4;
                count = 5;
                bulletSpeed = 3f;
                cloneCount = 0;
                break;
            default:

                break;
        }
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
            bullet.localRotation = Quaternion.identity;// 회전이 없음을 의미
            Vector3 rotVec = Vector3.forward * 360 * index / (count + fireCount);//생성된 무기를 일정한 각도로 나눔
            bullet.Rotate(rotVec);//회전
            bullet.Translate(bullet.up * 1.5f, Space.World);//위치 변경

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero, bulletSpeed, cloneCount);//-1 은 무한 관통 ,마지막 0은 원거리 무기일때 투사체의 속도이므로 근접무기는 어떤숫자써도 상관없음
        }
    }

    /* void Filp()//근거리 무기 플레이어 이동방향에 따라 위치 변경하는 함수
     {

         Transform bullet;


         bullet = GameManager.instance.W_pool.Get(prefabId).transform;
         bullet.parent = transform;




         bullet.localPosition = Vector3.zero;
         bullet.localRotation = Quaternion.identity;


         bullet.Translate(bullet.right * 1.25f, Space.World);

         //  bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);//-1 은 무한 관통

     }*/

    void Fire()//원거리무기 투사체를 상대방의 위치로 이동하는 함수
    {
        if (player.scanner.sortedTargets.Length == 0)//스케너 배열이 비어있으면 리턴
            return;

        if (player.scanner.sortedTargets.Length < fireCount)// 배열이 탄 개수 보다 적으면 배열의 크기만큼만 발사
        {
            for (int i = 0; i < player.scanner.sortedTargets.Length; i++)
            {
                targetPos = player.scanner.sortedTargets[i].transform.position;//스캐너에서 감지한 배열에서 위치 정보가져옴
                dir = targetPos - player.transform.position;//타겟과 플레이어의 방향
                dir.Normalize();//벡터길이 1로 변경
                Transform bullet = poolManager.Get().transform;//투사체 생성
                bullet.position = player.transform.position;//투사체 위치 플레이어위치로 변경
                bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);//프리펩의 회전을 적의 방향으로 회전
                bullet.GetComponent<Bullet>().Init(damage, count, dir, bulletSpeed, cloneCount); //원거리 무기에서의 count는 관통력을 의미
            }
        }
        else
        {
            for (int i = 0; i < fireCount; ++i)
            {
                targetPos = player.scanner.sortedTargets[i].transform.position;
                dir = targetPos - player.transform.position;
                dir.Normalize();
                Transform bullet = poolManager.Get().transform;
                bullet.position = player.transform.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
                bullet.GetComponent<Bullet>().Init(damage, count, dir, bulletSpeed, cloneCount);
            }
        }
    }

    void Thunder()
    {

        int num = Mathf.Min(player.scanner.sortedTargets.Length, count);
        for (int i = 0; i < num; ++i)
        {

            randomScan = Random.Range(0, player.scanner.sortedTargets.Length);
            Randomtarget = player.scanner.sortedTargets[randomScan].transform.position;
            if (player.scanner.sortedTargets[randomScan].activeSelf)
            {
                Transform bullet = poolManager.Get().transform;
                bullet.position = Randomtarget;
                bullet.rotation = Quaternion.identity;

                player.scanner.sortedTargets[randomScan].SendMessage("onDamaged", damage);
            }
        }
    }





}


