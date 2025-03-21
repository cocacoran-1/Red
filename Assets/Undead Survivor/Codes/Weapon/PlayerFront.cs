using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFront : MonoBehaviour
{
    public Transform playerTransform; // 따라다닐 플레이어 객체의 Transform 컴포넌트
    public float distance = 2.0f; // 플레이어와의 일정 거리


    void Update()
    {
        // 빈 객체의 위치를 설정합니다.
        Vector3 playerPos = playerTransform.position;
        Vector3 playerRight = playerTransform.right.normalized * Mathf.Sign(playerTransform.localScale.x);
        Vector3 newPosition = playerPos + playerRight * distance;
        newPosition.y = transform.position.y; // y축 위치를 이전 프레임의 위치로 고정시킵니다.
        transform.position = newPosition;

        // 빈 객체의 회전 정보를 설정합니다.
        Vector3 playerScale = playerTransform.localScale;
        transform.localScale = new Vector3(Mathf.Sign(playerScale.x), 1, 1);
        transform.rotation = Quaternion.Euler(0, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z) * Quaternion.AngleAxis(playerScale.x > 0 ? 0 : 180, Vector3.up);
    }
}