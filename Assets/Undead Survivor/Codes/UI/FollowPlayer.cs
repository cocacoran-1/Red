using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    RectTransform rect;
    Player player;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        rect.position = 
        Camera.main.WorldToScreenPoint(player.transform.position);
    }
}
