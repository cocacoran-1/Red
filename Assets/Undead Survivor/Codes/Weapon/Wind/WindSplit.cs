using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSplit : MonoBehaviour
{
    public float damage;
    public int bulletSpeed;
    public int cloneCount;
    float angle;
    float Attack_Range;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    Vector3 dir;
    WeaponPoolManager cloneobj;
    public WeaponPoolManager poolManager;
    public bool ishit = false;

    public float spawnInterval = 1.0f;
    private float spawnTimer;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        poolManager = GetComponent<WeaponPoolManager>();
    }
    void Start()
    {
        spawnTimer = spawnInterval; // 초기화
    }
    void Update()
    {
        rigid.velocity = dir * bulletSpeed* 0.5f;

        // 시간을 추적하고 일정 시간이 지났을 때 클론 생성 코드를 실행
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = spawnInterval;
            CreateClones();
        }
    }

    // 발사된 객체가 범위 콜라이더에서 나갔을 때 호출되는 함수
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BulletRange"))
        {
            gameObject.SetActive(false);

        }
    }
    public void Init(float damage, Vector3 dir, int bulletSpeed, int cloneCount, float angle, float Attack_Range)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.cloneCount = cloneCount;
        this.angle = angle;
        this.Attack_Range = Attack_Range;
        this.dir = dir;
        rigid.velocity = dir * bulletSpeed;
    }

    void CreateClones()
    {
        Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("Wind_Split_Clone");

        if (obj.gameObject.activeSelf == false)
        {
            obj.gameObject.SetActive(true);
            cloneobj = GameObject.Find("Wind_Split_Clone").GetComponent<WeaponPoolManager>();
        }
        else
        {
            cloneobj = GameObject.Find("Wind_Split_Clone").GetComponent<WeaponPoolManager>();
        }

        for (int i = 0; i < cloneCount; ++i)
        {
            Vector2 position = transform.position;
            float currentAngle = transform.rotation.eulerAngles.z;
            float random = Random.Range(0, 360f);
            float angleInRadians = (currentAngle + random) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            direction *= -1f;

            Transform clone = cloneobj.Get().transform;
            clone.transform.localScale = new Vector3(Attack_Range * 2f, Attack_Range * 2f, Attack_Range * 2f);
            clone.position = position;
            clone.rotation = Quaternion.FromToRotation(Vector3.right, direction);
            clone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            clone.GetComponent<WindSplit_Clone>().Init(damage, bulletSpeed);
        }
    }

}
