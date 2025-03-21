using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonDemonSlime : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriter;
    Summon_Boss Boss;
    Enemy enemy;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        enemy = GetComponentInParent<Enemy>();
        Boss = GetComponentInParent<Summon_Boss>();
    }
    private void Start()
    {
        gameObject.transform.SetParent(null);
    }

    private void Update()
    {
        if (!gameManager.isLive)
        {
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Magic_Circle"))
        {
            gameObject.SetActive(false);
        }
    }
}
