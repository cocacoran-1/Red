using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningQ : MonoBehaviour
{
    public float maxDamage = 20f;
    public float maxDistance = 10f;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Forked_Lightning"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float distance = Vector2.Distance(transform.position, collision.transform.position);
        float damage = (distance / maxDistance) * maxDamage;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.onDamaged(damage);

            Debug.Log(damage);
        }
    }
    public void init(float damage)
    {
        maxDamage = damage;
    }
}

