using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenuControl : MonoBehaviour
{
    [Header("메뉴 오브젝트")]
    public GameObject ShopMenu;
    public GameObject ItemMenu;
    public GameObject MainMenu;
    public GameObject RuneMenu;
    public GameObject ChallengeMenu;
    public GameObject ReinforceMenu;
    public GameObject Equipment_Item;

    [Header("메인 기능")]
    public GameObject Mail;
    public GameObject MailButton;
    public GameObject SoundOff;
    public GameObject SoundOn;
    public GameObject SoundButton;
    // public GameObject SoundoffButton;
    public GameObject Attend;
    public GameObject AttendButton;
    public GameObject Task;
    public GameObject TaskButton;
    public GameObject Achievement;
    public GameObject AchievementButton;


    [Header("페이드효과")]
    public MainFadeEffect mainFadeEffect;
    public GameObject FadeEffectImage;

    [Header("맵 이미지")]
    public Text mapName;
    private string[] mapNameStr = { "Desert", "Halloween", "Winter", "Dungeon", "Temple", "Lava" };
    public GameObject[] mapImages;
    public GameObject[] mapSilhouette;
    public Button leftButton;
    public Button rightButton;
    public Button startButton;
    private int selectedMapIndex = 0;

    [Header("사운드")]
    public MainMenuSound mainMenuSound;

    [Header("프로필")]
    public Player_Stat Player;
    public TMP_Text Gold_text;
    public TMP_Text Cash_text;
    public Image Invisible_btn;
    public Sprite off_invisible_btn;
    public Sprite on_invisible_btn;
    public Player_Status status;


    public Text DamageTxt;
    public Text MaxHpTxt;
    public EnergySystem Energy;
    public bool winterUnlock = false;
    public bool halloweenUnlock = false;
    public bool desertUnlock = false;
    public bool templeUnlock = false;
    public bool dungeonUnlock = false;
    public bool lavaUnlock = false;

    private void Awake()
    {
        mainMenuSound = GameObject.Find("SoundManager").GetComponent<MainMenuSound>();
    }
    private void Start()
    {
        Time.timeScale = 1;
        Change_Text();
        UpdateImages();
    }

    void OnEnable()
    {
        halloweenUnlock = System.Convert.ToBoolean(PlayerPrefs.GetInt("DesertLastBossKill"));
        winterUnlock = System.Convert.ToBoolean(PlayerPrefs.GetInt("HalloweenLastBossKill"));
        dungeonUnlock = System.Convert.ToBoolean(PlayerPrefs.GetInt("WinterLastBossKill"));
        templeUnlock = System.Convert.ToBoolean(PlayerPrefs.GetInt("DungeonLastBossKill"));
        lavaUnlock = System.Convert.ToBoolean(PlayerPrefs.GetInt("TempleLastBossKill"));
        
        //DeleteMapPlayerPrefs();
    }

    void DeleteMapPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("WinterLastBossKill");
        PlayerPrefs.DeleteKey("HalloweenLastBossKill");
        PlayerPrefs.DeleteKey("DesertLastBossKill");
        PlayerPrefs.DeleteKey("TempleLastBossKill");
        PlayerPrefs.DeleteKey("DungeonLastBossKill");
    }
    public void OnLeftButtonClick()
    {
        selectedMapIndex--;
        UpdateImages();
        mapImages[selectedMapIndex+1].SetActive(false);
    }

    public void OnRightButtonClick()
    {
        selectedMapIndex++;
        UpdateImages();
        mapImages[selectedMapIndex-1].SetActive(false);
    }

    private void UpdateImages() //맵관련 업데이트
    {
        for (int i = 0; i < mapSilhouette.Length; i++)
        {
            mapSilhouette[i].SetActive(i == selectedMapIndex);
            switch (selectedMapIndex)
            {
                case 0: //desertMap
                    startButton.interactable = true;
                    mapName.text = mapNameStr[selectedMapIndex].ToString();    //맵이름 업데이트
                    break;
                case 1: //halloweenMap
                    if (halloweenUnlock == true)
                    {
                        mapSilhouette[selectedMapIndex].SetActive(false);
                        mapImages[i].SetActive(i == selectedMapIndex);
                        startButton.interactable = true;
                        mapName.text = mapNameStr[selectedMapIndex].ToString();
                    }
                    else
                    {
                        startButton.interactable = false;
                        mapName.text = "?";
                    }
                    break;
                case 2: //winterMap
                    if (winterUnlock == true)
                    {
                        mapSilhouette[selectedMapIndex].SetActive(false);
                        mapImages[i].SetActive(i == selectedMapIndex);
                        startButton.interactable = true;
                        mapName.text = mapNameStr[selectedMapIndex].ToString();
                    }
                    else
                    {
                        startButton.interactable = false;
                        mapName.text = "?";
                    }
                    break;
                case 3: //dungeonMap
                    if (dungeonUnlock == true)
                    {
                        mapSilhouette[selectedMapIndex].SetActive(false);
                        mapImages[i].SetActive(i == selectedMapIndex);
                        startButton.interactable = true;
                        mapName.text = mapNameStr[selectedMapIndex].ToString();
                    }
                    else
                    {
                        startButton.interactable = false;
                        mapName.text = "?";
                    }
                    break;
                case 4: //templeMap
                    if (templeUnlock == true)
                    {
                        mapSilhouette[selectedMapIndex].SetActive(false);
                        mapImages[i].SetActive(i == selectedMapIndex);
                        startButton.interactable = true;
                        mapName.text = mapNameStr[selectedMapIndex].ToString();
                    }
                    else
                    {
                        startButton.interactable = false;
                        mapName.text = "?";
                    }
                    break;
                case 5: //lavaMap
                    if (lavaUnlock == true)
                    {
                        mapSilhouette[selectedMapIndex].SetActive(false);
                        mapImages[i].SetActive(i == selectedMapIndex);
                        startButton.interactable = true;
                        mapName.text = mapNameStr[selectedMapIndex].ToString();
                    }
                    else
                    {
                        startButton.interactable = false;
                        mapName.text = "?";
                    }
                    break;
                default:
                    break;
            }
        }

        leftButton.interactable = selectedMapIndex > 0;
        rightButton.interactable = selectedMapIndex < mapImages.Length - 1;
    }

    public void Change_Text()
    {
        Gold_text.text = FormatNumber(Player.Gold);
        Cash_text.text = FormatNumber(Player.Cash);
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

    public void ShopClick()
    {
        AllMenuFalse();
        mainMenuSound.ButtonClickSound();
        ShopMenu.SetActive(true);
    }
    public void ItemClick()
    {
        AllMenuFalse();
        mainMenuSound.ButtonClickSound();
        ItemMenu.SetActive(true);
        Equipment_Item.SetActive(true);
        DamageTxt.text = "Damage\n" + Player.Damage;
        MaxHpTxt.text = "MaxHp\n" + Player.Max_Hp;
    }
    public void MainClick()
    {
        AllMenuFalse();
        mainMenuSound.ButtonClickSound();
        MainMenu.SetActive(true);
    }
    public void RuneClick()
    {
        AllMenuFalse();
        mainMenuSound.ButtonClickSound();
        RuneMenu.SetActive(true);
    }
    public void ChallengeClick()
    {
        AllMenuFalse();
        mainMenuSound.ButtonClickSound();
        ChallengeMenu.SetActive(true);
    }
    public void ReinforceClick()
    {
        mainMenuSound.ButtonClickSound();
        Equipment_Item.SetActive(false);
        ReinforceMenu.SetActive(true);
    }
    public void ReturnItemClick()
    {
        mainMenuSound.ButtonClickSound();
        ReinforceMenu.GetComponent<Reinforce>().item_image.gameObject.SetActive(false);
        //ReinforceMenu.GetComponent<Reinforce>().beforeLevelTxt.gameObject.SetActive(false);
        ReinforceMenu.SetActive(false);
        Equipment_Item.SetActive(true);
    }
    public void MailClick()
    {
        mainMenuSound.ButtonClickSound();
        Mail.SetActive(true);
    }
    public void SoundOnClick()
    {
        mainMenuSound.ButtonClickSound();
        SoundOff.SetActive(true);
        mainMenuSound.TurnOnAudio();
    }
    public void SoundOffClick()
    {
        mainMenuSound.ButtonClickSound();
        SoundOff.SetActive(false);
        mainMenuSound.TurnOffAudio();
    }
    public void AttendClick()
    {
        mainMenuSound.ButtonClickSound();
        Attend.SetActive(true);
    }
    public void TaskClick()
    {
        mainMenuSound.ButtonClickSound();
        Task.SetActive(true);
    }
    public void AchievementClick()
    {
        mainMenuSound.ButtonClickSound();
        Achievement.SetActive(true);
    }
    public void ExitClik()
    {
        mainMenuSound.ButtonClickSound();
        if (Mail.activeSelf)
        {
            Mail.SetActive(false);
        }
        else if (Attend.activeSelf)
        {
            Attend.SetActive(false);
        }
        else if (Task.activeSelf)
        {
            Task.SetActive(false);
        }
        else if (Achievement.activeSelf)
        {
            Achievement.SetActive(false);
        }
    }
    public void StartClick()
    {
        mainMenuSound.ButtonClickSound();
        if (Player.energy >= 5)
        {
            status.Daily.Game_Start_Count++;
            Player.energy -= 5;
            Energy.SaveEnergy();
            PlayerPrefs.SetInt("SelectedMapIndex", selectedMapIndex);
            FadeEffectImage.SetActive(true);
            LoadingSceneController.LoadingScene("InGameScene");
            //mainFadeEffect.StartFadeEffectStart();
        }
    }

    public void InvisibleClick()
    {
        mainMenuSound.ButtonClickSound();
        if (MailButton.activeSelf)
        {
            Invisible_btn.sprite = off_invisible_btn;
            SoundButton.SetActive(false);
        }
        else
        {
            Invisible_btn.sprite = on_invisible_btn;
            SoundButton.SetActive(true);
        }

        MailButton.SetActive(!MailButton.activeSelf);
        AttendButton.SetActive(!AttendButton.activeSelf);
        TaskButton.SetActive(!TaskButton.activeSelf);
        AchievementButton.SetActive(!AchievementButton.activeSelf);
    }

    public void AllMenuFalse()
    {
        ShopMenu.SetActive(false);
        ItemMenu.SetActive(false);
        MainMenu.SetActive(false);
        RuneMenu.SetActive(false);
        ChallengeMenu.SetActive(false);
        ReinforceMenu.SetActive(false);
    }
}
