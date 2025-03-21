using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start() 
    {
        anim.SetFloat("offset", Random.Range(0, 10f));
    }
}
