using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSplit_Clone : MonoBehaviour
{
    public float damage;
    int bulletSpeed;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    WindSplit_Clone cloneobj;
    private Vector3 dir;
    public WeaponPoolManager poolManager;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<WeaponPoolManager>();
    }
    void Start()
    {
        Invoke("Exit", 1f);
     
    }



    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Exit", 1f);
    }
    public void Init(float damage, int bulletSpeed)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.StoponDamaged(damage);

        }
    }

    void Exit()
    {
        gameObject.SetActive(false);
    }
}
