using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_ThrowRock_clone : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    Rigidbody2D rigid;
    public bool ishit = false;
    Animator anim;
    Collider2D coll;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
       
    }
 
    private void OnEnable()
    {

        coll.enabled = false;
        ishit = false;
        anim.SetBool("ishit", false);
        StartCoroutine(coll_on());
     
    }

    // Update is called once per frame
    void Update()
    {
        if (ishit)
        {
            rigid.velocity = Vector3.zero;

        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("HitRockBullet"))
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(float damage, int bulletSpeed)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = (damage *0.5f);//기존 무기의 데미지보다 일정부분 감소되서 적용, 현재 int이기때문에 정수 기입, float로 바꿀시 퍼센트 형식으로 감소
        this.bulletSpeed = bulletSpeed;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            anim.SetBool("ishit", true);
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
    IEnumerator coll_on()
    {
        yield return new WaitForSeconds(0.1f);
        coll.enabled = true;
    }
}
