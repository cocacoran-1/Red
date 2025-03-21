using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_shot_clone : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    Ice_shot_clone cloneobj;
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



    void Update()
    {
        if (ishit)
        {
            rigid.velocity = Vector3.zero;

        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Ice_shot_hit"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {

        coll.enabled = false;
        ishit = false;
        anim.SetBool("ishit", false);

    }
    public void Init(float damage, int bulletSpeed)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = (damage *0.5f);//기존 무기의 데미지보다 일정부분 감소되서 적용, 현재 int이기때문에 정수 기입, float로 바꿀시 퍼센트 형식으로 감소
        this.bulletSpeed = bulletSpeed;


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

}
