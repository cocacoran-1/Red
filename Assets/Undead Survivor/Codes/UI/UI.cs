using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public enum UiType { Exp, Health }
    public UiType type;
    Slider expSlider;
    GameManager gameManager;
    void Awake()
    {
        expSlider = GetComponent<Slider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void LateUpdate()
    {
        switch (type)
        {
            case UiType.Exp:
                float curExp = gameManager.curExp;
                float maxExp = gameManager.nextExp[gameManager.curLevel];
                expSlider.value = curExp / maxExp;
                break;
            case UiType.Health:
                float curHealth = gameManager.curHealth;
                float maxHealth = gameManager.maxHealth;
                expSlider.value = curHealth / maxHealth;
                break;
        }
    }
}
