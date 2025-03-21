using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [Header("Golem")]
    public int damage = 10;
    public float moveSpeed;
    public float attackRange;
    public float followRange;
    float timer;
    float Skill_Time = 5f;
    private bool isAttacking;
    bool isSkill = false;
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    GameObject player;
    public LayerMask enemyLayer;
    public PoolManager PoolManager;
    private Vector2 facingDirection;
    public Scanner scanner;

    [Header("Stomp")]
    int count = 1;
    public Transform stompPoint;
    private bool isFliped = false;

    [Header("StoneFall")]
    int StoneCount = 1;
    public Transform throwPoint;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        PoolManager = GetComponent<PoolManager>();
        stompPoint = transform.Find("StompPoint");
        throwPoint = transform.Find("ThrowPoint");
        isAttacking = false;
    }
    private void OnEnable()
    {
        animator.SetBool("isWake", true);
        animator.SetBool("isStay", false);
        animator.SetBool("Stomp", false);
        animator.SetBool("StoneFall", false);
    }
    private void OnDisable()
    {
        animator.SetBool("isWake", true);
        animator.SetBool("isStay", false);
        animator.SetBool("Stomp", false);
        animator.SetBool("StoneFall", false);
    }
    void Update()
    {
        if (!isAttacking)
        {
            LookAtPlayer();
        }
        FollowPlayer();

        CheckForEnemies();
    }

    void FollowPlayer()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance > followRange)
        {
            if (!isAttacking)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rigid.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            }
            else if (isAttacking)
            {
                rigid.velocity = Vector2.zero;
            }
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }

    }

    void LookAtPlayer()
    {
        if (!isAttacking)
        {
            if (player.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
                isFliped = false;
                facingDirection = Vector2.right;
            }
            else
            {
                spriteRenderer.flipX = true;
                isFliped = true;
                facingDirection = Vector2.left;
            }
        }
    }


    void CheckForEnemies()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > attackRange)
        {
            return;
        }
        else if (distance <= attackRange)
        {
            if (enemiesInRange.Length > 0)
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


    }

    void Start_Skill()
    {
        int random = Random.Range(1, 2);
        //Debug.Log(random);
        switch (random)
        {
            case 0:
                {
                    Stomp_Start();
                    break;
                }
            case 1:
                {
                    Stone_Fall_Start();
                    break;
                }
            default:
                break;
        }
    }

    void Stomp_Start()
    {
        isAttacking = true;
        animator.SetBool("isStay", false);
        animator.SetBool("Stomp", true);
        animator.SetBool("StoneFall", false);
    }
    void Stone_Fall_Start()
    {
        isAttacking = true;
        animator.SetBool("isStay", false);
        animator.SetBool("Stomp", false);
        animator.SetBool("StoneFall", true);
    }
    private Quaternion CalculateRotation(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Stomp()
    {
        if (scanner.sortedTargets.Length == 0) // 스케너 배열이 비어있으면 리턴
            return;

        if (scanner.sortedTargets.Length < count) // 배열이 탄 개수보다 적으면 배열의 크기만큼만 발사
        {
            for (int i = 0; i < scanner.sortedTargets.Length; i++)
            {
                Vector3 dir = Vector3.right;
                bool flipBullet = false;

                if (isFliped)
                {
                    dir = Vector3.left;
                    flipBullet = true;
                }

                Transform bullet = PoolManager.GetEnemy(0).transform;

                bullet.position = stompPoint.position; // 투사체 생성 위치를 StompPoint로 변경
                Transform bulletContent = bullet.Find("BulletContent"); // 총알 게임 오브젝트 내부의 빈 게임 오브젝트 찾기
                if (flipBullet)
                {
                    bulletContent.localScale = new Vector3(-1, 1, 1); // 빈 게임 오브젝트를 뒤집음
                }
                else
                {
                    bulletContent.localScale = Vector3.one;
                }
                bullet.GetComponent<SpriteRenderer>().flipX = flipBullet;
                bullet.GetComponent<StompBullet>().Init(damage * 0.1f, dir);
            }
        }
        else
        {
            for (int i = 0; i < count; ++i)
            {
                Vector3 dir = Vector3.right;
                bool flipBullet = false;

                if (isFliped)
                {
                    dir = Vector3.left;
                    flipBullet = true;
                }
                Transform bullet = PoolManager.GetEnemy(0).transform;

                bullet.position = stompPoint.position; // 투사체 생성 위치를 StompPoint로 변경
                Transform bulletContent = bullet.Find("BulletContent"); // 총알 게임 오브젝트 내부의 빈 게임 오브젝트 찾기
                if (flipBullet)
                {
                    bulletContent.localScale = new Vector3(-1, 1, 1); // 빈 게임 오브젝트를 뒤집음
                }
                else
                {
                    bulletContent.localScale = Vector3.one;
                }
                bullet.GetComponent<SpriteRenderer>().flipX = flipBullet;
                bullet.GetComponentInChildren<StompBullet>().Init(damage * 0.4f, dir);
            }
        }

    }

    void StoneFall()
    {
        if (scanner.sortedTargets.Length == 0) // 스케너 배열이 비어있으면 리턴
            return;

        if (scanner.sortedTargets.Length < StoneCount) // 배열이 탄 개수보다 적으면 배열의 크기만큼만 발사
        {
            for (int i = 0; i < scanner.sortedTargets.Length; i++)
            {
                Vector3 targetPos = scanner.sortedTargets[i].transform.position;

                Transform bullet = PoolManager.GetEnemy(1).transform;

                Vector3 upwardDirection = Vector3.up;
                bullet.position = throwPoint.position;
                bullet.SetParent(null); // 자식 객체로 생성되지 않게 설정
                Quaternion rotation = CalculateRotation(upwardDirection);
                bullet.GetComponent<StoneFall>().ApplyRotation(rotation);
                bullet.GetComponent<StoneFall>().Init(damage, upwardDirection, rotation);
            }
        }
        else
        {
            for (int i = 0; i < StoneCount; ++i)
            {
                Vector3 targetPos = scanner.sortedTargets[i].transform.position;

                Transform bullet = PoolManager.GetEnemy(1).transform;

                Vector3 upwardDirection = Vector3.up;
                bullet.position = throwPoint.position;
                bullet.SetParent(null); // 자식 객체로 생성되지 않게 설정
                Quaternion rotation = CalculateRotation(upwardDirection);
                bullet.GetComponent<StoneFall>().ApplyRotation(rotation);
                bullet.GetComponent<StoneFall>().Init(damage, upwardDirection, rotation);
            }
        }
    }


    void Skill_End()
    {
        animator.SetBool("Stomp", false);
        animator.SetBool("isStay", true);
        animator.SetBool("StoneFall", false);
        isSkill = false;
        isAttacking = false;
    }
    public void StartStay()
    {
        animator.SetBool("isStay", true);
        animator.SetBool("isWake", false);
        animator.SetBool("StoneFall", false);
        animator.SetBool("Stomp", false);
    }
    public void Init(int damage, int Count)
    {
        StoneCount = Count;
        this.damage = damage;

    }
}
