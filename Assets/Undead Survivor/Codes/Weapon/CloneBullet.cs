using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBullet : MonoBehaviour
{
    public float damage;
    Rigidbody2D rigid;
    public int count = 2;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
       
            StartCoroutine(WaitAndDeactivate(5.0f));
        
    }
    private void OnEnable()
    {
        count = 2;
        StartCoroutine(WaitAndDeactivate(5.0f));
    }
    private void Update()//카운트가 줄면 비활성화 , 생성된지 2초가 지나면 비활성화
    {
         if (count<=0)//0이되면 비활성화
            gameObject.SetActive(false);
          
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))//생성될시 count값이 2인데 생성될때 충돌한걸로 판정이나서 1로 시작 충돌때마다 감소
            count--;
    }


    public void Init(float damage, float bulletSpeed)//무기의 데미지와 ,총알속도;

    {
        this.damage = damage;
    }

    IEnumerator WaitAndDeactivate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
