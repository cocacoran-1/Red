using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Beam : MonoBehaviour
{
    Lightning lightning;
    Animator anim;
    Collider2D coll;
    CircleCollider2D circle;
    Player player;
    public float angle;
    Vector2 spawnOffset;
    float radius;
    public float Attack_Duration;
    Rigidbody2D rigid;
    private void Awake()
    {
        lightning = GetComponentInParent<Lightning>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        circle = GameObject.Find("Lightning_Ray").GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Exit());
        coll.enabled = false;

        radius = circle.radius;
    }


    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Lightning_Beam_End"))
        {
            GetComponentInParent<Lightning>().timer = 0;
            gameObject.SetActive(false);
        }
        spawnOffset = player.joystickDirection.normalized * radius;
        if (spawnOffset == new Vector2(0, 0))
            spawnOffset = Vector2.right * radius;
        gameObject.transform.position = spawnOffset;
        gameObject.transform.position += player.transform.position;
        angle = player.angle + 90f;
        transform.eulerAngles = new Vector3(0, 0, angle);

    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(lightning.Attack_Duration);


        anim.Play("Lightning_Beam_End");

        coll.enabled = false;
    }

    void on_coll()
    {
        if (coll.enabled == false)
            coll.enabled = true;
    }


}
