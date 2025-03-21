using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Boss : MonoBehaviour
{ 
    public PoolManager poolManager;
    Animator anim;
    public SpriteRenderer spriter;
    Enemy enemy;
    public Boss_Troll troll;
    private void Awake()
    {
        spriter=GetComponent<SpriteRenderer>();
        //spriter.color = new Color(0, 0, 0, 0f);
        poolManager = GetComponent<PoolManager>();
        enemy = GetComponent<Enemy>();
        if(enemy.spriteType == 18)
            troll=GetComponent<Boss_Troll>();
    }
    private void Start()
    {
        GameObject game = poolManager.GetEnemy(0);
        if(enemy.spriteType==18)
        game.transform.position = gameObject.transform.position + new Vector3(0.6f, 0.3f);
        else
        {
            game.transform.position = gameObject.transform.position ;
        }
    }
}
