using System.Collections;
using UnityEngine;
public class Troll_Skill : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    public int cloneCount;
    public float Attack_Range;
   
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    WeaponPoolManager cloneobj;
    public WeaponPoolManager poolManager;
  

    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<WeaponPoolManager>();
    }
    void Start()
    {
        Invoke("Exit", 5f);
    }
    private void OnEnable()
    {     
        coll.enabled = true;       
        CancelInvoke();
        Invoke("Exit", 5f);
    }
    
    public void Init(float damage, Vector3 dir, int bulletSpeed, int cloneCount, float attact_range)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.cloneCount = cloneCount;
        this.Attack_Range = attact_range;
        rigid.velocity = dir * bulletSpeed;
    }  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //gameObject.SetActive(false);    
        }
    }  
    void Exit()
    {
        gameObject.SetActive(false);
    }
}

