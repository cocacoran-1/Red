using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_shield : MonoBehaviour
{
    public Animator anim;
    public Player player;
    public float Attack_Duration;
    public float shield;
    CircleCollider2D coll;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        coll = GetComponent<CircleCollider2D>();
    }
  

    private void OnEnable()
    {
        
        coll.enabled = false;
        
    }


    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Shield"))
            gameObject.SetActive(false);
    }
    

     IEnumerator Push_enemy()//애니매이션 컨트롤러에서 제어
    {
        player.StartCoroutine(player.Shield(shield, Attack_Duration) ); //플레이어에게 shield만큼의 추가 체력 부여
        coll.enabled = true;
        yield return new WaitForSeconds(0.1f);
        coll.enabled = false;
    }

    public void Init(float shield,float Attack_Duration)//무기에 데미지와 ,관통력 정보 입력

    {
        this.Attack_Duration = Attack_Duration;
        this.shield = shield;

    }

}

