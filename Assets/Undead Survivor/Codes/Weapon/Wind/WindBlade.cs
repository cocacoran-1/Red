using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlade : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    float angle;
    float Attack_Range ;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    WeaponPoolManager cloneobj;
    public WeaponPoolManager poolManager;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<WeaponPoolManager>();
    }




    public void Init(float damage, Vector3 dir, int bulletSpeed, float angle, float Attack_Range)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.angle = angle;
        this.Attack_Range = Attack_Range;
        rigid.velocity = dir * bulletSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.onDamaged(damage);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("BulletRange"))
        {

            gameObject.SetActive(false);
        }
    }

    void Exit()
    {
        gameObject.SetActive(false);
    }
}
