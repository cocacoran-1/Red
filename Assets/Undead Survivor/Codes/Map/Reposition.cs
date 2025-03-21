using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Reposition : MonoBehaviour
{
    CinemachineConfiner2D virtualCamera;
    Collider2D coll;
    Player player;

    void Awake()
    {
        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineConfiner2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        coll = GetComponent<Collider2D>();
    }

    void OnEnable() 
    {
        virtualCamera.enabled = false;

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        Vector3 playerDir = player.inputVec;
        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (Mathf.Abs(diffX - diffY) <= 0.1f)
                {
                    transform.Translate(Vector3.up * dirY * 80);
                    transform.Translate(Vector3.right * dirX * 80);
                }
                else if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 80);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 80);
                }
                break;

            case "Enemy":

                break;
        }
    }
}
