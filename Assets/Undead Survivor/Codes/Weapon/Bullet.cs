using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public int cloneCount;
    public float bulletSpeed;
    public bool isTurn = true;

    public GameObject particle;
    public Animator anim;
    public GameObject ClonePrefab;
    public CloneWeapon Cloneobj;
    public Weapon weapon;
    public ParticleSystem particlesystem;
    public GameObject Player;
    bool isHit = false; // 바위총알 충돌 여부
    EnemyHitSounds enemyHitSounds;
    Transform targettransform;
    Rigidbody2D rigid;
    CircleCollider2D circleCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        enemyHitSounds = GameObject.Find("EnemyHitSounds").GetComponent<EnemyHitSounds>();
        Player = GameObject.Find("Player");
        weapon = GetComponentInParent<Weapon>();




    }
    private void OnEnable()
    {
        if (weapon.prefabId == 5)
        {

            StartCoroutine(WaitAndDeactivate(1.0f));

        }
        else if (weapon.prefabId == 6)
        {

        }
        else if (weapon.prefabId == 0)
        {

        }
        else
        {
            StartCoroutine(WaitAndDeactivate(5.0f));
        }
        isTurn = true;
        if (weapon.prefabId == 4)//4�� �������̸� �ݶ��̴� ���� �ٽ� ���󺹱�
            circleCollider.radius = 0.33f;//���Ⱑ �ٽ� Ȱ��ȭ �ɶ� 4�� �������̸� ���� ����ȭ
    }
    private void Start()
    {
        if (weapon.prefabId == 5)
        {

            StartCoroutine(WaitAndDeactivate(1.0f));

        }
        else if (weapon.prefabId == 6)
        {

        }
        else if (weapon.prefabId == 0)
        {

        }
        else
        {
            StartCoroutine(WaitAndDeactivate(5.0f));
        }
        // ��Ȱ��ȭ�Ǳ����� ���ٸ� �ð�(��)



    }
    private void Update()
    {
        if (weapon.prefabId == 4 && isTurn)
        {
            transform.Rotate(Vector3.back * 360f * Time.deltaTime);//4�� �������Ͻ� ������Ʈȸ��
        }
        if (weapon.prefabId == 4 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("poison_attack"))//4���������� ���ĸ����� ������ ����
        {
            gameObject.SetActive(false);
        }


    }

    public void Init(float damage, int per, Vector3 dir, float bulletSpeed, int cloneCount)//���⿡ �������� ,������ ���� �Է�

    {
        this.damage = damage;
        this.per = per;
        this.cloneCount = cloneCount;
        this.bulletSpeed = bulletSpeed;
        if (per > -1)
        {
            rigid.velocity = dir * bulletSpeed;//����ü�� �ӵ�

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//������ enemy�浹������ ȣ���Լ�
    {
        if (weapon.prefabId != 0 && gameObject.activeSelf)
            StartCoroutine(WaitAndDeactivate(5.0f));

        // ������ Ÿ���� ����
        if (collision.CompareTag("Enemy"))
        {
            enemyHitSounds.EnemyHitPlay();
        }

        if (!collision.CompareTag("Enemy") || per == -1)//���� �浹�Ѱ� �ƴϰų� ������ �������� -1(����: ��������)�̶��� ����
            return;

        per--;//�浹�� ��ŭ ������ ���� ����


        if (weapon.prefabId == 3)
        {
            StartCoroutine(HitRockBullet());
            Transform obj = GameObject.Find("WeaponPoolManager").transform.Find("CloneWeapon");//Ŭ�� ���� ������Ʈ ã��


            if (isHit)
            {

                if (obj.gameObject.activeSelf == false)//Ŭ�п��� ������Ʈ�� ��Ȱ��ȭ ����
                {
                    obj.gameObject.SetActive(true);
                    Cloneobj = GameObject.Find("CloneWeapon").GetComponent<CloneWeapon>();
                }
                else//Ȱ��ȭ ����
                {
                    Cloneobj = GameObject.Find("CloneWeapon").GetComponent<CloneWeapon>();
                }
                for (int i = 0; i < cloneCount; ++i)//������ ���� ��ŭ Ŭ�� ���⸦ �����ؼ� ������ �������� �߻�  ���⼭ Ŭ�� ī��Ʈ��ŭ Ǯ��������Ʈ�� �߻�
                {
                    Vector2 position = collision.transform.position;//�浹�� ��ġ ����

                    float angle = Random.Range(0, 360);//���� ���� ����
                    Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//���� ������ ����ŭ ���� ����
                    Transform clone = Cloneobj.Get().transform;//Ŭ�� ����
                    clone.position = position;//Ŭ�� ��ġ�� �浹�� ��ġ�� ����
                    clone.rotation = Quaternion.identity;// ȸ���� ������ �ǹ�
                    clone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;//������ ������ bulletSpeed��ŭ�� �ӵ��� �߻�
                    clone.GetComponent<CloneBullet>().Init(damage, bulletSpeed);//CloneBullet�� �������� �߻� �ӵ� ����
                }
            }

            IEnumerator HitRockBullet()
            {
                anim.SetBool("isHit", true);
                while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("HitRockBullet"))
                {
                    yield return null;
                }
                isHit = true;
            }
        }




        if (per == -1)//���Ÿ����Ⱑ �浹�ؼ� �������� -1 �� �Ǿ�����
        {

            if (weapon.prefabId == 4)//4�� �������Ͻ� ȸ���� ���߰� �ִϸ��̼� ������ �ݶ��̴� ���� ���� ���� ��ŭ ����
            {
                isTurn = false;//�浹�ÿ� ���� �������� ��ü ȸ�� ����

                transform.rotation = Quaternion.Euler(0, 0, 0);//�������� ȸ�� ���� �ʱ�ȭ

                anim.Play("poison_Bomb");
                circleCollider.radius = 2.1f;//������ �浹 ���� Ȯ��

            }
            rigid.velocity = Vector2.zero;//������ ������ �� �ʱ�ȭ

        }

    }


    IEnumerator WaitAndDeactivate(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
    }

    void set()
    {


    }
    void transformInit(Transform targettransform)
    {
        this.targettransform = targettransform;
    }


}
