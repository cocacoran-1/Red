using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : MonoBehaviour
{
    Animator anim;
    float timer;
    float Skill_Time = 5f;
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
        Shot_point = GameObject.Find("FireShot_Point");
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
        StartCoroutine(Fire_Attack());
    }
    IEnumerator Fire_Attack()
    {
        Vector3 direction = player.transform.position - Shot_point.transform.position;
        Quaternion lookRotation = Quaternion.FromToRotation(Vector3.left, direction);
        float currentAngle = lookRotation.eulerAngles.z;

        for (int i = 0; i < 30; i++)
        {

            float random = Random.Range(-30f, 30f);//랜덤 범위 저장
            float angleInRadians = (currentAngle + random) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            dir *= -1f;
            // Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정

            Transform bullet = poolManager.GetEnemy(1).transform; // 총알 생성하기
            bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            bullet.transform.position = Shot_point.transform.position;
            bullet.transform.localScale = bullet.transform.lossyScale;
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * 5f; // 총알 속도 적용하기

            yield return new WaitForSeconds(0.1f);
        }

        anim.SetBool("isAttack", false);
        enemy.isDamage = false;
        isSkill = false;
    }
    void Skill_End()
    {
        Debug.Log("종료");
        anim.SetBool("isAttack", false);
        enemy.isDamage = false;
        isSkill = false;
    }
}
