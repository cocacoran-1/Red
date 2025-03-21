using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSlime : MonoBehaviour
{
    Animator anim;
    float timer;
    float Skill_Time = 5f;
    bool isSkill = false;
    bool readyAttack = false;   
    public float health;
    bool isSlimeDead = false;


    public bool isSummon = false;
    PoolManager poolManager;
    GameObject Stone_point;
    GameObject Cast_point;
    GameObject Smash_point;
    Enemy enemy;
    GameObject player;
    Rigidbody2D rigid;
    public Rigidbody2D target;
    public bool isLive = true; // 몬스터 생존 여부
    public bool isTrigger = false;
    bool isSmashUp = false;
    bool isSmashDown = false;
    public float speed; // 몬스터 이동속도
    SpriteRenderer Spriter;
    Collider2D coll;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        poolManager = GetComponent<PoolManager>();
        Stone_point = GameObject.Find("Stone_point");
        Cast_point = GameObject.Find("Cast_point");
        Smash_point = GameObject.Find("Smash_point");
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {

    }
    
    void LateUpdate()
    {
        
    }
    void Start() {

    }
    
    void Update()
    {
        Transform();

        if (readyAttack == true)
        {
            if (!isSkill)
            {
                timer += Time.deltaTime;
            }
            else if(isSkill)
            {
                return;
            }

            if (timer > Skill_Time)
            {
                timer = 0f;
                isSkill = true;
                Attack();
            }
        }


    }

    void Transform()
    {
        health = enemy.health;
        if(isSlimeDead == false){
            if(health <= 0)
            {
                isSlimeDead = true;
                enemy.health = 1000; //본체 피통
                coll.enabled = false;
                enemy.isDamage = true; 
                anim.Play("Demon_Slime_Transform");
                rigid.velocity = Vector2.zero;  
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Demon_Slime_Transform"))
        {
            rigid.simulated = true;
            coll.enabled = true;
            enemy.isDamage = false; //다시 움직이게
            anim.Play("Demon_Slime_Walk");
            readyAttack = true;
        }
    }

    void Attack()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0://아래베기
                {
                    Cleave();
                    break;
                }
            case 1://캐스트스펠
                {
                    CastSpell();
                    break;
                }
            case 2://쿵찍기
                {
                    SmashUp();
                    break;
                }
            default:
                break;
        }
    }

    void Cleave()
    {
        enemy.isDamage = true;
        anim.SetBool("isCleave", true);
    }

    void CastSpell()
    {
        enemy.isDamage = true;
        anim.SetBool("isCastSpell", true);
    }

    void SmashUp()
    {
        isSmashUp = true;
        isSmashDown = false;
        enemy.isDamage = true;
        anim.SetBool("isSmashUp", true);

        Invoke("SmashDown", 3f);
    }

    void SmashDown()
    {
        Vector2 playerPosition = player.transform.position;
        isSmashUp = false;
        isSmashDown = true;
        enemy.isDamage = true;
        if(isSmashDown == true)
        {
            gameObject.transform.position = playerPosition;

        }
        else
        {
            return;
        }
        anim.SetBool("isSmashDown", true);
        //gameObject.transform.localScale = gameObject.transform.lossyScale;
        
    }

    /* 
    void FireBreath()
    {
        enemy.isDamage = true;
        anim.SetBool("isFireBreath", true);
    }
    */

    void Smash_Attack()
    {
        float angleStep = 360f / 12; // 총알 간의 각도 계산하기

        for (int i = 0; i < 12; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // 해당 총알의 각도 계산하기
            Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정

            Transform bullet = poolManager.GetEnemy(0).transform; // 총알 생성하기
           
            bullet.rotation = Quaternion.FromToRotation(Vector3.left, -quaternion);// 각도를 기반으로 회전값 계산하기

            bullet.transform.position = Smash_point.transform.position;
            bullet.transform.localScale = bullet.transform.lossyScale;
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().velocity = quaternion * 5f; // 총알 속도 적용하기
        }
    }

    void Cleave_Attack()
    {
        float angleStep = 10f;
        for (int i = 0; i < 6; i++)
        {
            Vector3 direction = player.transform.position- Stone_point.transform.position;

            float angle =(-30f+ i * angleStep )* Mathf.Deg2Rad; // 해당 총알의 각도 계산하기
            Vector3 projectileDirection = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg) * direction.normalized;

            Transform bullet = poolManager.GetEnemy(2).transform;

            bullet.rotation = Quaternion.FromToRotation(Vector3.left, -projectileDirection);

            bullet.transform.position = Stone_point.transform.position;
            bullet.transform.localScale = bullet.transform.lossyScale;
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().velocity = projectileDirection * 5f;
        }

    }

    void Casting()
    {
        Transform bullet = poolManager.GetEnemy(1).transform;
        
        Vector3 dir = Cast_point.transform.position - player.transform.position;//타겟과 플레이어의 방향
        dir.Normalize();//벡터길이 1로 변경

        bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
        
        bullet.position = Cast_point.transform.position;
        bullet.transform.localScale = bullet.transform.lossyScale;
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody2D>().velocity = (-dir) * 10f; // 총알 속도 적용하기
    }

    void Skill_End()
    {
        anim.SetBool("isCleave",false);
        anim.SetBool("isCastSpell", false);
        anim.SetBool("isSmashUp", false);
        anim.SetBool("isSmashDown", false);
        anim.SetBool("isWalk", true);
        enemy.isDamage = false;
        if(isSmashUp == true){
            isSkill = true;
        }
        isSkill = false;
    }

}