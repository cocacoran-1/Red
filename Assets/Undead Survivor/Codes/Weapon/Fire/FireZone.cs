using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    Player_Info player_info;
    
    public float damagePerSecond = 1f; // 초당 입히는 데미지
    public float damageDelay = 0.1f; // 데미지 입히는 딜레이 시간
    private float nextDamageTime = 0f; // 다음 데미지 입히는 시간
    private bool isOnDamage = false; // 데미지 입히는 중인지 여부

    public float colliderRadius = 1f; // 장판 크기

    private List<Collider2D> collidingEnemies = new List<Collider2D>(); // 범위 안의 적 리스트

    private void Awake()
    {
        player_info = GameObject.Find("GameManager").GetComponent<Player_Info>();
        Init();
    }
    void Update()
    {
        transform.localScale = new Vector3(colliderRadius, colliderRadius, 1f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 범위 안으로 들어온 적을 리스트에 추가
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            collidingEnemies.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 범위 밖으로 나간 적은 리스트에서 제거
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {

            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isDamagedZone = false;
            collidingEnemies.Remove(other);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isOnDamage && Time.time > nextDamageTime)
        {

            isOnDamage = true;
            nextDamageTime = Time.time + damageDelay;

            // collidingEnemies 리스트의 복사본을 만들어서 열거하면서 데미지를 입힘
            List<Collider2D> collidingEnemiesCopy = new List<Collider2D>(collidingEnemies);

            // collidingEnemies 리스트에 저장된 모든 적에게 데미지를 입힘
            foreach (Collider2D enemyCollider in collidingEnemiesCopy)
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();

                // 이미 삭제된 적 객체는 데미지를 입히지 않음
                if (enemy != null)
                {
                    enemy.StoponDamaged(damagePerSecond * damageDelay);

                }
            }

            StartCoroutine(OnDamageDelay());
        }
        IEnumerator OnDamageDelay()
        {
            yield return new WaitForSeconds(damageDelay);
            isOnDamage = false;
        }
    }
    
    public void Init()
    {
        damagePerSecond = damagePerSecond + player_info.Get_Damage();
        colliderRadius= colliderRadius+player_info.Get_Attack_Range();
    }
}
