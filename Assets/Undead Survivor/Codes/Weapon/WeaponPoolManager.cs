using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoolManager : MonoBehaviour
{
    public GameObject prefabs;
    public List<GameObject> pools;

    void Awake()
    {
        pools = new List<GameObject>();
    }

    public GameObject Get()
    {
        GameObject select = null;

        // 선택한 풀의 놀고 있는(비활성화 된) 게임 오브젝트 접근
        foreach (GameObject item in pools)
        {
            if (!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 못 찾았다면
        if (select == null)
        {
            // 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs,transform);
            pools.Add(select);
        }
        return select;
    }
   
}
