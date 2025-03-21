using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posion_Bomb : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("poison_Bomb"))
        {
            gameObject.SetActive(false);
        }
    }
 
   
}
