using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    private float speed = 9;
    Rigidbody2D rigid;
    GameObject target;

    private CircleCollider2D coll;

    void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        coll = GetComponent<CircleCollider2D>();
        rigid.velocity = Vector2.zero;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Item이 Magnet에 접촉시 실행
        if (other.CompareTag("Magnet"))
        {
            if(gameObject.activeSelf)
            {
                StartCoroutine(PullingExp());
            }
        }
    }
    IEnumerator PullingExp() // 경험치 오브젝트를 플레이어로 끌어오는 함수
    {
        // 경험치 오브젝트를 먹을 때까지 실행
        while (gameObject.activeSelf)
        {
            // 경험치 오브젝트의 position을 target의 position으로 이동
            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
            // 시간이 지날수록 경험치 오브젝트 가속
            speed += 0.05f;

            yield return null;
        }
        gameObject.SetActive(false);
    }


}

