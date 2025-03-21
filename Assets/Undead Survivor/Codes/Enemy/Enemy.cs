using System.Collections;
using UnityEngine;
using DamageNumbersPro;


public class Enemy : MonoBehaviour
{

    public static Enemy instance;

    public DamageNumber numberPrefab;
    public DamageNumber DotPrefab;
    [Header("Enemy Stats")]
    public float health; // 몬스터 현재 체력
    public float maxHealth; // 몬스터 최대 체력
    public float speed; // 몬스터 이동속도
    public float damage; // 몬스터 데미지
    public int spriteType;
    public int KnockBack_distance = 5;
    [Header("Fire")]
    FireProperty fireProperty; // 불속성 스킬 정리 스크립트
    public float ExplosionRadius = 1f;
    public int ExplosionDamage;
    public bool isFireZone = false; // 불장판 안에 있는지 여부
    public bool isFirePillar = false; // 불장판 안에 있는지 여부
    [Header("Parameters")]
    public int DOT_time = 0;

    public Rigidbody2D target;
    public bool isLive = true; // 몬스터 생존 여부
    public bool isTrigger = false;  //맞았을때
    public bool isDamagedZone = false;
    public bool isCoroutineRunning = false;
    public bool isIce = false;
    public bool isBeam = false;
    public bool ismass = false;
    public bool isBoss = false;
    public bool isSummon = false;
    public bool isDamage = false;   //보스 움직임 멈추는거
    public bool isBossDead = false;
    public int[] drop;
    public GameObject exp; // 몬스터 드랍 경험치
    public GameObject highExp; // 몬스터 드랍 경험치
    public GameObject coin; //몬스터 드랍 재화
    public GameObject upgrade;  //보스만 드랍 강화재료
    public GameObject box;

    PoolManager pool;

    public Collider2D EnemyAttackCapsule;  // 몬스터 공격 범위
    public Animator anim;
    Rigidbody2D Rigid;
    SpriteRenderer Spriter;
    Player player;
    WaitForFixedUpdate wait;
    Spawner spawner;
    public CanvasManager canvasManager;
    GameManager manager;
    EnemyHitSounds enemyHitSounds;
    void Awake()
    {
        fireProperty = GetComponent<FireProperty>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pool = GameObject.Find("UIPoolManager").GetComponent<PoolManager>();
        spawner = GameObject.Find("CameraCollider").transform.Find("Spawner").GetComponentInChildren<Spawner>();
        enemyHitSounds = GameObject.Find("EnemyHitSounds").GetComponent<EnemyHitSounds>();
        canvasManager = GameObject.Find("GamePlayCanvas").GetComponent<CanvasManager>();
        Rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
        EnemyAttackCapsule = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        wait = new WaitForFixedUpdate();
        if (instance == null)
            instance = this;
    }
    
