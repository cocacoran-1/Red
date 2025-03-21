using UnityEngine;

public class Big_Cultist : MonoBehaviour
{
    Animator anim;
    float timer;
    float Skill_Time = 4f;
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
        Shot_point = GameObject.Find("Axe_point");
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
    void Axe_Attack()
    {
        Vector3 direction = player.transform.position - Shot_point.transform.position;
        Transform bullet = poolManager.GetEnemy(1).transform; // 총알 생성하기
        bullet.transform.position = Shot_point.transform.position;
        bullet.transform.localScale = bullet.transform.lossyScale;
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * 5f; // 총알 속도 적용하기
    }
    void Axe_Attack_45()
    {
        Vector3 direction = player.transform.position - Shot_point.transform.position;
        Transform bullet = poolManager.GetEnemy(1).transform; // 총알 생성하기
        bullet.GetComponent<Big_Cultist_Axe>().issecond = true;
        bullet.transform.position = Shot_point.transform.position;
        bullet.transform.localScale = bullet.transform.lossyScale;
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * 5f; // 총알 속도 적용하기
    }
    void Skill_End()
    {
        anim.SetBool("isAttack", false);
        enemy.isDamage = false;
        isSkill = false;
    }
}
