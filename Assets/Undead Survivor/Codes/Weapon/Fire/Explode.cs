using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;


    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    Explode cloneobj;
    public WeaponPoolManager poolManager;
    public bool ishit = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<WeaponPoolManager>();
    }


    void Update()
    {
        rigid.velocity = Vector3.zero;

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Explosion"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {

        coll.enabled = true;

    }
    public void Init(float damage) //무기에 데미지
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
           
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.onDamaged(damage);
        }
        
    }
    void Exit()
    {
        gameObject.SetActive(false);
    }
}
