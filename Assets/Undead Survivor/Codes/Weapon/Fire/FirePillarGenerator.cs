using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarGenerator : MonoBehaviour
{
    public GameObject firePillarPrefab; // 불기둥 프리팹
    public float damage = 5f; // 데미지
    public float radius = 6f; // 적 탐색 반경
    public float spawnInterval = 4f; // 생성 간격
    public float pillarDuration = 2.5f; // 불기둥 지속시간
    public float damageInterval = 1f; // 피해를 입히는 간격
    public int maxPillars = 3; // 최대 불기둥 개수

    
    private float spawnTimer = 0f; // 불기둥 생성 타이머를 저장할 변수 선언

    private List<GameObject> firePillars; // 생성된 불기둥들을 저장할 리스트 선언
    private List<int> availablePillarIndexes;

    void Start()
    {
        // 불기둥들을 미리 생성하고, 모두 비활성화
        firePillars = new List<GameObject>();
        availablePillarIndexes = new List<int>();
        for (int i = 0; i < maxPillars; i++)
        {
            GameObject firePillar = Instantiate(firePillarPrefab, transform.position, Quaternion.identity);
            firePillar.SetActive(false);
            firePillars.Add(firePillar);
            availablePillarIndexes.Add(i);
        }
    }
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnFirePillar();
            spawnTimer = 0f;
        }
    }

    void SpawnFirePillar()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        List<GameObject> targetEnemies = new List<GameObject>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                targetEnemies.Add(collider.gameObject);
            }
        }

        int pillarCount = Mathf.Min(maxPillars, targetEnemies.Count);
        for (int i = 0; i < pillarCount; i++)
        {
            // 불기둥 재활용을 위해, 사용 가능한 불기둥 인덱스 찾기
            int index = -1;
            for (int j = 0; j < availablePillarIndexes.Count; j++)
            {
                int pillarIndex = availablePillarIndexes[j];
                if (firePillars[pillarIndex].activeSelf == false)
                {
                    index = pillarIndex;
                    availablePillarIndexes.RemoveAt(j);
                    break;
                }
            }

            if (index == -1)
            {
                // 사용 가능한 불기둥이 없으면, 새로 생성
                GameObject newFirePillar = Instantiate(firePillarPrefab, transform.position, Quaternion.identity);
                newFirePillar.SetActive(false);
                firePillars.Add(newFirePillar);
                index = firePillars.Count - 1;
            }
            // 불기둥을 생성할 좌표
            int enemyIndex = Random.Range(0, targetEnemies.Count);
            Vector3 spawnPos = targetEnemies[enemyIndex].transform.position + Random.insideUnitSphere * 2f;

            GameObject firePillar = firePillars[index];
            firePillar.transform.position = spawnPos;
            firePillar.SetActive(true);

            StartCoroutine(DamageEnemies(firePillar, targetEnemies[enemyIndex], index));
        }
    }



    IEnumerator DamageEnemies(GameObject firePillar, GameObject targetEnemy, int pillarIndex)
    {
        Animator animator = firePillar.GetComponent<Animator>();
        animator.Play("firePillar");
        animator.Update(0f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(firePillar.transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == targetEnemy && collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().StoponDamaged(damage);
            }
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1
                            && animator.GetCurrentAnimatorStateInfo(0).IsName("firePillar"))
        {
            yield return new WaitForSeconds(damageInterval);
        }

        // 불기둥 비활성화하고, 사용 가능한 불기둥 리스트에 추가
        firePillar.SetActive(false);
        availablePillarIndexes.Add(pillarIndex);
    }
}