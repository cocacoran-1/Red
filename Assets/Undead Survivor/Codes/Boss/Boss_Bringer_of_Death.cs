using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bringer_of_Death : MonoBehaviour
{
    Animator anim;
    float timer;
    float Skill_Time = 3f;
    bool isSkill = false;
    PoolManager poolManager;
    GameObject Shot_point;
    Enemy enemy;
    GameObject player;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<PoolManager>();
        Shot_point = GameObject.Find("Shot_point");
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (!gameManager.isLive)
        {
            return;
        }
        if (enemy.isSummon)
        {
            if (!isSkill)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f;
            }

            if (timer > Skill_Time)
            {
                timer = 0f;
                isSkill = true;
                Start_Skill();
            }
        }
    }
    void Start_Skill()
    {
        Stone_Start();
    }

    void Stone_Start()
    {
        enemy.isDamage = true;
        anim.SetBool("isAttack", true);
    }
    void Wind_Attack()
    {
        float angleStep = 10f;
        for (int i = 0; i < 6; i++)
        {
            Vector3 direction = player.transform.position - Shot_point.transform.position;

            float angle = (-30f + i * angleStep) * Mathf.Deg2Rad; // 해당 총알의 각도 계산하기
            Vector3 projectileDirection = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg) * direction.normalized;
            // Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정

            Transform bullet = poolManager.GetEnemy(1).transform; // 총알 생성하기

            bullet.rotation = Quaternion.FromToRotation(Vector3.left, -projectileDirection);// 각도를 기반으로 회전값 계산하기

            bullet.transform.position = Shot_point.transform.position;
            bullet.transform.localScale = bullet.transform.lossyScale;
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().velocity = projectileDirection * 5f; // 총알 속도 적용하기
        }
    }
    void Skill_End()
    {
        Debug.Log("종료");
        anim.SetBool("isAttack", false);
        enemy.isDamage = false;
        isSkill = false;
    }
}
