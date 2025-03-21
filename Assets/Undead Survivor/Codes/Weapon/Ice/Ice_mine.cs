using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_mine : MonoBehaviour
{
    Animator anim;
    public float speed = 1.5f;
    public float minAlpha = 0.5f;
    public float maxAlpha = 1f;
    public bool isalpha=true;
    public Renderer color;
    Ice ice;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        color = GetComponent<SpriteRenderer>();
        ice=GetComponentInParent<Ice>();
    }

    private void OnEnable()
    {
        CancelInvoke("Exit");
        Invoke("Exit", ice.Attack_Duration);
        isalpha = true;
        StartCoroutine(Move_alpha());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Ice_mine"))
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Move_alpha()
    {
        float alpha = minAlpha;
        float direction = 1f;

        while (isalpha)
        {
            alpha += direction * Time.deltaTime * speed;
            color.material.color = new Color(color.material.color.r, color.material.color.g, color.material.color.b, alpha);

            if (alpha >= maxAlpha)
            {
                alpha = maxAlpha;
                direction = -1f;
            }
            else if (alpha <= minAlpha)
            {
                alpha = minAlpha;
                direction = 1f;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Enemy"))
        {
            
            isalpha = false;
            color.material.color = new Color(color.material.color.r, color.material.color.g, color.material.color.b, 1);
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                anim.Play("Ice_mine");
        }
    }

    void Exit()
    {
        isalpha = false;
        color.material.color = new Color(color.material.color.r, color.material.color.g, color.material.color.b, 1);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            anim.Play("Ice_mine");
        
    }
}
