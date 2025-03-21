using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha_effect : MonoBehaviour
{
    public Animator anim;
    public Gacha_Slot slot;
    public GameObject fire_effect;
 

   
    

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Gacha_effect"))
        {
            fire_effect.SetActive(true);
           
            slot.FadeInImage(slot.Frame.gameObject, 0.7f, true);
            gameObject.SetActive(false);
        }
    }

}
