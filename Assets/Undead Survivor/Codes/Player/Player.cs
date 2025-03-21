using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class Player : MonoBehaviour
{
   

    [Header("UI References")]
    public Vector2 inputVec;
    public float angle;
    public Vector2 joystickDirection = Vector2.right;
    public float joy_x;
    public float speed;//이동속도
    public float avoid = 0; //회피율
    public float Defense;//방어도
    public float Exp_Up;// 경험치 증가
    public float exp;
    public float highexp;
    public float Gold_Up;//골드 증가
    private float Magnet_Range;//자석 범위


    [Header("Prefabs")]
    public FloatingJoystick Joystick;
    public Scanner scanner;
    public int killCount = 0;   //플레이어 킬수
    public List<GameObject> Last_Boss = new List<GameObject>();


    public CircleCollider2D Itemcoll; // 아이템을 당겨오는 범위
    public GameObject menuSet; // 아이템 메뉴



    [Header("Boolean Variables")]
    bool isDamage = false; // 플레이어 피격
    bool ishit = false;
    bool isDead = false;


    //bool isDead; // 플레이어 사망

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    Player_Info player_info;
    GameManager gameManager;
    CanvasManager canvasManager;
    InGameSound inGameSound;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
        canvasManager = GameObject.Find("GamePlayCanvas").GetComponent<CanvasManager>();
        inGameSound = GameObject.Find("InGameSound").GetComponent<InGameSound>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        isDead = false;
    }
    private void Start()
    {
        speed = player_info.Get_Speed();
        Defense = player_info.Get_Defense();
        Exp_Up = player_info.Get_Exp_Up();
        Gold_Up = player_info.Get_Gold_Up();
        Magnet_Range = player_info.Get_Magnet_Range();
        Itemcoll.radius = Magnet_Range;
    }
    void Update()
    {
        if (!gameManager.isLive)
        {
            return;
        }
        if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
        {
            angle = Mathf.Atan2(Joystick.Vertical, Joystick.Horizontal) * Mathf.Rad2Deg;
            joystickDirection = Joystick.Direction;
            joy_x = Joystick.Horizontal;
        }
        inputVec = new Vector3(Joystick.Horizontal, Joystick.Vertical, 0);
        rigid.velocity = inputVec * speed;
    }
    void FixedUpdate()
    {
        float horizontal = Joystick.Horizontal;
        float vertical = Joystick.Vertical;
        transform.position = transform.position + new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
    }

    void LateUpdate()   //프레임이 종료 되기 전 실행되는 생명주기 함수
    {
        anim.SetFloat("Speed", inputVec.magnitude);    //벡터의 순수한 크기값(Speed에 반영할 float값인 SetFloat의 두번째 인자)

        //flip - SpriteRenderer(그림)이 반전되는것, flipX는 좌우반전, flipY는 상하반전
        if (Joystick.Horizontal != 0)
        {
            spriter.flipX = Joystick.Horizontal < 0;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if(isDead==false)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>(); // Enemy.cs 가져오기
                if (isDamage == false)   //무적이 아닌 상태
                {
                    if (ishit == false)
                    {
                        if ((int)enemy.damage > (int)player_info.Get_Defense())
                        {
                           
                            gameManager.curHealth -= ((int)enemy.damage - (int)player_info.Get_Defense());
                        }// enemy의 damage에 따라 현재 체력 감소
                        inGameSound.SfxPlay(InGameSound.Sfx.playerHit); // 타격음
                                                                        //float curHealth = GameManager.instance.curHealth;
                        if (gameManager.curHealth <= 0)
                        {
                            OnDie();
                        }
                        StartCoroutine(IsDamageFalse());
                    }
                }
            }
        }
        
    }

    IEnumerator IsDamageFalse()
    {
        ishit = true;
        isDamage = true;
        spriter.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.3f);
        spriter.color = new Color(1, 1, 1, 1);
        isDamage = false;
        ishit = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(isDead==false)
        {
            if (other.CompareTag("Item"))
            {
                Item item = other.GetComponent<Item>(); // Item.cs 가져오기
                switch (item.type)
                {
                    // 일반 경험치
                    case "Exp":
                        gameManager.GetExp(10 * Exp_Up); // GameManager의 GetExp() 함수로 경험치 획득, 레벨업
                        item.gameObject.SetActive(false);  // Player가 exp를 먹을 시 exp 삭제
                        break;
                    // 강화 경험치
                    case "highExp":
                        gameManager.GetExp(100 * Exp_Up);
                        item.gameObject.SetActive(false);
                        break;
                    // 자석 아이템
                    case "MagnetItem":
                        StartCoroutine(PullingExpAll());
                        item.gameObject.SetActive(false);
                        break;
                    // 아이템 상자
                    case "Box":
                        StartCoroutine(GetBox());
                        item.gameObject.SetActive(false);
                        break;
                    /*
                    // 재화
                    case "Coin":
                        canvasManager.GetCoin();
                        item.gameObject.SetActive(false);
                        break;
                    */
                    // 강화재료
                    case "Upgrade":
                        canvasManager.GetUpgrade();
                        item.gameObject.SetActive(false);
                        break;
                }
            }

            if (other.CompareTag("Bullet"))
            {
                Boss_damaged boss_Damaged = other.GetComponent<Boss_damaged>(); // Enemy.cs 가져오기
                if (isDamage == false)   //무적이 아닌 상태
                {
                    if (ishit == false)
                    {
                        if ((int)boss_Damaged.damage > (int)player_info.Get_Defense())
                        {
                           
                            gameManager.curHealth -= ((int)boss_Damaged.damage - (int)player_info.Get_Defense());
                        }// enemy의 damage에 따라 현재 체력 감소
                        inGameSound.SfxPlay(InGameSound.Sfx.playerHit); // 타격음

                        //float curHealth = GameManager.instance.curHealth;
                        if (gameManager.curHealth <= 0)
                        {
                            OnDie();
                        }
                        StartCoroutine(IsDamageFalse());
                    }
                }
            }
        }
        
    }
    //아이템 상자 획득시 아이템 선택 UI 불러오기
    public IEnumerator GetBox()
    {
        yield return null;
        Time.timeScale = 0;
        menuSet.SetActive(true);
        while (menuSet.activeSelf)
        {
            yield return null;
        }
        Time.timeScale = 1;
    }
    void GetCoin()
    {
        canvasManager.GetCoin();
    }
    void GetUpgrade()
    {
        canvasManager.GetUpgrade();
    }
    IEnumerator PullingExpAll() // 아이템 획득 영역을 넓혀서 모든 아이템 당겨오기
    {

        yield return new WaitForSeconds(0.1f);
        // 원래 아이템 획득 영역 저장
        float originalRaius = Itemcoll.radius;
        // 증가된 영역을 생성
        float newRadius = Mathf.Sin(Time.time) * 10000.0f + 10000.0f;
        // 증가된 영역을 플레이어의 영역으로 변경
        Itemcoll.radius = newRadius;
        // 아이템 획득 영역 원상복구
        yield return new WaitForSeconds(2f);
        Itemcoll.radius = originalRaius;
    }
    void OnDie()
    {
        isDead = true;
        // 사망 사운드 출력
        inGameSound.SfxPlay(InGameSound.Sfx.Over);
        canvasManager.GameOver();
    }
    public IEnumerator Shield(float shield, float speed)//쉴드 구현
    {
        gameManager.curHealth += shield;
        gameManager.maxHealth += shield;
        float hp = gameManager.curHealth;
        yield return new WaitForSeconds(speed);

        if ((hp - gameManager.curHealth) >= 0)
        {
            UnityEngine.Debug.Log(hp - gameManager.curHealth);
        
        gameManager.curHealth -= shield-(hp - gameManager.curHealth);
        }
        gameManager.maxHealth -= shield;
        //체력재생은 고려하지 않음. 
       
    }
    public void Init()
    {
        speed = player_info.Get_Speed();
        Defense = player_info.Get_Defense();
        Exp_Up = player_info.Get_Exp_Up();
        Gold_Up = player_info.Get_Gold_Up();
        Magnet_Range = player_info.Get_Magnet_Range();
        Itemcoll.radius = Magnet_Range;
    }

    
    
    public IEnumerator Last_Boss_kill()
    {
        
        if (Last_Boss.Count==0)
        {
            yield return new WaitForSeconds(3f);
            inGameSound.SfxPlay(InGameSound.Sfx.Over);
            canvasManager.GameClear();

        }
    }
    public void start_Last_Boss_kill()
    {
        StartCoroutine(Last_Boss_kill());
    }
}
