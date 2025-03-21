using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Cultist_Axe : MonoBehaviour
{
    public bool issecond=false;
    PoolManager poolManager;
  
    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }
    private void Start()
    {
        Invoke("start_Clone", 1f);
    }
    void Update()
    {
        transform.Rotate(Vector3.back * 360f * Time.deltaTime);
    }
    
    private void OnEnable()
    {
        issecond = false;



        CancelInvoke();
        Invoke("start_Clone", 1f);
    }

    void Clone_Axe()
    {
        float angleStep = 360f / 4;
        for (int i = 0; i < 4; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // 해당 총알의 각도 계산하기
            Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정
            Transform bullet = poolManager.GetEnemy(0).transform; // 총알 생성하기
            bullet.rotation = Quaternion.FromToRotation(Vector3.left, -quaternion);// 각도를 기반으로 회전값 계산하기
            bullet.transform.position = transform.position;
            bullet.transform.localScale = bullet.transform.lossyScale;
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().velocity = quaternion * 5f; // 총알 속도 적용하기
        }
    }
    void Clone_Axe_45()
    {
        float angleStep = (360f / 4);
        for (int i = 0; i < 4; i++)
        {
            float angle = ((i * (angleStep))+45) * Mathf.Deg2Rad; // 해당 총알의 각도 계산하기
            Vector2 quaternion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//각도 설정
            Transform bullet = poolManager.GetEnemy(0).transform; // 총알 생성하기
            bullet.rotation = Quaternion.FromToRotation(Vector3.left, -quaternion);// 각도를 기반으로 회전값 계산하기
            bullet.transform.position = transform.position;
            bullet.transform.localScale = bullet.transform.lossyScale;
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().velocity = quaternion * 5f; // 총알 속도 적용하기
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CancelInvoke();
            if (issecond == true)
            {
                Clone_Axe_45();
            }
            else
            {
                Clone_Axe();
            }
            gameObject.SetActive(false);
        }
        
    }

    void start_Clone()
    {
        if(issecond==true)
        {
            Clone_Axe_45();
        }
        else
        {
            Clone_Axe();
        }
        
        gameObject.SetActive(false);
    }
}
