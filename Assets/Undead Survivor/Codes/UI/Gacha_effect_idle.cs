using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha_effect_idle : MonoBehaviour
{
    public Animator anim;
    public GachaPanel panel;
    public GameObject book;
    public GameObject effect;
    private void OnEnable()
    {
        book.SetActive(true);
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Gacha_effect_circle"))
        {
            on_gacha();
        }
    }
    public void effect_start()
    {
        book.SetActive(false);
        anim.Play("Gacha_effect_circle");
    }
    public void on_gacha()
    {
        Time.timeScale = 1;
        effect.SetActive(false);
       
        panel.enabled = true;
        panel.isTouch = false;
    }
}
