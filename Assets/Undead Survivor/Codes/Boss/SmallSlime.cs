using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlime : MonoBehaviour
{
    Animator anim;
    Enemy enemy;
    GameObject player;
    Rigidbody2D rigid;
    PoolManager poolManager;
    PoolManager pool;
    BoxCollider2D coll;
    Vector3 dir;
    public Vector3 aim;
    Spawner sp;

    float speed = 20.0f;
    float stopspeed = 0.5f;
    float timer;
    float Dash_timer;
    float Skill_Time = 3f;
    bool isSkill = false;
    bool isDash = false;
    bool isrunning = false;
    bool isReady = false;
    bool isPlayer = false;
    public float health;
    public float currentHealth;
    int slimeCount = 2; //슬라임이 분열할때마다 나올 슬라임 수
    int divisionCount = 4;   //슬라임 총 분열횟수
    public bool divisionFinish = false;

    Vector3[] scale = {new Vector3(0.2f,0.2f,0.2f), new Vector3(0.4f,0.4f,0.4f), new Vector3(0.6f,0.6f,0.6f),
    new Vector3(0.8f,0.8f,0.8f), new Vector3(1f,1f,1f) };
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        poolManager = GetComponent<PoolManager>();
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player");
        pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        sp = GameObject.Find("CameraCollider").transform.Find("Spawner").GetComponentInChildren<Spawner>();
    }

    void Start()
    {
        health = enemy.maxHealth;
        enemy.isDamage = true;
        sp.SlimeCount += 1;
        //Debug.Log("start:" + divisionCount);
    }

    void Update()
    {
        if (!gameManager.isLive)
        {
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Slime_DivisionUp"))
        {
            enemy.isDamage = false; //다시 움직이게
            anim.SetBool("isIdle", true);
        }

        if (!isSkill)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }

        if (timer > Skill_Time && divisionCount >= 3)
        {
            timer = 0f;
            isSkill = true;
            Dash_Start();
        }

        if (isReady == true)
        {
            if (player.transform.position.x < rigid.position.x)
            {

                transform.localScale = new Vector3(-scale[divisionCount].x, scale[divisionCount].y, scale[divisionCount].z);
            }
            else if (player.transform.position.x > rigid.position.x)
            {

                transform.localScale = new Vector3(scale[divisionCount].x, scale[divisionCount].y, scale[divisionCount].z);
            }
        }

        if (isDash)
        {
            // rigid.velocity = dir.normalized * speed;
            rigid.MovePosition(transform.position + dir.normalized * speed * Time.fixedDeltaTime);
            Dash_timer += Time.deltaTime;

            if (Dash_timer > 1f)
            {
                isDash = false;
                Dash_timer = 0;
                //Debug.Log("시간초과");
                anim.speed = 1;
                rigid.velocity = Vector3.zero;
            }
            else if (Vector3.Distance(transform.position, aim) <= stopspeed)
            {
                isDash = false;
                Dash_timer = 0;
                anim.speed = 1;
                //Debug.Log("도달");
                rigid.velocity = Vector3.zero;
            }
        }
    }
    void Division()
    {
        if (divisionCount == 0)
        {
            divisionFinish = true;
            sp.SlimeCount -= 1;
            return;
        }
        else
        {
            if (divisionFinish == false)
            {
                //coll = gameObject.GetComponent<BoxCollider2D>();
                anim.speed = 0;
                currentHealth = enemy.health;
                rigid.velocity = Vector3.zero;
                if (currentHealth <= 0)
                {
                    anim.SetBool("isDead", true);
                    enemy.isDamage = true;
                    for (int k = 0; k < slimeCount; k++)
                    {
                        /*
                        range_X = coll.size.x;
                        range_y = coll.size.y;
                        range_X = Random.Range(-range_X / 2, range_X / 2);
                        range_y = Random.Range(-range_y / 2, range_y / 2);
                        */
                        GameObject bullet = pool.GetEnemy(15);
                        bullet.transform.position = /*new Vector3(range_X, range_y) + */gameObject.transform.position;
                        bullet.GetComponent<SmallSlime>().divisionCount = divisionCount - 1;
                        bullet.GetComponent<Enemy>().health = health / 2;
                        bullet.GetComponent<Enemy>().maxHealth = health / 2;

                        bullet.transform.localScale = scale[bullet.GetComponent<SmallSlime>().divisionCount];
                        bullet.transform.SetParent(GameObject.Find("PoolManager").transform);
                    }
                }
                anim.speed = 1;
                sp.SlimeCount -= 1;
            }
        }
    }

    void Dash_Start()
    {
        enemy.isDamage = true;
        anim.SetBool("isDash", true);
    }
    IEnumerator Dash()
    {
        if (isrunning || isPlayer || divisionCount < 3)
        {
            yield break;
        }
        else if (divisionCount >= 3)
        {
            rigid.velocity = Vector3.zero;
            //gameObject.transform.localScale = scale[divisionCount];
            isReady = true;
            isrunning = true;
            anim.speed = 0;
            GameObject bullet = poolManager.GetEnemy(0);
            Slime_Dash_line line = bullet.GetComponent<Slime_Dash_line>();
            bullet.transform.SetParent(gameObject.transform);
            yield return new WaitForSeconds(2f);
            line.isStart = false;
            isReady = false;
            yield return new WaitForSeconds(0.5f);
            dir = line.lineRenderer.GetPosition(1) - gameObject.transform.position;
            aim = line.lineRenderer.GetPosition(1);
            bullet.SetActive(false);
            isDash = true;
            anim.speed = 1;
            isrunning = false;
        }
    }

    void Dash_moving()
    {
        if (isDash)
            anim.speed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Player") && isDash) //|| CompareTag("Enemy") && isDash)
        {
            isDash = false;
            isPlayer = true;
            Dash_timer = 0;
            anim.speed = 1;
            //Debug.Log("도달");
            rigid.velocity = Vector3.zero;
        }
    }

    void Skill_End()
    {
        //Debug.Log("종료");
        anim.SetBool("isDash", false);
        enemy.isDamage = false;
        isSkill = false;
        isPlayer = false;
    }
}
