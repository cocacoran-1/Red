using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_ball : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    float angle;
    float Attack_Range;
    public Vector2 velocity;

    public Vector2 dir;
    Rigidbody2D rigid;
    Collider2D coll;
    Ice ice;
    WeaponPoolManager cloneobj;
    public WeaponPoolManager poolManager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        ice = GetComponentInParent<Ice>();
        poolManager = GetComponent<WeaponPoolManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.back*360f*Time.deltaTime);
        velocity = rigid.velocity;  //CameraReflect에 쓰는 velocity
        
    }


    public void Init(float damage, Vector2 dir, int bulletSpeed, float angle, float Attack_Range)   //무기에 데미지와 ,관통력 정보 입력
    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.angle = angle;
        this.Attack_Range = Attack_Range;
        rigid.velocity = dir * bulletSpeed;
    }

    public void iceballRange()
    {
        gameObject.transform.localScale = new Vector3(ice.Attack_Range, ice.Attack_Range, ice.Attack_Range);
    }
    /*private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.onDamaged(damage);
        }
    }*/
}
