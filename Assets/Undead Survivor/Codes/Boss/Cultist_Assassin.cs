using UnityEngine;

public class Cultist_Assassin : MonoBehaviour
{
    Animator anim;


    float timer;

    float Skill_Time = 3f;
    bool isSkill = false;


    PoolManager poolManager;
    GameObject Shot_point;
    Enemy enemy;

    Player player;


    private void Awake()
    {


        anim = GetComponent<Animator>();
        poolManager = GetComponent<PoolManager>();
        Shot_point = GameObject.Find("AssassinShot_point");
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    void Update()
    {
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

    void Hide_Skill()
    {
        anim.speed = 0;
        if (player.joy_x < 0)
        {
            gameObject.transform.position = player.transform.position + new Vector3(3, 0, 0);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        else
        {
            gameObject.transform.position = player.transform.position + new Vector3(-3, 0, 0);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        anim.speed = 1;

    }
    void Hide_Skill_End()
    {
        anim.speed = 0;
        if (player.joy_x < 0)
        {
            gameObject.transform.position = player.transform.position + new Vector3(3, 0, 0);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        else
        {
            gameObject.transform.position = player.transform.position + new Vector3(-3, 0, 0);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        anim.speed = 1;
        anim.Play("Assassin_Cultist_HideSkill_on");
    }


    void Wind_Attack()
    {

        Vector3 direction = player.transform.position - Shot_point.transform.position;
        if(direction.normalized.x>=0)
        {
            direction = new Vector3(1, 0, 0);
        }
        if (direction.normalized.x < 0)
        {
            direction = new Vector3(-1, 0, 0);
        }
      



        // Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정

        Transform bullet = poolManager.GetEnemy(1).transform; // 총알 생성하기
        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        bullet.rotation = Quaternion.FromToRotation(Vector3.left, -direction);// 각도를 기반으로 회전값 계산하기


        bullet.transform.position = Shot_point.transform.position;
        bullet.transform.localScale = bullet.transform.lossyScale;
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f; // 총알 속도 적용하기
     




    }
    void Skill_End()
    {
        Debug.Log("종료");
        anim.SetBool("isAttack", false);
        enemy.isDamage = false;
        isSkill = false;
    }
}
