using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 사전에 준비된 게임 오브젝트 배열
    public GameObject[] prefabs;

    // 사전에 준비된 게임 오브젝트 배열
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // pools 배열 초기화

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>(); // pools 배열의 각 요소 리스트 초기화
        }

    }
    public GameObject GetEnemy(int i)
    {

        GameObject select = null;

        // 선택한 풀에서 사용 가능한 비활성화된 적 게임 오브젝트 찾기
        foreach (GameObject item in pools[i])
        {
            if (!item.activeSelf)
            {
                select = item;              // 비활성화된 적 게임 오브젝트를 찾으면 선택 변수에 할당
                select.SetActive(true);     // 선택한 적 게임 오브젝트 활성화
                break;                      // 반복문 탈출
            }
        }

        // 사용 가능한 게임 오브젝트가 없을 경우
        if (select == null)
        {
            select = Instantiate(prefabs[i], transform);  // 해당 프리팹 복제
            pools[i].Add(select);                         // 풀에 추가
        }

        return select;   // 적 게임 오브젝트 반환
    }

}
