using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{

    [Header("Controls")]
    public Player_Info player_info;

    public bool isLive;

    [Header("UI References")]
    Player player;
    InGameSound inGameSound;
    CanvasManager canvasManager;

    [Header("Player Stats")]
    public float maxHealth;
    public float curHealth;
    public float Hp_Regen;
    float hp_Gap;

    [Header("Exp")]

    // 플레이어 현재 경험치 
    public float curExp = 0;
    //현재 레벨
    public int curLevel = 0;
    // 다음 레벨까지 경험치
    public List<int> nextExp = new List<int>();
    // 최대 레벨 경험치
    public float lastExp;
    // 최대 레벨
    public int lastLevel;
    // 레벨 리스트 범위
    public int levelSize = 200;

    public Text LV_text;
    public bool bossSpawned;
    public LevelUp uiLevelUp;
    void Awake()
    {
        player_info = GetComponent<Player_Info>();
        inGameSound = GameObject.Find("InGameSound").GetComponent<InGameSound>();
        player = GameObject.Find("Player").GetComponent<Player>();
        canvasManager = GameObject.Find("GamePlayCanvas").GetComponent<CanvasManager>();
    }

    void Start()
    {
        // 테스트 코드 , 게임 시작할때 무기 선택
        Hp_Regen = player_info.Get_Hp_Regen();
        maxHealth = player_info.Get_Max_Hp();
        curHealth = maxHealth;
        StartCoroutine(Regen());
        LV_text.text = "LV. " + (curLevel+1);
        // 레벨별 필요 경험치 생성
        int curValue = 10;
        int val = 10;
        for (int i = 0; i < levelSize; i++)
        {
            nextExp.Add(curValue);
            curValue += val;
            val += 10;
        }
        // 최대레벨 필요 경험치 
        lastExp = nextExp[nextExp.Count - 1];
        // 최대레벨
        lastLevel = nextExp.Count - 1;
    }

    public void test_hp_up()
    {
        curHealth += 100;
    }
    public void GetExp(float exp) // 경험치 획득
    {
        curExp += exp;
        bool leveledUp = false; // 레벨업 여부를 확인하는 변수를 추가합니다.

        while (curExp >= nextExp[curLevel])
        {
            float excessExp = curExp - nextExp[curLevel];
            curLevel++; // 레벨 업
            curExp = 0; // 현재 경험치 초기화

            // 최대 레벨 증가
            int lastIndex = nextExp.Count - 1;
            // 생성된 최대 레벨의 경험치 생성
            int lastValue = nextExp[lastIndex] + 10 * (lastIndex - levelSize + 2);
            nextExp.Add(lastValue);
            // 생성된 최대 레벨의 경험치 값 얻기
            lastExp = nextExp[nextExp.Count - 1];
            // 생성된 최대 레벨 값 얻기
            lastLevel = nextExp.Count - 1;
            LV_text.text = "LV. " + (curLevel + 1);
            curExp += excessExp;
            leveledUp = true; // 레벨업 여부를 true로 설정합니다.
        }
      
        if (leveledUp) // 레벨업이 발생한 경우에만 사운드와 UI를 실행합니다.
        {
            inGameSound.SfxPlay(InGameSound.Sfx.LevelUp); // 레벨업 사운드
            uiLevelUp.Show();
        }
    }

    public void Init()
    {
        hp_Gap = player_info.Get_Max_Hp() - maxHealth;
        maxHealth = player_info.Get_Max_Hp();
        curHealth += hp_Gap;
        Hp_Regen = player_info.Get_Hp_Regen();
    }
    IEnumerator Regen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            curHealth += Hp_Regen;
            if (curHealth >= maxHealth)
                curHealth = maxHealth;
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}