    void FixedUpdate()
    {
        if (!isLive || isTrigger || isIce || isDamage)
        {
            if (isBoss)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    if (isBossDead == false)    //Death애니메이션이 실행되면서 프레임마다 Dead함수를 계속 불러와서 추가함
                    {                           //이러면 죽을때 Dead()를 한번만 실행함
                        //Debug.Log("사망");    //나중에 여기서 오류나면 각 보스의 Death 애니메이션 끝에 Dead() 이벤트 추가해보기
                        Dead();
                    }
                }
            }
            return;
        }
        else if (isTrigger)
        {

        }
        else
        {


            // 적에서 목표물로의 방향
            Vector2 dirVec = target.position - Rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            // 적이 목표물을 향해서 이동
            Rigid.MovePosition(Rigid.position + nextVec);
            // 가속도는 0으로 설정
            Rigid.velocity = Vector2.zero;
        }

    }
    void LateUpdate()
    {

        if (!isLive)
        {
            return;
        }

        if (!isBoss)
        {
            Spriter.flipX = target.position.x > Rigid.position.x;
        }

        else if (isBoss && !isTrigger && !isDamage)
        {
            if (spriteType == 15)
            {
                return;
            }
            else
            {
                if (target.position.x < Rigid.position.x)
                {

                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (target.position.x > Rigid.position.x)
                {

                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }

        }

    }
    public void EnemyAttack() // 몬스터가 플레이어와 부딪혔을 때 플레이어가 데미지를 받는다
    {
        // "Player" 태그를 가진 객체와 부딪혔을 때
        if (gameObject.tag == "Player")
        {
            // 플레이어가 데미지 받는 코루틴 실행
            StopCoroutine("TakeDamage");
            StartCoroutine("TakeDamage");
        }
    }
    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.1f);
        EnemyAttackCapsule.enabled = true;
        yield return new WaitForSeconds(0.1f);

    }
    void OnEnable()
    {
        target = player.GetComponent<Rigidbody2D>();

        // 몬스터가 생존 상태 일 때

        isLive = true;

        Rigid.simulated = true;
        Rigid.bodyType = RigidbodyType2D.Dynamic;   //이거해야 보스끼리 충돌함 지우지마샘.
        
        health = maxHealth;
        isSummon = false;
        isCoroutineRunning = false;
        isFireZone = false;
        isFirePillar = false;
        isIce = false;
        ismass = false;
        KnockBack_distance = 5;

        if (isBoss)
        {
            isBossDead = false;
            if (spriteType == 13 || spriteType == 14 || spriteType == 16 || spriteType == 18 || spriteType == 19 || spriteType == 20 || spriteType == 21)// 보스 스프라이트 타입 확인
            {
                anim.SetBool("isSummon", false);
                Spriter.color = new Color(0, 0, 0, 0);
                isTrigger = true;
            }
            Spriter.sortingOrder = 200;
            EnemyAttackCapsule.enabled = false;
            if(spriteType == 19 || spriteType == 20 || spriteType == 21 || spriteType == 18 || spriteType == 17)
            {
                player.Last_Boss.Add(gameObject);   //최종보스애들
            }

        }
        else if (!isBoss)
        {
            Spriter.sortingOrder = 100;
            isTrigger = false;
            EnemyAttackCapsule.enabled = true;
            Spriter.color = new Color(1, 1, 1, 1f);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 몬스터가 플레이어의 스킬에 맞았을 때 각 스킬이 데미지를 입히는 방식대로 몬스터가 데미지를 받는다
        if (!collision.CompareTag("Earth") && !collision.CompareTag("Ice") && !collision.CompareTag("Poison") && !collision.CompareTag("Wind")
            && !collision.CompareTag("Lightning") && !collision.CompareTag("Fire") && !isLive)
        {
            return;
        }

        // 땅속성 스킬
        if (collision.CompareTag("Earth"))
        {



            if (collision.GetComponentInParent<Earth>() == null)
            {
                if(collision.GetComponent<Earth_ThrowRock_clone>()!=null)
                onDamaged(collision.GetComponent<Earth_ThrowRock_clone>().damage);
            }
            else if(collision.GetComponentInParent<Golem>()!=null)
            {

            }
            else
            {
                onDamaged(collision.GetComponentInParent<Earth>().damage);
            }
        }

        // 얼음 속성 스킬
        if (collision.CompareTag("Ice"))
        {

            if (collision.GetComponentInParent<Ice>() == null)
            {
                Ice_onDamaged(collision.GetComponent<Ice_shot_clone>().damage, collision.transform.position);
            }
            else
            {
                Ice_onDamaged(collision.GetComponentInParent<Ice>().damage, collision.transform.position);
            }
        }

        // 독 속성 스킬
        if (collision.CompareTag("Poison"))
        {


            if (collision.GetComponentInParent<Poison>().weapon_id == 1 || collision.GetComponentInParent<Poison>().weapon_id == 2)
            {
                onDamaged(collision.GetComponentInParent<Poison>().damage);

            }
            else if(collision.GetComponentInParent<Poison>().weapon_id == 3)
            {
                onDamaged(collision.GetComponentInParent<Poison>().damage);
                StartCoroutine("Poison_Damaged", collision.GetComponentInParent<Poison>().dot_damage);

            }
        }

        // 바람 속성 스킬
        if (collision.CompareTag("Wind"))
        {
            if (ismass == false && gameObject.activeSelf)
            {
                StartCoroutine(Wind_Mass(collision.gameObject, collision.GetComponentInParent<Wind>().MassPower));
            }
            else if (collision.GetComponentInParent<Wind>().weapon_id == 1 && gameObject.activeSelf)
            {
                StartCoroutine(Wind_Mass(collision.gameObject, collision.GetComponentInParent<Wind>().MassPower));
            }
            if (gameObject.activeSelf)
            {
                StartCoroutine(Wind_Damaged(collision.GetComponentInParent<Wind>().damage));
            }
        }

        // 번개 속성 스킬
        if (collision.CompareTag("Lightning"))
        {


        }

        // 불 속성 스킬
        if (collision.CompareTag("Fire"))
        {

        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Poison"))
        {

            if (!isCoroutineRunning&& gameObject.activeSelf)
            {
                StartCoroutine("Poison_Damaged", collision.GetComponentInParent<Poison>().dot_damage);
            }

            if (isCoroutineRunning)
            {
                DOT_time = 0;
            }
        }
        if (collision.CompareTag("Wind"))
        {
            if (collision.GetComponentInParent<Wind>().weapon_id == 1 && gameObject.activeSelf)
            {
                StartCoroutine(Wind_Mass(collision.gameObject, collision.GetComponentInParent<Wind>().MassPower));
            }

        }
        if (collision.CompareTag("Lightning"))
        {
            if (gameObject.activeSelf && isBeam == false && collision.GetComponentInParent<Lightning>().weapon_id != 2)
            {

                StartCoroutine(Lightning_Damaged(collision.GetComponentInParent<Lightning>().damage));
            }

        }
        if (collision.CompareTag("Fire"))
        {
            if (collision.GetComponentInParent<Fire>().weapon_id == 2)
            {

                if (isFireZone == false)
                {
                    isFireZone = true;

                    StoponDamaged(collision.GetComponentInParent<Fire>().damage);
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(OnDamageDelay(1f));//데미지 주는 간격
                    }
                }

            }
            if (collision.GetComponentInParent<Fire>().weapon_id == 3)
            {

                if (isFirePillar == false)
                {
                    isFirePillar = true;

                    StoponDamaged(collision.GetComponentInParent<Fire>().damage);
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(OnDamageDelay(0.5f));//데미지 주는 간격
                    }
                }

            }
        }

    }
    IEnumerator OnDamageDelay(float time)
    {
        yield return new WaitForSeconds(time);
        isFireZone = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            isTrigger = false;
            ismass = false;
            StopCoroutine("Wind_Damaged");
        }

        if (collision.CompareTag("Lightning"))
        {
            isBeam = false;
            StopCoroutine("Lightning_Damaged");

        }
        if (collision.CompareTag("Fire"))
        {
            if (collision.GetComponentInParent<Fire>().weapon_id == 2)
            {
                isFireZone = false;
            }
            if (collision.GetComponentInParent<Fire>().weapon_id == 3)
            {
                isFirePillar = false;
            }
        }
    }
    public void Dead() // 몬스터가 플레이어의 공격을 맞고 죽었을 때
    {
        // 몬스터를 죽은 상태로 변경
        if (isBoss)
        {
            isBossDead = true;
        }
        isLive = false;
        isTrigger = false;
        EnemyAttackCapsule.enabled = false;
        Rigid.simulated = false;

        // 몬스터의 시체를 맵 뒤로 없애기
        //Spriter.sortingOrder = -2;

        if (isBoss)
        {
            if (spriteType == 14)  //BigSlime
            {
                spawner.isBoss = true;
            }
            else if (spriteType == 15) //SmallSlime
            {
                if (spawner.SlimeCount == 0)    //슬라임카운트가 0일떄 isBoss를 꺼서 잡몹이 나오게해야함
                {
                    spawner.isBoss = false;
                }
                else
                {
                    spawner.isBoss = true;
                }
            }
            else if (spriteType == 19 || spriteType == 21)
            {

            }
            else
            {
                spawner.isBoss = false;
            }

        }
        if (spriteType == 19 || spriteType == 20 || spriteType == 21 || spriteType == 17 || spriteType == 18)// 마지막 보스 처치시
        {
            player.Last_Boss.Remove(gameObject);
            player.start_Last_Boss_kill();
        }


        // 일반 몬스터는 확률에 따라 경험치를 드랍한다
        int ran = Random.Range(0, 100);
        if (ran < 50)   // 50% 확률로 경험치를 드랍한다
        {
            Debug.Log(ran+"경험치");
            GameObject bullet = pool.GetEnemy(0);
            bullet.transform.position = gameObject.transform.position;
        }

        if (ran < 20)   //20% 확률로 재화 얻음
        {
            Debug.Log(ran+"재화");
            canvasManager.GetCoin();
        }

        if (isBoss)  //보스인 경우에 무조건 강화재료 생김
        {
            if (ran < 100)
            {
                GameObject bullet = pool.GetEnemy(3);
                bullet.transform.position = gameObject.transform.position;
            }
        }
        StopAllCoroutines();
        gameObject.SetActive(false); // 몬스터 오브젝트 비활성화

        // 플레이어가 잡은 몬스터 수 증가
        player.killCount++;
        if(spriteType == 19 || spriteType == 20 || spriteType == 21 || spriteType == 17 || spriteType == 18)
        {
            canvasManager.playerHistory.Boss_Kill++;
            canvasManager.dailyhistory.Boss_Kill++;
        }
        else if(isBoss)
        {
            canvasManager.playerHistory.Elite_Kill++;
            canvasManager.dailyhistory.Elite_Kill++;
        }
        else
        {
            canvasManager.playerHistory.Monster_Kill++;
            canvasManager.dailyhistory.Monster_Kill++;
        }
        canvasManager.EnemyKill();
    }



    public IEnumerator KnockBack(int num)
    {

        yield return wait;
        Spriter.color = new Color(1, 0, 0, 0.6f);
        Vector3 playerPos = player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        if (!isBoss)
        {
            Rigid.AddForce(dirVec.normalized * num, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(0.1f);
        isTrigger = false;
        Spriter.color = new Color(1, 1, 1, 1f);
        KnockBack_distance = 5;
    }
    IEnumerator StopKnockBack()
    {
        yield return wait;
        Spriter.color = new Color(1, 0, 0, 0.6f);

        yield return new WaitForSeconds(0.1f);
        isTrigger = false;
        Spriter.color = new Color(1, 1, 1, 1f);
    }
    IEnumerator Wind_KnockBack()
    {
        yield return wait;
        Spriter.color = new Color(1, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.1f);
        Spriter.color = new Color(1, 1, 1, 1f);
    }
    IEnumerator Poison_KnockBack()
    {
        yield return wait;
        Spriter.color = new Color(1, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.1f);
        Spriter.color = new Color(0, 1, 0, 0.6f);
        isTrigger = false;
    }

    public void Init(EnemyData data)
    {
        spriteType = data.spriteType;
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    public void onDamaged(float damage)
    {

        if (isLive == true)
        {
            // 타격음 재생
            enemyHitSounds.EnemyHitPlay();

            // 적 현재 체력을 스킬의 데미지 만큼 감소
            health -= damage;
            DamageNumber damageNumber = numberPrefab.Spawn(transform.position + new Vector3(0, 0.5f, 0), ((float)damage));
            
            isTrigger = true;
            StartCoroutine(KnockBack(KnockBack_distance));

            if (health > 0)
            {
                KnockBack_distance = 5;
                return;
            }
            else
            {
                if (isBoss)
                {
                    isTrigger = true;
                    isDamage = true;
                    EnemyAttackCapsule.enabled = false;
                    Rigid.simulated = false;    //rigid 멈춤
                    anim.Play("Death");

                }
                else
                {
                    Dead();
                }

            }

        }
    }
    public void StoponDamaged(float damage)
    {
        if (!gameObject.activeSelf)
        {

            // 게임 오브젝트가 활성화되어 있는지 확인

            return;
        }
        // 타격음 재생
        enemyHitSounds.EnemyHitPlay();
        isDamagedZone = true;
        isTrigger = true;
        StartCoroutine(StopKnockBack());

        // 적 현재 체력을 스킬의 데미지 만큼 감소
        health -= damage;
        DamageNumber damageNumber = numberPrefab.Spawn(transform.position + new Vector3(0, 0.5f, 0), ((float)damage));
        if (health > 0)
        {
            return;
        }
        else
        {

            if (isBoss)
            {
                isDamage = true;
                isTrigger = true;
                EnemyAttackCapsule.enabled = false;
                Rigid.simulated = false;
                anim.Play("Death");
            }
            else
            {
                Dead();
            }
        }

    }
    public void Poison_onDamaged(float damage)
    {
        // 타격음 재생
        enemyHitSounds.EnemyHitPlay();

        // 적 현재 체력을 스킬의 데미지 만큼 감소
        health -= damage;
        DamageNumber damageNumber = DotPrefab.Spawn(transform.position + new Vector3(0, 0.5f, 0), ((float)damage));

        isTrigger = true;

        StartCoroutine(Poison_KnockBack());

        if (health > 0)
        {
            return;
        }
        else
        {

            if (isBoss)
            {
                isDamage = true;
                isTrigger = true;
                EnemyAttackCapsule.enabled = false;
                Rigid.simulated = false;
                anim.Play("Death");
            }
            else
            {
                Dead();
            }
        }

    }
    public void Ice_onDamaged(float damage, Vector3 transform)
    {
        // 타격음 재생
        enemyHitSounds.EnemyHitPlay();

        // 적 현재 체력을 스킬의 데미지 만큼 감소
        health -= damage;
        DamageNumber damageNumber = numberPrefab.Spawn(this.transform.position + new Vector3(0, 0.5f, 0), ((float)damage));

        isTrigger = true;
        if (gameObject.activeSelf && isIce == false)
            StartCoroutine(KnockBack(KnockBack_distance));

        if (health > 0)
        {
            return;
        }
        else
        {

            if (isBoss)
            {
                isDamage = true;
                isTrigger = true;
                EnemyAttackCapsule.enabled = false;
                Rigid.simulated = false;
                anim.Play("Death");
            }
            else
            {
                Dead();
            }
        }

    }
    public void Wind_onDamaged(float damage)
    {
        // 타격음 재생
        enemyHitSounds.EnemyHitPlay();
        // 적 현재 체력을 스킬의 데미지 만큼 감소
        health -= damage;
        DamageNumber damageNumber = numberPrefab.Spawn(transform.position + new Vector3(0, 0.5f, 0), ((float)damage));
        StartCoroutine(Wind_KnockBack());
        if (health > 0)
        {
            return;
        }
        else
        {
            if (isBoss)
            {
                isDamage = true;
                isTrigger = true;
                EnemyAttackCapsule.enabled = false;
                Rigid.simulated = false;
                anim.Play("Death");
            }
            else
            {
                Dead();
            }
        }
    }
    public IEnumerator Poison_Damaged(float damage)
    {
        DOT_time = 0;
        isCoroutineRunning = true;
        Spriter.color = new Color(0, 1, 0, 0.6f);
        while (DOT_time < 5 && gameObject.activeSelf)
        {
            Poison_onDamaged(damage);
            yield return new WaitForSeconds(1f);
            DOT_time++;
        }
        Spriter.color = new Color(1, 1, 1, 1f);
        isCoroutineRunning = false;
    }
    public IEnumerator Wind_Damaged(float damage)
    {
        while (ismass == true)
        {
            Wind_onDamaged(damage);
            yield return new WaitForSeconds(0.5f);
        }

    }
    public IEnumerator Lightning_Damaged(float damage)
    {
        isBeam = true;
        while (isBeam == true)
        {
            KnockBack_distance = 10;
            onDamaged(damage);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator Wind_Mass(GameObject obj, float MassPower)
    {
        if(!isBoss)
        {
            isTrigger = true;
            ismass = true;
            while (obj.activeSelf && isIce == false)
            {
                Vector2 dir = obj.transform.position - transform.position;
                Rigid.MovePosition(Rigid.position + (dir * Time.fixedDeltaTime * MassPower));
                //Rigid.velocity = Vector2.zero;
                yield return null;
            }
        }
    }
}