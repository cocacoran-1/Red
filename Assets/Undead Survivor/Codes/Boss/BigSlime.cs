using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlime : MonoBehaviour
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

    public float health;
    float speed = 20.0f;
    float stopspeed = 0.5f;
    float timer;
    float Dash_timer;
    float Skill_Time = 3f;
    int slimeCount = 2;
    bool isSkill = false;
    bool isDash = false;
    bool isrunning = false;
    bool isReady = false;
    bool isPlayer = false;
    bool isDead = false;
    public float range_X;
    public float range_y;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<PoolManager>();
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player");
        pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        sp = GameObject.Find("CameraCollider").transform.Find("Spawner").GetComponentInChildren<Spawner>();
    }
    void Start()
    {
        sp.SlimeCount = 1;
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
                Dash_Start();
            }
        }
        if (isReady == true)
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
        if (isDash && !isDead)
        {
            // rigid.velocity = dir.normalized * speed;
            rigid.MovePosition(transform.position + dir.normalized * speed * Time.fixedDeltaTime);
            Dash_timer += Time.deltaTime;

            if (Dash_timer > 1.5f)
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

    void Dash_Start()
    {
        enemy.isDamage = true;
        anim.SetBool("isDash", true);
    }
    IEnumerator Dash()
    {
        if (isrunning || isPlayer || isDead)
        {
            yield break;
        }
        isReady = true;
        isrunning = true;
        anim.speed = 0;
        GameObject bullet = poolManager.GetEnemy(1);
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
    void Dash_moving()
    {
        if (isDash)
            anim.speed = 0;
    }
    void Division()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
        anim.speed = 0;
        health = enemy.health;
        if (health <= 0)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            enemy.isDamage = true;
            for (int i = 0; i < slimeCount; i++)
            {
                range_X = coll.size.x;
                range_y = coll.size.y;
                range_X = Random.Range(-range_X / 2, range_X / 2);
                range_y = Random.Range(-range_y / 2, range_y / 2);
                GameObject bullet = pool.GetEnemy(15);
                bullet.transform.position = new Vector3(range_X, range_y) + gameObject.transform.position;
                bullet.transform.SetParent(GameObject.Find("PoolManager").transform);
            }
        }
        anim.speed = 1;
        sp.SlimeCount -= 1;
        Debug.Log(sp.SlimeCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Player") && isDash)
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
        isDead = false;
    }
}
