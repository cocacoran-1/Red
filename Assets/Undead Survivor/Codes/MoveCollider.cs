using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCollider : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        if(GameObject.Find("HalloweenCollider"))
        {
            transform.position = new Vector2(transform.position.x, player.transform.position.y);
            // 오브젝트의 위치를 플레이어의 y좌표로 변경하고, X 좌표를 계산하여 위치 업데이트
        }
        else if(GameObject.Find("DungeonCollider"))
        {
            transform.position = new Vector2(player.transform.position.x, transform.position.y);
        }
        else
        {
            transform.position=player.transform.position;
        }
        
    }
}
