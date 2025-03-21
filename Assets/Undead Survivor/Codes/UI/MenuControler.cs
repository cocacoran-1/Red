using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class MenuControler : MonoBehaviour
{
    GameManager gameManager;

    public GameObject levelUp;

    [Header("메뉴")]
    public GameObject skill; // 스킬 UI
    public GameObject skillUi; // 세부 UI
    public Animator SkillAnimator;
    public GameObject optionMenu; // 설정 UI
    public GameObject optionUi; // 세부 UI
    public Animator OptionAnimator;

    public GameObject itemMenu; // 스킬 UI
    public GameObject itemUi; // 세부 UI
    public Animator ItemAnimator;
    public ButtonsActivator ButtonsActivator;

    public FadeEffect fadeEffect;
    public GameObject GameFadeOut;
    InGameSound inGameSound;
    private void Awake()
    {
        inGameSound = GameObject.Find("InGameSound").GetComponent<InGameSound>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // 시작 시 호출되는 함수
    void Start()
    {
        optionUi.SetActive(false); // 옵션 세부 UI를 비활성화
        skillUi.SetActive(false); // 스킬 세부 UI를 비활성화
    }

    // 업데이트 시 호출되는 함수
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Escape 키를 누를 때
        {
            PauseButton(); // 스킬 함수 호출
            inGameSound.SfxPlay(InGameSound.Sfx.ButtonClick);
        }
    }

    public void PauseButton()
    {
        // 모든 매뉴가 비활성화 상태 일 때
        if (!optionMenu.activeSelf && !skill.activeSelf && !itemMenu.activeSelf)
        {
            skill.SetActive(true);
            if (!levelUp.activeSelf)
            {
                Time.timeScale = 0;
            }
            FlipSound();
            SkillAnimator.SetTrigger("Open");
        }
        // 스킬 메뉴만 활성화 상태 일 때
        else if (!optionMenu.activeSelf && skill.activeSelf && !itemMenu.activeSelf)
        {
            UiOff();
            FlipSound();
            SkillAnimator.SetTrigger("Close");
        }
        // 옵션 메뉴만 활성화 상태 일 때
        else if (optionMenu.activeSelf && !skill.activeSelf && !itemMenu.activeSelf)
        {
            UiOff();
            FlipSound();
            OptionAnimator.SetTrigger("Close");
        }
        // 아이템 메뉴만 활성화 상태 일 때
        else if (!optionMenu.activeSelf && !skill.activeSelf && itemMenu.activeSelf)
        {
            UiOff();
            FlipSound();
            ItemAnimator.SetTrigger("Close");
        }
    }

    // 스킬 버튼을 누를 때 호출되는 함수
    public void SkillButton()
    {
        // 옵션 메뉴만 활성화된 경우
        if (optionMenu.activeSelf && !skill.activeSelf && !itemMenu.activeSelf)
        {
            UiOff();

            optionMenu.SetActive(false);
            skill.SetActive(true);
            FlipSound();
            SkillAnimator.SetTrigger("Flip"); // 옵션 메뉴 닫기 애니메이션 실행
        }
        // 아이템 메뉴만 활성화된 경우
        else if (!optionMenu.activeSelf && !skill.activeSelf && itemMenu.activeSelf)
        {
            UiOff();
            itemMenu.SetActive(false);
            skill.SetActive(true);
            FlipSound();
            SkillAnimator.SetTrigger("Flip");
        }
        // 스킬 메뉴만 활성화된 경우
        else if (!optionMenu.activeSelf && skill.activeSelf && !itemMenu.activeSelf)
        {
            return;
        }
    }
    public void ItemButton()
    {
        // 옵션 메뉴만 활성화된 경우
        if (optionMenu.activeSelf && !skill.activeSelf && !itemMenu.activeSelf)
        {
            UiOff();

            optionMenu.SetActive(false);
            itemMenu.SetActive(true);
            FlipSound();
            ItemAnimator.SetTrigger("Flip");
        }
        // 스킬 메뉴만 활성화된 경우
        else if (!optionMenu.activeSelf && skill.activeSelf && !itemMenu.activeSelf)
        {
            UiOff();

            skill.SetActive(false);
            itemMenu.SetActive(true);
            FlipSound();
            ItemAnimator.SetTrigger("Flip");
        }
        // 아이템 메뉴만 활성화된 경우
        else if (!optionMenu.activeSelf && !skill.activeSelf && itemMenu.activeSelf)
        {
            return;
        }
    }

    // 옵션 버튼을 누르면 옵션 창을 열어주는 함수
    public void Option()
    {
        // 아이템 메뉴만 활성화된 경우
        if (!optionMenu.activeSelf && !skill.activeSelf && itemMenu.activeSelf)
        {
            UiOff();

            itemMenu.SetActive(false);
            optionMenu.SetActive(true);
            FlipSound();
            OptionAnimator.SetTrigger("Flip");
        }
        // 스킬 메뉴만 활성화된 경우
        else if (!optionMenu.activeSelf && skill.activeSelf && !itemMenu.activeSelf)
        {
            UiOff();

            skill.SetActive(false);
            optionMenu.SetActive(true);
            FlipSound();
            OptionAnimator.SetTrigger("Flip");
        }
        // 옵션 메뉴만 활성화된 경우
        else if (optionMenu.activeSelf && !skill.activeSelf && !itemMenu.activeSelf)
        {
            return;
        }
    }

    // 스킬 버튼을 누르면 스킬 UI를 활성화하는 함수
    public void OnSkill()
    {
        skillUi.SetActive(true);
    }

    // 취소 버튼을 누를 때 호출되는 함수
    public void Cancel()
    {
        // 세부 ui 끄기
        UiOff();

        // 스킬 메뉴만 활성화된 경우
        if (!optionMenu.activeSelf && skill.activeSelf && !itemMenu.activeSelf)
        {
            SkillAnimator.SetTrigger("Close"); // 스킬 메뉴 닫기 애니메이션 실행
            FlipSound();
            if (SkillAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && SkillAnimator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                AllInactive(); // 모든 UI 비활성화 함수 호출
            }
        }
        // 옵션 메뉴만 활성화된 경우
        else if (optionMenu.activeSelf && !skill.activeSelf && !itemMenu.activeSelf)
        {
            OptionAnimator.SetTrigger("Close"); // 옵션 메뉴 닫기 애니메이션 실행
            FlipSound();
            if (OptionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && OptionAnimator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                AllInactive(); // 모든 UI 비활성화 함수 호출
            }
        }
        else if (!optionMenu.activeSelf && !skill.activeSelf && itemMenu.activeSelf)
        {
            ItemAnimator.SetTrigger("Close");
            FlipSound();
            if (ItemAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && ItemAnimator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                AllInactive(); // 모든 UI 비활성화 함수 호출
            }
        }
    }
    // 모든 UI를 비활성화하는 함수
    public void AllInactive()
    {
        UiOff();
        itemMenu.SetActive(false); // 아이템 메뉴 비활성화
        skill.SetActive(false); // 스킬 메뉴 비활성화
        optionMenu.SetActive(false); // 옵션 메뉴 비활성화
        if (!levelUp.activeSelf)
        {
            Time.timeScale = 1; // 게임 재개
        }
    }
    public void UiOff()
    {
        itemUi.SetActive(false); // 아이템 세부 UI 비활성화
        skillUi.SetActive(false); // 스킬 세부 UI를 비활성화
        optionUi.SetActive(false); // 옵션 세부 UI를 비활성화
    }

    // 닫힘 애니메이션이 끝날 때 호출되는 메서드입니다.
    public void DeactivateMenu()
    {
        AllInactive();
    }

    // 버튼 비활성화 함수
    public void InactiveButtons()
    {
        ButtonsActivator.InactiveButtons(); // 버튼 비활성화 함수 호출
    }
    public void GoMain()
    {
       
        LoadingSceneController.LoadingScene("main");
        //GameFadeOut.SetActive(true);
        //fadeEffect.GameFadeEffectStart();
    }

    // 게임 종료 함수
    public void GameExit()
    {
        GameFadeOut.SetActive(true);
        fadeEffect.GameFadeEffectStart();
    }

    // 옵션 메뉴 책을 넘기는 애니매이션이 끝날 때 호출되는 함수
    public void OnFlipOption()
    {
        optionUi.SetActive(true); // 옵션 세부 UI를 활성화
    }
    public void OnFlipSkill()
    {
        skillUi.SetActive(true);
    }
    public void OnFlipItem()
    {
        itemUi.SetActive(true);
    }

    public void FlipSound()
    {
        inGameSound.SfxPlay(InGameSound.Sfx.FlipPage);
    }
}
