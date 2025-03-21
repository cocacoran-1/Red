using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GachaSystem : MonoBehaviour
{
    public All_Item all_Item;
    public InventoryObject player_inventory;
    public InventorySlot finditem;
    public Player_Stat player;
    EquipmentData selected_item;
    public Text Gacha_stack;
    public GachaPanel panel;
    public GameObject panel_effect;
    public PoolManager pool;
    public MainMenuControl mainmenu;
    public Player_Status player_status;
    public EquipmentData RollGacha(int num)//SS~D등급 까지 , 100번뽑으면 S,SS중 하나 무조건 나옴
    {
        EquipmentData selectedItem = null;
        float randomValue = Random.Range(0f, 100f);

        List<EquipmentData> selectedGradeItems = null;
        if (num == 0)
        {
            if (randomValue <= 0.1f) // 0.1% 확률로 SS 등급 선택
            {
                selectedGradeItems = all_Item.SS_Item;

            }
            else if (randomValue <= 1.3f) // 1.2% 확률로 S 등급 선택
            {
                selectedGradeItems = all_Item.S_Item;

            }
            else if (randomValue <= 10f) // 8.7% 확률로 A 등급 선택
            {
                selectedGradeItems = all_Item.A_Item;

            }
            else if (randomValue <= 22f) // 12% 확률로 B 등급 선택
            {
                selectedGradeItems = all_Item.B_Item;

            }
            else if (randomValue <= 52f) // 30% 확률로 C 등급 선택
            {
                selectedGradeItems = all_Item.C_Item;

            }
            else // 51% 확률로 D 등급 선택
            {
                selectedGradeItems = all_Item.D_Item;

            }
        }
        else if (num == 1)
        {
            if (randomValue <= 30f) // 30% 확률로 SS 등급 선택
            {
                selectedGradeItems = all_Item.SS_Item;

            }
            else
            {
                selectedGradeItems = all_Item.S_Item;

            }
        }


        int randomIndex = Random.Range(0, selectedGradeItems.Count);
        selectedItem = selectedGradeItems[randomIndex];
        player_status.Daily.Gacha_Count++;
        player_status.Daily.Cash_Gacha_Count++;
        player_status.PlayerHistory.Gacha_Count++;// 플레이어의 누적 뽑기 횟수
        return selectedItem;

    }
    public EquipmentData Gold_RollGacha()//S~D등급 까지 
    {
        EquipmentData selectedItem = null;
        float randomValue = Random.Range(0f, 100f);

        List<EquipmentData> selectedGradeItems = null;


        if (randomValue <= 1.3f) // 1.2% 확률로 S 등급 선택
        {
            selectedGradeItems = all_Item.S_Item;

        }
        else if (randomValue <= 10f) // 8.7% 확률로 A 등급 선택
        {
            selectedGradeItems = all_Item.A_Item;

        }
        else if (randomValue <= 22f) // 12% 확률로 B 등급 선택
        {
            selectedGradeItems = all_Item.B_Item;

        }
        else if (randomValue <= 52f) // 30% 확률로 C 등급 선택
        {
            selectedGradeItems = all_Item.C_Item;

        }
        else // 51% 확률로 D 등급 선택
        {
            selectedGradeItems = all_Item.D_Item;

        }



        int randomIndex = Random.Range(0, selectedGradeItems.Count);
        selectedItem = selectedGradeItems[randomIndex];
        player_status.Daily.Gacha_Count++;
        return selectedItem;

    }
    public void start_Gacha_10()
    {
        if (player.Cash >= 1000)
        {
            player.Cash -= 1000;
            player_status.PlayerHistory.Cash_Count += 1000;
            Gacha_10();
        }

    }
    public void Gacha_10()
    {
        panel.enabled = false;
        for (int i = 0; i < 10; ++i)
        {
            selected_item = null;
            finditem = null;
            if (player.Gacha_Stack == 100)
            {
                player.Gacha_Stack = 0;
                selected_item = RollGacha(1);
            }
            else
            {
                selected_item = RollGacha(0);
            }
            finditem = player_inventory.Container.Find(slot => slot.Equipment.Item_index == selected_item.Item_index);
            if (finditem != null)
            {
                panel.gameObject.SetActive(true);
                GameObject Gacha_slot = pool.GetEnemy(0);
                Gacha_slot.transform.SetParent(panel.transform);
                var my_slot = Gacha_slot.GetComponent<Gacha_Slot>();
                my_slot.image_change(selected_item);
                Debug.Log("중복!");
                player.Upgrade_Item += selected_item.Upgrade_item;
                player.Gacha_Stack++;

            }
            else
            {
                panel.gameObject.SetActive(true);
                GameObject Gacha_slot = pool.GetEnemy(0);
                Gacha_slot.transform.SetParent(panel.transform);
                var my_slot = Gacha_slot.GetComponent<Gacha_Slot>();
                my_slot.image_change_Upgrade(selected_item);
                Debug.Log(selected_item.type);
                if (selected_item.type == ItemType.Weapon)
                {
                    player_status.PlayerHistory.Weapon_Count++;
                }
                else
                {
                    player_status.PlayerHistory.Defense_Count++;
                }
                Debug.Log(selected_item.grade);
                player_inventory.AddItem(selected_item, 0);
                player.Gacha_Stack++;
            }



        }
        Time.timeScale = 0;
        panel_effect.SetActive(true);

       // panel.enabled = true;
       // panel.isTouch = false;
    }

    public void start_Gacha_1()
    {
        if (player.Cash >= 100)
        {
            player.Cash -= 100;
            player_status.PlayerHistory.Cash_Count += 100;
            Gacha_1();
        }
    }
    public void Gacha_1()
    {
        panel.enabled = false;
        selected_item = null;
        finditem = null;
        if (player.Gacha_Stack == 100)
        {
            player.Gacha_Stack = 0;
            selected_item = RollGacha(1);
        }
        else
        {
            selected_item = RollGacha(0);
        }
        finditem = player_inventory.Container.Find(slot => slot.Equipment.Item_index == selected_item.Item_index);

        if (finditem != null)
        {
            panel.gameObject.SetActive(true);
            GameObject Gacha_slot = pool.GetEnemy(0);
            Gacha_slot.transform.SetParent(panel.transform);
            var my_slot = Gacha_slot.GetComponent<Gacha_Slot>();
            my_slot.image_change(selected_item);
            Debug.Log("중복!");
            player.Upgrade_Item += selected_item.Upgrade_item;
            player.Gacha_Stack++;

        }
        else
        {
            panel.gameObject.SetActive(true);
            GameObject Gacha_slot = pool.GetEnemy(0);
            Gacha_slot.transform.SetParent(panel.transform);
            var my_slot = Gacha_slot.GetComponent<Gacha_Slot>();
            my_slot.image_change_Upgrade(selected_item);
            if (selected_item.type == ItemType.Weapon)
            {
                player_status.PlayerHistory.Weapon_Count++;
            }
            else
            {
                player_status.PlayerHistory.Defense_Count++;
            }
            Debug.Log(selected_item.grade);
            player_inventory.AddItem(selected_item, 0);
            player.Gacha_Stack++;
        }
        panel.enabled = true;
        panel.isTouch = false;

    }
    public void start_Gold_Gacha_10()
    {
        if (player.Gold >= 10000)
        {
            player.Gold -= 10000;
            Gold_Gacha_10();
        }
    }
    public void start_Gold_Gacha_1()
    {
        if (player.Gold >= 1000)
        {
            player.Gold -= 1000;
            Gold_Gacha_1();
        }
    }
    public void Gold_Gacha_10()
    {
        panel.enabled = false;
        for (int i = 0; i < 10; ++i)
        {
            selected_item = null;
            finditem = null;
            selected_item = Gold_RollGacha();
            finditem = player_inventory.Container.Find(slot => slot.Equipment.Item_index == selected_item.Item_index);
            if (finditem != null)
            {
                panel.gameObject.SetActive(true);
                GameObject Gacha_slot = pool.GetEnemy(0);
                Gacha_slot.transform.SetParent(panel.transform);
                var my_slot = Gacha_slot.GetComponent<Gacha_Slot>();
                my_slot.image_change(selected_item);
                Debug.Log("중복!");
                player.Upgrade_Item += selected_item.Upgrade_item;
            }
            else
            {
                panel.gameObject.SetActive(true);
                GameObject Gacha_slot = pool.GetEnemy(0);
                Gacha_slot.transform.SetParent(panel.transform);
                var my_slot = Gacha_slot.GetComponent<Gacha_Slot>();
                my_slot.image_change_Upgrade(selected_item);
                if (selected_item.type == ItemType.Weapon)
                {
                    player_status.PlayerHistory.Weapon_Count++;
                }
                else
                {
                    player_status.PlayerHistory.Defense_Count++;
                }
                Debug.Log(selected_item.grade);
                player_inventory.AddItem(selected_item, 0);
            }
        }
        Time.timeScale = 0;
        panel_effect.SetActive(true);
      
    }
    public void Gold_Gacha_1()
    {
        panel.enabled = false;
        selected_item = null;
        finditem = null;
        selected_item = Gold_RollGacha();
        finditem = player_inventory.Container.Find(slot => slot.Equipment.Item_index == selected_item.Item_index);
        if (finditem != null)
        {
            panel.gameObject.SetActive(true);
            GameObject Gacha_slot = pool.GetEnemy(0);
            Gacha_slot.transform.SetParent(panel.transform);
            var my_slot = Gacha_slot.GetComponent<Gacha_Slot>();
            my_slot.image_change(selected_item);
            Debug.Log("중복!");
            player.Upgrade_Item += selected_item.Upgrade_item;
        }
        else
        {
            panel.gameObject.SetActive(true);
            GameObject Gacha_slot = pool.GetEnemy(0);
            Gacha_slot.transform.SetParent(panel.transform);
            var my_slot = Gacha_slot.GetComponent<Gacha_Slot>();
            my_slot.image_change_Upgrade(selected_item);
            if (selected_item.type == ItemType.Weapon)
            {
                player_status.PlayerHistory.Weapon_Count++;
            }
            else
            {
                player_status.PlayerHistory.Defense_Count++;
            }
            Debug.Log(selected_item.grade);
            player_inventory.AddItem(selected_item, 0);
        }
        panel.enabled = true;
        panel.isTouch = false;
    }
    public void Stack_on()
    {
        Gacha_stack.gameObject.SetActive(true);
        Gacha_stack.text = player.Gacha_Stack + "/ 100";
    }

    public void Get_cash()
    {
        player.Cash += 1000;
        player_status.Daily.Payment_Count++;//광고 기능에 데일리 광고 스택 넣어야함.
        mainmenu.Change_Text();

    }
    public void Get_Gold()
    {
        player.Gold += 1000;
        player_status.PlayerHistory.Gold_Count += 1000;
        mainmenu.Change_Text();
    }

    public void Get_Upgrade()
    {
        player.Upgrade_Item += 100;
    }
    public void Get_Energy()
    {
        player.energy += 10;
        mainmenu.Energy.SaveEnergy();
    }
}
