using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsActivator : MonoBehaviour
{
    MenuControler menuControler;
    public Button[] buttonsToActivate;
    public GameObject StampParent;
    public GameObject MaskParent;
    public GameObject Mask;
    CanvasManager canvasManager;
    LevelUp levelUp;
    InGameSound inGameSound;
    public void Awake()
    {
        inGameSound = GameObject.Find("InGameSound").GetComponent<InGameSound>();
        canvasManager = GameObject.Find("GamePlayCanvas").GetComponent<CanvasManager>();
        menuControler = GameObject.Find("MenuControler").GetComponent<MenuControler>();
        levelUp = GameObject.Find("GamePlayCanvas").GetComponent<LevelUp>();
    }
    // 메뉴 사이드 버튼을 활성화
    public void ActivateButtons()
    {
        foreach (Button button in buttonsToActivate)
        {
            button.gameObject.SetActive(true);
        }
    }
    public void OnFlipOption()
    {
        menuControler.OnFlipOption();
    }
    public void OnFlipSkill()
    {
        menuControler.OnFlipSkill();
    }
    public void OnFlipItem()
    {
        menuControler.OnFlipItem();
    }
    public void OnSkill()
    {
        menuControler.OnSkill();
    }
    public void GoMain()
    {
        menuControler.GoMain();
    }
    // 닫힘 애니메이션이 끝날 때 호출
    public void DeactivateMenu()
    {
        menuControler.DeactivateMenu();
    }
    public void Stamped()
    {
        StampParent.SetActive(true);
        
        if (levelUp.stampObject.activeSelf)
        {
            levelUp.Hide();
            levelUp.stampObject.SetActive(false);
            Mask.SetActive(false);
            levelUp.uiLevelUp.SetActive(false);
        }

    }
    public void StampMask()
    {
        MaskParent.SetActive(true);
        if (levelUp.stampObject.activeSelf)
        {
            Mask.SetActive(true);
        }
    }

    // 메뉴 사이드 버튼을 비활성화
    public void InactiveButtons()
    {
        foreach (Button button in buttonsToActivate)
        {
            button.gameObject.SetActive(false);
        }
    }
}