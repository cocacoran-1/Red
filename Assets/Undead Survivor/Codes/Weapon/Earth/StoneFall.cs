using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFall : MonoBehaviour
{
    public float damage;
    Vector3 dir;
    public float throwForce;
    public float speed = 5.0f;
    public float upwardForce = 5.0f;
    public float gravity = -9.81f;
    public float fallingGravityMultiplier = 2.0f;
    private float currentUpwardForce;
    private Vector2 velocity;
    private float horizontalForce;

    private void OnEnable()
    {
        Invoke("DisableGameObject", 3f);
        currentUpwardForce = upwardForce;
        horizontalForce = Random.Range(-0.2f, 0.2f);
    }
    private void Update()
    {
        float currentGravity = currentUpwardForce > 0 ? gravity : gravity * fallingGravityMultiplier;
        Vector3 movement = new Vector3(horizontalForce * speed * Time.deltaTime, currentUpwardForce * Time.deltaTime, 0);
        transform.position += movement;
        transform.Rotate(Vector3.back*360f*Time.deltaTime);
        currentUpwardForce += currentGravity * Time.deltaTime;
    }

    private void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
    public void ApplyRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void Init(float damage, Vector3 dir, Quaternion rotation)
    {

        this.damage = damage;
        this.dir = dir;
        ApplyRotation(rotation);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.onDamaged(damage);
        }
    }
}
