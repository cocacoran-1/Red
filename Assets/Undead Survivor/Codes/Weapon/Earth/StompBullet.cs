using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBullet : MonoBehaviour
{
    public float damage;
    float angle;
    Rigidbody2D rigid;
    public List<Collider2D> colliders;
    Animator anim;
    WeaponPoolManager cloneobj;
    Vector3 dir;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void SetActiveCollider(int index)
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            if (i == index)
            {
                colliders[i].enabled = true;
            }
            else
            {
                colliders[i].enabled = false;
            }
        }
    }

    public void Init(float damage, Vector3 dir)
    {
        this.damage = damage;
        this.dir = dir;
    }
    public void BulletInactive()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.onDamaged(damage);
        }
    }
}
