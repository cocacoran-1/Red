using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_skull : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    float Attack_Range;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    WeaponPoolManager cloneobj;
    public WeaponPoolManager poolManager;
    Player player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<WeaponPoolManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Init(float damage, Vector3 dir, int bulletSpeed, float Attack_Range)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.Attack_Range = Attack_Range;
        rigid.velocity = dir * bulletSpeed;
    }

    private void Update()
    {
        /* if (player.transform.position.x < rigid.position.x)
        {
            transform.localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
        }
        else if (player.transform.position.x > rigid.position.x)
        {
            transform.localScale = new Vector3(-Attack_Range, Attack_Range, Attack_Range);
        } */
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.onDamaged(damage);
        }
    }*/
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
