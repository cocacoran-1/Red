using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour
{
    Transform centerPoint; // 중심점
    public float radius = 1f; // 반지름
    public float angularSpeed = 1f; // 각속도

    private float theta = 0f; // 현재 각도
    Player player;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    private void FixedUpdate()
    {
        centerPoint = player.transform;
        // 각도를 증가시킴
        theta += angularSpeed * Time.deltaTime;

        // 새로운 위치 계산
        float x = centerPoint.position.x + radius * Mathf.Cos(theta);
        float y = centerPoint.position.y + radius * Mathf.Sin(theta);
        Vector3 newPos = new Vector3(x, y, transform.position.z);

        // 새로운 위치로 이동
        transform.position = newPos;
    }
}
