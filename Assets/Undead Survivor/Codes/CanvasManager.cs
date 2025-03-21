using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("SkillSelection")]
    WeaponSelect weaponSelect;
    [Header("결과창")]
    public FadeEffect fadeEffect;
    public GameObject ResultFadeOut;
    public TMP_Text ResultTitle;
    public GameObject gameover_image;
    public GameObject gameClear_image;
    public Image Result_image;
    public enum ButtonPosition
    {
        Position1,
        Position2,
        Position3
    }
    public GameObject AppearSkills;  // 나타나는 애니메이션을 가진 객체
    public Animator AppearAnim;
    [Header("Top Canvas")]
    public Text KillTxt;
    public Text ResultKillTxt;
    public int enemykillCount;
    public Text coinTxt;
    public Text ResultCoinTxt;
    public float coin;
    public Text ResultUpgradeTxt;
    public int upgrade;

    public Text timer;
    public Text ResultTimerTxt;
    public int min, sec;
    public float gameTime;
    public float maxGameTime = 900f;
    GameManager gameManager;
    public Player_history playerHistory;
    public Daily_history dailyhistory;
    public Player_Stat stat;
    LevelUp levelUp;
    public GameObject UiLevelUp;
    public Button Pause_Btn;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelUp = GetComponent<LevelUp>();
    }
    private IEnumerator Start()
    {
        gameManager.Stop();
        Pause_Btn.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);

        UiLevelUp.SetActive(true);
        Pause_Btn.gameObject.SetActive(true);
        StartCoroutine(levelUp.StartSkill());
    }


    private void Update()
    {
        if (!gameManager.isLive)
        {
            return;
        }
        Timer();
       
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;

        }

        if (AppearSkills != null)
        {
            if (AppearSkills.activeSelf && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                AppearAnim.SetTrigger("Appear");
            }
        }

    }
    public void EnemyKill()
    {
        enemykillCount++;
        KillTxt.text = enemykillCount.ToString();
        ResultKillTxt.text = enemykillCount.ToString();
    }

    public void GetCoin()
    {
        coin += 100;
        stat.Gold += 100;
        coinTxt.text = FormatNumber(coin);
        ResultCoinTxt.text = FormatNumber(coin);
    }

    public void GetUpgrade()
    {
        upgrade++;
        stat.Upgrade_Item++;
        ResultUpgradeTxt.text = upgrade.ToString();
    }

    public void Timer()
    {
        if (timer != null)
        {
            sec = (int)gameTime % 60;
            min = (int)gameTime / 60;

            timer.text = string.Format("{0:D1}:{1:D2}", min, sec); //분:초 타이머
            ResultTimerTxt.text = string.Format("{0:D1}:{1:D2}", min, sec);
        }
    }

    public void GameOver() // 게임 종료 
    {
        ResultFadeOut.SetActive(true);
        ResultTitle.text = "Game Over..";
        gameover_image.SetActive(true);
        gameClear_image.SetActive(false);
        fadeEffect.ResultFadeEffectStart();
    }

    public void GameClear()
    {
        ResultFadeOut.SetActive(true);
        ResultTitle.text = "Game Clear!!";
        gameover_image.SetActive(false);
        gameClear_image.SetActive(true);
        fadeEffect.ResultFadeEffectStart();
        int selectedMapIndex = PlayerPrefs.GetInt("SelectedMapIndex");
        switch (selectedMapIndex)
        {
            case 0:
                PlayerPrefs.SetInt("DesertLastBossKill", System.Convert.ToInt16(true));
                break;
            case 1:
                PlayerPrefs.SetInt("HalloweenLastBossKill", System.Convert.ToInt16(true));
                break;
            case 2:
                PlayerPrefs.SetInt("WinterLastBossKill", System.Convert.ToInt16(true));
                break;
            case 3:
                PlayerPrefs.SetInt("DungeonLastBossKill", System.Convert.ToInt16(true));
                break;
            case 4:
                PlayerPrefs.SetInt("TempleLastBossKill", System.Convert.ToInt16(true));
                break;
            case 5:
                PlayerPrefs.SetInt("LavaLastBossKill", System.Convert.ToInt16(true));
                break;
            default:
                break;
        }
    }
    string FormatNumber(float num)
    {
        if (num >= 10000)
        {
            return (num / 1000) + "k";
        }
        else
        {
            return num.ToString();
        }
    }

}