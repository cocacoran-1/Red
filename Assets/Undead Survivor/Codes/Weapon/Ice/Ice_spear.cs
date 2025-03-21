using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_spear : MonoBehaviour
{
    Ice ice;
    Animator anim;

    private void Awake()
    {
        ice = GetComponentInParent<Ice>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("IceSpear"))
        {
            gameObject.SetActive(false);
        }
    }

}
