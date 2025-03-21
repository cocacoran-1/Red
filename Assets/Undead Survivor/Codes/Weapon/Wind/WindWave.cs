using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWave : MonoBehaviour
{
    public float damage;
    public Player player;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;

    // Start is called before the first frame update
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("WindWave"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.KnockBack_distance = 15;
            enemy.onDamaged(damage);
            //collision activeCheck해서 다른 무기로 죽은 에너미를 못찾아서 생기는 오류 방지
        }
    }


}
