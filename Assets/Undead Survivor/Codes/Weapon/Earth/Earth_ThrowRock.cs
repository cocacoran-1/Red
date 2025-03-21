using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_ThrowRock : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    public int cloneCount;
    public float Attack_Range;
    public int per = 1;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    WeaponPoolManager cloneobj;
    public WeaponPoolManager poolManager;
    public bool ishit = false;

    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<WeaponPoolManager>();
    }
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back*360f*Time.deltaTime);
        if (ishit)
        {
            rigid.velocity = Vector3.zero;

        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("HitRockBullet"))
        {
            
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        per=1;
        coll.enabled = true;
        ishit = false;
        anim.SetBool("ishit", false);
       
    }
    public void Init(float damage, Vector3 dir, int bulletSpeed, int cloneCount,float attact_range)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.cloneCount = cloneCount;
        this.Attack_Range = attact_range;
        rigid.velocity = dir * bulletSpeed;
    }

    void oncollider()
    {
        coll.enabled = true;
    }

    void hit()
    {
        coll.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy"))
        { 
            --per;
            if (per == 0)
            {
                coll.enabled = false;
                StartCoroutine(HitRockBullet());
                Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Earth_ThrowRock_clone");//클론 웨폰 오브젝트 찾기
                if (ishit)
                {
                    if (obj.gameObject.activeSelf == false)//클론웨폰 오브젝트가 비활성화 상태
                    {
                        obj.gameObject.SetActive(true);
                        cloneobj = GameObject.Find("Earth_ThrowRock_clone").GetComponent<WeaponPoolManager>();
                    }
                    else//활성화 상태
                    {
                        cloneobj = GameObject.Find("Earth_ThrowRock_clone").GetComponent<WeaponPoolManager>();
                    }
                    for (int i = 0; i < cloneCount; ++i)//변수의 숫자 만큼 클론 무기를 생성해서 무작위 방향으로 발사  여기서 클론 카운트만큼 풀링오브젝트후 발사
                    {
                        Vector2 position = collision.transform.position;//충돌한 위치 저장

                        float random = Random.Range(0, 360);//랜덤 범위 저장

                        Vector2 direction = new Vector2(Mathf.Cos(random), Mathf.Sin(random));//랜덤 범위의 수만큼 각도 설정


                        Transform clone = cloneobj.Get().transform;//클론 생성
                        clone.transform.localScale = new Vector3(Attack_Range*0.5f, Attack_Range*0.5f, Attack_Range*0.5f);
                        clone.position = position;//클론 위치를 충돌한 위치로 변경
                        clone.rotation = Quaternion.identity;
                        clone.GetComponent<Rigidbody2D>().velocity = direction * (bulletSpeed + 10);//설정된 각도로 bulletSpeed만큼의 속도로 발사
                        clone.GetComponent<Earth_ThrowRock_clone>().Init(damage, bulletSpeed);//CloneBullet에 데미지와 발사 속도 전송
                    }
                }
            }
        }
        

        IEnumerator HitRockBullet()
        {
            anim.SetBool("ishit", true);
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("HitRockBullet"))
            {
                yield return null;
            }
            ishit = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BulletRange"))
        {
           
            gameObject.SetActive(false);

        }
    }
}
