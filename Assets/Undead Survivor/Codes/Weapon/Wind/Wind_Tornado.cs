using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Tornado : MonoBehaviour
{
    Wind wind;
    Animator anim;
    private void Awake()
    {
        wind = GetComponentInParent<Wind>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(Exit());

       
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Wind_Tornado_End"))
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(wind.Attack_Duration);
        anim.Play("Wind_Tornado_End");
    }
}
