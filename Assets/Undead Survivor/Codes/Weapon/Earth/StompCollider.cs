using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompCollider : MonoBehaviour
{
    StompBullet stompBullet;

    GameObject BulletContent;

    private void Awake()
    {
        stompBullet = GetComponentInChildren<StompBullet>();
        Transform BulletContent = transform.Find("BulletContent");
    }
    void SetActiveCollider(int index)
    {
        stompBullet.SetActiveCollider(index);
    }
    void BulletInactive()
    {
        gameObject.SetActive(false);
    }
}
