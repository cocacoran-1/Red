using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Troll : MonoBehaviour
{
    Animator anim;
    Vector3 dir;
    public Vector3 aim;
    float speed = 20.0f;
    float stopspeed = 0.5f;
    float timer;
    float Dash_timer;
    float Skill_Time = 3f;
    bool isSkill = false;
    bool isDash = false;
    bool isrunning = false;
    bool isready = false;

    PoolManager poolManager;
    GameObject Stone_point;
    Enemy enemy;
    GameObject player;
    Rigidbody2D rigid;
    
    GameManager gameManager;
  
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<PoolManager>();
        Stone_point = GameObject.Find("Stone_point");
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
        if (isready == true)
        {
            if (player.transform.position.x < rigid.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
              

            }
            else if (player.transform.position.x > rigid.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                

            }
        }
        if (isDash)
        {
            // rigid.velocity = dir.normalized * speed;
            rigid.MovePosition(transform.position + dir.normalized * speed * Time.fixedDeltaTime);
            Dash_timer += Time.deltaTime;
           

            if (Dash_timer > 2f)
            {
                isDash = false;
               
                Dash_timer = 0;
                Debug.Log("시간초과");
                anim.speed = 1;
                rigid.velocity = Vector3.zero;
               
            }
            else if (Vector3.Distance(transform.position, aim) <= stopspeed)
            {
                isDash = false;
                
                Dash_timer = 0;

                anim.speed = 1;
                Debug.Log("도달");
                rigid.velocity = Vector3.zero;
               
            }
        }
    }

    void Start_Skill()
    {
        int random = Random.Range(0, 3);
        //Debug.Log(random);
        switch (random)
        {
            case 0://돌던지기
                {
                    Stone_Start();
                    break;
                }
            case 1://돌진
                {
                    Dash_Start();
                    break;
                }
            case 2:// 전방 바람칼날
                {
                    Wind_Attack_Start();
                    break;
                }
            default:
                break;
        }
    }

    void Stone_Start()
    {
        enemy.isDamage = true;
        anim.SetBool("isAttack1", true);
    }

    void Dash_Start()
    {
        enemy.isDamage = true;
        anim.SetBool("isAttack2", true);
    }

    void Wind_Attack_Start()
    {
        enemy.isDamage = true;
        anim.SetBool("isAttack3", true);
    }


    void Stone()
    {
        float angleStep = 360f / 12; // 총알 간의 각도 계산하기
        GameObject stone_effect = poolManager.GetEnemy(4);
        stone_effect.transform.SetParent(null);
        stone_effect.transform.position = Stone_point.transform.position;
        for (int i = 0; i < 12; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // 해당 총알의 각도 계산하기
            Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정

            Transform bullet = poolManager.GetEnemy(1).transform; // 총알 생성하기

            bullet.rotation = Quaternion.FromToRotation(Vector3.left, -quaternion);// 각도를 기반으로 회전값 계산하기

            //Vector2 spawnOffset = quaternion * radius;
            // bullet.transform.position = spawnOffset;
            bullet.transform.position = Stone_point.transform.position;
            bullet.transform.localScale = bullet.transform.lossyScale;
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().velocity = quaternion * 5f; // 총알 속도 적용하기

            //bullet.GetComponent<Poison_nova>().Init(damage, quaternion, bulletspeed, Attack_Range);
        }
    }
    IEnumerator Dash()
    {
        if (isrunning)
        {
            yield break;
        }
        isready = true;
        isrunning = true;
        anim.speed = 0;
        GameObject bullet = poolManager.GetEnemy(3);
        Troll_Dash_line line = bullet.GetComponent<Troll_Dash_line>();
        bullet.transform.SetParent(gameObject.transform);
        yield return new WaitForSeconds(2f);
        line.isStart = false;
        isready = false;
        yield return new WaitForSeconds(0.5f);
        dir = line.lineRenderer.GetPosition(1) - gameObject.transform.position;
        aim = line.lineRenderer.GetPosition(1);
        bullet.SetActive(false);
        isDash = true;
        
        anim.speed = 1;
        isrunning = false;
    }

    void Dash_moving()
    {
        if (isDash)
            anim.speed = 0;
    }
    void Wind_Attack()
    {
        float angleStep = 10f;
        for (int i = 0; i < 6; i++)
        {
            Vector3 direction = player.transform.position - Stone_point.transform.position;

            float angle = (-30f + i * angleStep) * Mathf.Deg2Rad; // 해당 총알의 각도 계산하기
            Vector3 projectileDirection = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg) * direction.normalized;
            // Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정

            Transform bullet = poolManager.GetEnemy(2).transform; // 총알 생성하기

            bullet.rotation = Quaternion.FromToRotation(Vector3.left, -projectileDirection);// 각도를 기반으로 회전값 계산하기

            bullet.transform.position = Stone_point.transform.position;
            bullet.transform.localScale = bullet.transform.lossyScale;
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().velocity = projectileDirection * 5f; // 총알 속도 적용하기
        }

    }
    void Skill_End()
    {
        Debug.Log("종료");
        anim.SetBool("isAttack1", false);
        anim.SetBool("isAttack2", false);
        anim.SetBool("isAttack3", false);
        enemy.isDamage = false;
        isSkill = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Player") && isDash)
        {
            isDash = false;
            Dash_timer = 0;

            anim.speed = 1;
            Debug.Log("도달");
            rigid.velocity = Vector3.zero;
        }
    }
}
