using UnityEngine;

public class Player_Info : MonoBehaviour
{
    public static Player_Info instance = null;
    public Player_Stat Player_stat;
    [Header("공격 관련")]
    [SerializeField]
    private float Damage;// 공격력
    [SerializeField]
    private float AttackSpeed;//공격속도
    [SerializeField]
    private float Attack_Range;//공격범위
    [SerializeField]
    private float Attack_Duration;//공격 지속시간


    [Header("체력 관련")]
    [SerializeField]
    private float Max_Hp;//최대체력
    [SerializeField]
    private float Defense;//방어력 증가
    [SerializeField]
    private float Hp_Regen;//체력 회복

    [Header("기타")]
    [SerializeField]
    private float Speed;//이동속도
    [SerializeField]
    private float Magnet_Range;//자석 범위
    [SerializeField]
    private float Exp_Up;// 경험치 증가
    [SerializeField]
    private float Gold_Up;//골드 증가


    GameManager manager;
    Player player;

    private void Awake()
    {
        manager = GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();

        Damage= Player_stat.Damage;// 공격력
        AttackSpeed= Player_stat.AttackSpeed;//공격속도
        Attack_Range= Player_stat.Attack_Range;//공격범위
        Attack_Duration= Player_stat.Attack_Duration;//공격 지속시간


        Max_Hp= Player_stat.Max_Hp;//최대체력
        Defense = Player_stat.Defense;//방어력 증가
        Hp_Regen = Player_stat.Hp_Regen;//체력 회복

        Speed = Player_stat.Speed;//이동속도
        Magnet_Range = Player_stat.Magnet_Range;//자석 범위
        Exp_Up = Player_stat.Exp_Up;// 경험치 증가
        Gold_Up = Player_stat.Gold_Up;//골드 증가

    }

    // 캐릭터의 능력치를 조절할때 변수에 직접 접근하지 않고 함수로 접근해서 오류 최소화
    public void Init_Damage(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Damage += num;
                break;

            case '-':
                this.Damage -= num;
                break;
            case '*':
                this.Damage *= num;
                break;
            default:
                break;
        }
    }
    public void Init_AttackSpeed(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.AttackSpeed += num;
                break;

            case '-':
                this.AttackSpeed -= num;
                break;
            case '*':
                this.AttackSpeed *= num;
                break;
            default:
                break;
        }
    }
    public void Init_Attack_Range(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Attack_Range += num;
                break;

            case '-':
                this.Attack_Range -= num;
                break;
            case '*':
                this.Attack_Range *= num;
                break;
            default:
                break;
        }
    }
    public void Init_Attack_Duration(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Attack_Duration += num;
                break;

            case '-':
                this.Attack_Duration -= num;
                break;
            case '*':
                this.Attack_Duration *= num;
                break;
            default:
                break;
        }
    }
    public void Init_Max_Hp(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Max_Hp += num;
                manager.Init();
                break;

            case '-':
                this.Max_Hp -= num;
                manager.Init();
                break;
            case '*':
                this.Max_Hp *= num;
                manager.Init();
                break;
            default:
                break;
        }
    }
    public void Init_Speed(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Speed += num;
                player.Init();
                break;

            case '-':
                this.Speed -= num;
                player.Init();
                break;
            case '*':
                this.Speed *= num;
                player.Init();
                break;
            default:
                break;
        }
    }
    public void Init_Hp_Regen(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Hp_Regen += num;
                manager.Init();
                break;

            case '-':
                this.Hp_Regen -= num;
                manager.Init();
                break;
            case '*':
                this.Hp_Regen *= num;
                manager.Init();
                break;
            default:
                break;
        }
    }

    public void Init_Magnet_Range(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Magnet_Range += num;
                break;

            case '-':
                this.Magnet_Range -= num;
                break;
            case '*':
                this.Magnet_Range *= num;
                break;
            default:
                break;
        }
    }
    public void Init_Exp_Up(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Exp_Up += num;

                break;

            case '-':
                this.Exp_Up -= num;

                break;
            case '*':
                this.Exp_Up *= num;

                break;
            default:
                break;
        }
    }
    public void Init_Gold_Up(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Gold_Up += num;

                break;

            case '-':
                this.Gold_Up -= num;

                break;
            case '*':
                this.Gold_Up *= num;

                break;
            default:
                break;
        }
    }
    public void Init_Defense(float num, char a)
    {
        switch (a)
        {
            case '+':
                this.Defense += num;
                break;

            case '-':
                this.Defense -= num;
                break;
            case '*':
                this.Defense *= num;
                break;
            default:
                break;
        }
    }
    //-------------------------------------------------------------------------------------------------------------------//
    // 캐릭터의 변수를 가져올때도 함수를 사용해서 오류 최소화
    public float Get_Damage()
    {
        return this.Damage;
    }
    public float Get_AttackSpeed()
    {
        return this.AttackSpeed;
    }
    public float Get_Max_Hp()
    {
        return this.Max_Hp;
    }
    public float Get_Speed()
    {
        return this.Speed;
    }
    public float Get_Attack_Range()
    {
        return this.Attack_Range;
    }
    public float Get_Attack_Duration()
    {
        return this.Attack_Duration;
    }
    public float Get_Hp_Regen()
    {
        return this.Hp_Regen;
    }
    public float Get_Magnet_Range()
    {
        return this.Magnet_Range;
    }
    public float Get_Exp_Up()
    {
        return this.Exp_Up;
    }
    public float Get_Gold_Up()
    {
        return this.Gold_Up;
    }
    public float Get_Defense()
    {
        return this.Defense;
    }



}
