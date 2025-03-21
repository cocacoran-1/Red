using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public Animator anim;
    
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Reinforce_effect"))
        {
            gameObject.SetActive(false);
        }
    }
    
}
