using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    public Collider2D coll;
    public bool isstop = false;
    Animator anim;
    GameObject target;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        coll.enabled = false;
        isstop = false;
        anim.speed = 2.0f;
    }
    private void Update()
    {
        follow_target();
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("fire_pillar"))
            gameObject.SetActive(false);
    }

    public void coll_on()
    {
        anim.speed = 1.0f;
        coll.enabled=true;
        isstop = true;
    }
    void follow_target()
    {
        if (target.activeSelf && isstop==false)
            gameObject.transform.position = target.transform.position;
    }
    public void target_transform(GameObject obj)
    {
        target = obj;
    }

}
