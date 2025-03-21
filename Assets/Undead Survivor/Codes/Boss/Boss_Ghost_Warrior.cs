using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Boss_Ghost_Warrior : MonoBehaviour
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
    GameObject Shot_point;
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
        Dash_Start();
    }



    void Dash_Start()
    {
        enemy.isDamage = true;
        anim.SetBool("isAttack", true);
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
        GameObject bullet = poolManager.GetEnemy(1);
        Ghost_Warrior_Dash_line line = bullet.GetComponent<Ghost_Warrior_Dash_line>();
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

    void Dash_End()//종료후에 투사체 발사
    {
        Debug.Log("종료");

        anim.SetBool("isAttack", false);
        anim.SetBool("isAttack1", true);

    }
    void Skill_End()//종료후에 투사체 발사
    {
        Debug.Log("종료");


        anim.SetBool("isAttack1", false);
        enemy.isDamage = false;
        isSkill = false;
    }

    void Slash()
    {

        Vector3 direction = player.transform.position - Shot_point.transform.position;



        // Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정

        Transform bullet = poolManager.GetEnemy(2).transform; // 총알 생성하기

        bullet.rotation = Quaternion.FromToRotation(Vector3.left, -direction);// 각도를 기반으로 회전값 계산하기


        bullet.transform.position = Shot_point.transform.position;
        bullet.transform.localScale = bullet.transform.lossyScale;
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * 5f; // 총알 속도 적용하기





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
