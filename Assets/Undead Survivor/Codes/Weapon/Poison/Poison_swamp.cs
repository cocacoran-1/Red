using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_swamp : MonoBehaviour
{
    Poison poison;
    private void Awake()
    {
        poison=GetComponentInParent<Poison>();
    }

    private void OnEnable()
    {
        
        Invoke("exit", poison.Attack_Duration);
    }



   
    void exit()
    {

        gameObject.SetActive(false);

    }


    

   
}
