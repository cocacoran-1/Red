using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireThrower : MonoBehaviour
{
    public ParticleSystem flameThrowerEffect;
    
    Player player;
    public float angle;
    Vector2 spawnOffset;
    Fire fire;
    float radius;
    public float Attack_Duration;//유지시간
    public float Attack_Range;
    public float FireThrowerDamage;
    public float skillDuration = 1.0f;
    private bool isActivated = false;
    private float activationTime;
    public float damageRate = 0.1f;
    private float nextDamageTime = 0f;
    private Collider2D flameThrowerCollider;
    private List<Enemy> enemiesInRange;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        flameThrowerEffect = GetComponentInChildren<ParticleSystem>();
        flameThrowerCollider = GetComponentInChildren<Collider2D>();
        fire = GetComponentInParent<Fire>();
        
        enemiesInRange = new List<Enemy>();
    }

    private void OnEnable()
    {
        Attack_Duration=fire.Get_Attack_Duration();
        Attack_Range=fire.Get_Attack_Range();
        FireThrowerDamage=fire.Get_Damage();
        isActivated = true;
        transform.GetChild(0).localScale = new Vector3(Attack_Range, Attack_Range, Attack_Range);
        if (flameThrowerEffect != null)
        {
            flameThrowerEffect.Play();
        }
        Invoke("Exit", Attack_Duration);
    }

    void Update()
    {
        if (isActivated)
        {
            spawnOffset = player.joystickDirection.normalized * radius;
            gameObject.transform.position = spawnOffset;
            gameObject.transform.position += player.transform.position;
            angle = player.angle;
            transform.eulerAngles = new Vector3(0, 0, angle);

            if (flameThrowerEffect != null)
            {
                var shapeModule = flameThrowerEffect.shape;
                shapeModule.rotation = new Vector3(0, 0, angle);
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemiesInRange.Add(enemy);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time >= nextDamageTime)
        {
            List<Enemy> enemiesToDamage = new List<Enemy>(enemiesInRange);
            foreach (Enemy enemy in enemiesToDamage)
            {
                if (enemy != null)
                {
                   
                    enemy.onDamaged(FireThrowerDamage);
                }
            }
            nextDamageTime = Time.time + damageRate;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
    
}
