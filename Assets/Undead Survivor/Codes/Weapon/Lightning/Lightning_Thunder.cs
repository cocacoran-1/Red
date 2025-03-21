using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Thunder : MonoBehaviour
{
    GameObject target;
    Animator anim;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        follow_target();
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Lightning_Thunder"))
            gameObject.SetActive(false);
    }

    public void target_transform(GameObject obj)
    {
        target = obj;
    }
    void follow_target()
    {
        if(target.activeSelf)
            gameObject.transform.position= target.transform.position;
    }

    void hit()
    {
        if(target.activeSelf)
            target.SendMessage("StoponDamaged", damage);
    }

    public void Init(float damage)//무기에 데미지와 ,관통력 정보 입력

    {
        this.damage = damage;

    }
}
