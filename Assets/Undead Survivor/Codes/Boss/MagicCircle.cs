using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
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
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Summons_End"))
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator Summon_color()
    {
        float num = 0;

        while (num < 1f)
        {

            num += 0.1f;
            Boss.spriter.color = new Color(0, 0, 0, num);
            yield return new WaitForSeconds(0.1f);
        }
        float num2 = 0;
        while (num2 < 1f)
        {

            num2 += 0.1f;
            Boss.spriter.color = new Color(num2, num2, num2, num);
            yield return new WaitForSeconds(0.1f);
        }
        if (num >= 1f)
        {
            Boss.spriter.color = new Color(1, 1, 1, 1f);
        }
        yield return new WaitForSeconds(1f);
        enemy.isTrigger = false;
        enemy.EnemyAttackCapsule.enabled = true;
        enemy.anim.SetBool("isSummon", true);
        anim.SetBool("isEnd", true);
        enemy.isSummon = true;
    }
}
