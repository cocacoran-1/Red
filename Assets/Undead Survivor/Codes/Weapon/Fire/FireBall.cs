using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    public int cloneCount = 1;
    float angle;
    float Attack_Range;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    WeaponPoolManager cloneobj;
    Vector3 dir;
    public WeaponPoolManager poolManager;
    public bool isCharging = true; // 차징 여부
    private bool isFiring = false; // 발사 여부
    public MoveInCircle moveInCircle; // 발사 지점

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<WeaponPoolManager>();
        moveInCircle = GameObject.Find("MoveInCircle").GetComponent<MoveInCircle>(); // 부모 오브젝트의 MoveInCircle 컴포넌트 가져오기
    }


    private void FixedUpdate()
    {
        if (isCharging)
        {
            coll.enabled = false;

        }


        if (!isFiring)
        {
            transform.position = moveInCircle.transform.position;
        }
        else
        {
            rigid.velocity = dir * bulletSpeed;//투사체의 속도
        }

    }
    private void OnEnable()
    {
        anim.SetBool("isCharging", true);
        isCharging = true;
        coll.enabled = false;
        isFiring = false;
    }

    // 발사된 화염구 객체가 콜라이더에서 나갔을 때 호출되는 함수
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BulletRange"))
        {
            gameObject.SetActive(false);

        }
    }

    public void Init(float damage, Vector3 dir, int bulletSpeed, float Attack_Range)
    {
        
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.Attack_Range = Attack_Range;
        this.dir = dir;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {

            Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Fire_Ball_Explosion");//클론 웨폰 오브젝트 찾기


            if (obj.gameObject.activeSelf == false)//클론웨폰 오브젝트가 비활성화 상태
            {
                obj.gameObject.SetActive(true);
                cloneobj = GameObject.Find("Fire_Ball_Explosion").GetComponent<WeaponPoolManager>();

            }

            else//활성화 상태
            {
                cloneobj = GameObject.Find("Fire_Ball_Explosion").GetComponent<WeaponPoolManager>();
            }

            for (int i = 0; i < cloneCount; ++i)//변수의 숫자 만큼 클론 무기를 생성해서 무작위 방향으로 발사  여기서 클론 카운트만큼 풀링오브젝트후 발사
            {
                Vector2 position = collision.transform.position; //충돌한 위치 저장
                Transform clone = cloneobj.Get().transform; //클론 생성
                clone.transform.localScale = new Vector3(Attack_Range * 10f, Attack_Range * 10f, Attack_Range * 10f);// 범위 조정
                clone.position = position; //클론 위치를 충돌한 위치로 변경
               
                clone.GetComponent<Explode>().Init(damage);//CloneBullet에 데미지와 발사 속도 전송
            }

            gameObject.SetActive(false);
        }
    }

    public void EndCharging()
    {
        anim.SetBool("isCharging", false);
        isCharging = false;
        coll.enabled = true;
        isFiring = true;
    }
}

