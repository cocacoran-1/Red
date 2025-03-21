using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReflect : MonoBehaviour
{
    Rigidbody2D IceBallRB;
    Vector2 velocity;
    Vector2 getVelocity;

    void Update() {
        GetVelocity();    
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        IceBallRB = collision.gameObject.GetComponent<Rigidbody2D>();
        velocity = collision.gameObject.GetComponent<Ice_ball>().velocity;
        IceBallRB.velocity = Vector2.Reflect(velocity, -collision.GetContact(0).normal);    //반사벡터
        getVelocity = IceBallRB.velocity.normalized;
    }

    public Vector2 GetVelocity()
    {
        return getVelocity;
    }
}
