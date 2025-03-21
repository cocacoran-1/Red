using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wind_hole : MonoBehaviour
{

    Wind wind;
    public float Attack_Duration;
    
    private void Awake()
    {
        wind=GetComponentInParent<Wind>();
    }
    private void OnEnable()
    {
        
        Invoke("exit", wind.Attack_Duration);
    }
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            GameObject obj = collision.gameObject; 
            Rigidbody2D rigid=collision.GetComponent<Rigidbody2D>();
            Enemy enemy= collision.GetComponent<Enemy>();
            if(rigid != null && enemy!=null)
            {
                enemy.isTrigger = true;
                if(obj.activeSelf)
                StartCoroutine(Mass(rigid));
                if (obj.activeSelf)
                    enemy.StartCoroutine("Wind_Damaged", enemy);

            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.CompareTag("Enemy"))
        {
            enemy.isTrigger = false;
            if (obj.activeSelf)
                enemy.StopCoroutine("Wind_Damaged");
            if (obj.activeSelf)
                StopCoroutine("Mass");
        }
    }
    public IEnumerator Mass(Rigidbody2D rigid)
    {
        

        while(true)
        {
            Vector2 dir=transform.position-rigid.transform.position;
            rigid.MovePosition(rigid.position+(dir * Time.fixedDeltaTime * inForce));
            rigid.velocity = Vector2.zero;
            yield return null;

            
        }

        
    }*/

    



    void exit()
    {
       /* Collider2D collider = GetComponent<Collider2D>();

        // 바람 구멍의 범위 안에 있는 모든 적을 찾아서 isTrigger를 false로 바꿈
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            if (collider.bounds.Contains(enemy.transform.position) && enemy.isTrigger)
            {
                enemy.isTrigger = false;
                enemy.StopCoroutine("Wind_Damaged");
            }
        }*/
        gameObject.SetActive(false);

    }
   
}
