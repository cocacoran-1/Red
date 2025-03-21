using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public Transform RootSlot;
    public PoolManager PoolManager;
    //public GameObject SlotPrefab;
    public Player_Status player;
    public Image Weapon;
    public Image Armor;
    public Image Boots;
    public Image Earring;
    public Image Gloves;
    public Image Helmet;
    public Image Weapon_Frame;
    public Image Armor_Frame;
    public Image Boots_Frame;
    public Image Earring_Frame;
    public Image Gloves_Frame;
    public Image Helmet_Frame;
    public Sprite SS_Frame;
    public Sprite S_Frame;
    public Sprite A_Frame;
    public Sprite B_Frame;
    public Sprite C_Frame;
    public Sprite D_Frame;
    public Equipment_Slot Weapon_;
    public Equipment_Slot Armor_;
    public Equipment_Slot Boots_;
    public Equipment_Slot Earring_;
    public Equipment_Slot Gloves_;
    public Equipment_Slot Helmet_;

    public TMP_Text Player_stat;
    public GameObject Player_info;
    

    public Player_Stat PlayerStat;
    public List<My_Slot> my_slots;


    private void Awake()
    {
        my_slots = new List<My_Slot>();
    }

    public void SetInventory()// 인벤토리 데이터 초기화
    {
        if (my_slots != null)
        {
            for (int i = 0; i < my_slots.Count; i++)
            {
                my_slots[i].gameObject.SetActive(false);

            }
        }
        my_slots.Clear();
        //player.Inventory.Container.Sort((a, b) => a.Equipment.sort_num.CompareTo(b.Equipment.sort_num));
        player.Inventory.Container.Sort((item1, item2) =>
        {
            if (item1.Equipment.IsEquipped != item2.Equipment.IsEquipped)
            {
                return item2.Equipment.IsEquipped.CompareTo(item1.Equipment.IsEquipped); // 장착 여부에 따라 내림차순 정렬
            }
            else if (item1.Equipment.grade != item2.Equipment.grade)
            {
                return item1.Equipment.grade.CompareTo(item2.Equipment.grade); // 등급 내림차순 정렬
            }
            else
            {
                return item1.Equipment.sort_num.CompareTo(item2.Equipment.sort_num); // sort_num 오름차순 정렬
            }
        });
        for (int i = 0; i < player.Inventory.Container.Count; ++i)
        {
            GameObject slot = PoolManager.GetEnemy(0);
            slot.transform.SetParent(RootSlot.transform);
            var my_slot = slot.GetComponent<My_Slot>();

            player.Inventory.Container[i].Equipment.inventory_index = i;
            my_slot.setItem(player.Inventory.Container[i].Equipment);
            my_slots.Add(my_slot);


        }
    }
    public void Set_Equiupment()// 아이템 버튼을 누를때 장비데이터목록을 가져와서 장비창 초기화
    {
        for (int i = 0; i < player.Equipment.Container.Count; ++i)
        {
            if (player.Equipment.Container[i].Equipment.type == ItemType.Weapon)
            {
                Weapon.gameObject.SetActive(true);
                Weapon_.player = player.Equipment.Container[i].Equipment;
                Weapon.sprite = player.Equipment.Container[i].Equipment.texture;
                Change_Frame(Weapon_Frame, player.Equipment.Container[i].Equipment);
            }
            else if (player.Equipment.Container[i].Equipment.type == ItemType.Armor)
            {
                Armor.gameObject.SetActive(true);
                Armor_.player = player.Equipment.Container[i].Equipment;
                Armor.sprite = player.Equipment.Container[i].Equipment.texture;
                Change_Frame(Armor_Frame, player.Equipment.Container[i].Equipment);
            }
            else if (player.Equipment.Container[i].Equipment.type == ItemType.Helmet)
            {
                Helmet.gameObject.SetActive(true);
                Helmet_.player = player.Equipment.Container[i].Equipment;
                Helmet.sprite = player.Equipment.Container[i].Equipment.texture;
                Change_Frame(Helmet_Frame, player.Equipment.Container[i].Equipment);
            }
            else if (player.Equipment.Container[i].Equipment.type == ItemType.Gloves)
            {
                Gloves.gameObject.SetActive(true);
                Gloves_.player = player.Equipment.Container[i].Equipment;
                Gloves.sprite = player.Equipment.Container[i].Equipment.texture;
                Change_Frame(Gloves_Frame, player.Equipment.Container[i].Equipment);
            }
            else if (player.Equipment.Container[i].Equipment.type == ItemType.Boots)
            {
                Boots.gameObject.SetActive(true);
                Boots_.player = player.Equipment.Container[i].Equipment;
                Boots.sprite = player.Equipment.Container[i].Equipment.texture;
                Change_Frame(Boots_Frame, player.Equipment.Container[i].Equipment);
            }
            else if (player.Equipment.Container[i].Equipment.type == ItemType.Earring)
            {
                Earring.gameObject.SetActive(true);
                Earring_.player = player.Equipment.Container[i].Equipment;
                Earring.sprite = player.Equipment.Container[i].Equipment.texture;
                Change_Frame(Earring_Frame, player.Equipment.Container[i].Equipment);
            }

        }

    }
    public void In_Equipment(EquipmentData item)// 장비 장착시 온클릭
    {

        if (item.type == ItemType.Weapon)
        {
            Weapon.gameObject.SetActive(true);
            Weapon_.player = item;
            Weapon.sprite = item.texture;
            Change_Frame(Weapon_Frame, item);
        }
        else if (item.type == ItemType.Armor)
        {
            Armor.gameObject.SetActive(true);
            Armor_.player = item;
            Armor.sprite = item.texture;
            Change_Frame(Armor_Frame, item);
        }
        else if (item.type == ItemType.Helmet)
        {
            Helmet.gameObject.SetActive(true);
            Helmet_.player = item;
            Helmet.sprite = item.texture;
            Change_Frame(Helmet_Frame, item);
        }
        else if (item.type == ItemType.Gloves)
        {
            Gloves.gameObject.SetActive(true);
            Gloves_.player = item;
            Gloves.sprite = item.texture;
            Change_Frame(Gloves_Frame, item);
        }
        else if (item.type == ItemType.Boots)
        {
            Boots.gameObject.SetActive(true);
            Boots_.player = item;
            Boots.sprite = item.texture;
            Change_Frame(Boots_Frame, item);
        }
        else if (item.type == ItemType.Earring)
        {
            Earring.gameObject.SetActive(true);
            Earring_.player = item;
            Earring.sprite = item.texture;
            Change_Frame(Earring_Frame, item);
        }
    }
    public void Out_Equipment(EquipmentData item)//장비 해제시 온클릭
    {
        if (item.type == ItemType.Weapon)
        {
            Weapon.gameObject.SetActive(false);
            Weapon_.player = null;
            Weapon.sprite = null;
            Weapon_Frame.sprite = D_Frame;
        }
        else if (item.type == ItemType.Armor)
        {
            Armor.gameObject.SetActive(false);
            Armor_.player = null;
            Armor.sprite = null;
            Armor_Frame.sprite = D_Frame;
        }
        else if (item.type == ItemType.Helmet)
        {
            Helmet.gameObject.SetActive(false);
            Helmet_.player = null;
            Helmet.sprite = null;
            Helmet_Frame.sprite = D_Frame;
        }
        else if (item.type == ItemType.Gloves)
        {
            Gloves.gameObject.SetActive(false);
            Gloves_.player = null;
            Gloves.sprite = null;
            Gloves_Frame.sprite = D_Frame;
        }
        else if (item.type == ItemType.Boots)
        {
            Boots.gameObject.SetActive(false);
            Boots_.player = null;
            Boots.sprite = null;
            Boots_Frame.sprite = D_Frame;
        }
        else if (item.type == ItemType.Earring)
        {
            Earring.gameObject.SetActive(false);
            Earring_.player = null;
            Earring.sprite = null;
            Earring_Frame.sprite = D_Frame;
        }
    }
    void Change_Frame(Image Frame, EquipmentData item)
    {
        if (item.grade == ItemGrade.SS)
        {
            Frame.sprite = SS_Frame;
        }
        else if (item.grade == ItemGrade.S)
        {
            Frame.sprite = S_Frame;
        }
        else if (item.grade == ItemGrade.A)
        {
            Frame.sprite = A_Frame;
        }
        else if (item.grade == ItemGrade.B)
        {
            Frame.sprite = B_Frame;
        }
        else if (item.grade == ItemGrade.C)
        {
            Frame.sprite = C_Frame;
        }
        else
        {
            Frame.sprite = D_Frame;
        }
    }
    public void stat_change()
    {
        Player_info.SetActive(true);
        Player_stat.text = string.Format(
    "<b>공격 관련</b>\n" +
    "공격력: {0}\n" +
    "공격속도: {1}\n" +
    "공격범위: {2}\n" +
    "공격 지속시간: {3}\n\n" +
    "<b>체력 관련</b>\n" +
    "최대체력: {4}\n" +
    "방어력 증가: {5}\n" +
    "체력 회복: {6}\n\n" +
    "<b>기타</b>\n" +
    "이동속도: {7}\n" +
    "자석 범위: {8}\n" +
    "경험치 증가: {9}\n" +
    "골드 증가: {10}",
    PlayerStat.Damage, PlayerStat.AttackSpeed, PlayerStat.Attack_Range, PlayerStat.Attack_Duration,
    PlayerStat.Max_Hp, PlayerStat.Defense, PlayerStat.Hp_Regen,
    PlayerStat.Speed, PlayerStat.Magnet_Range, PlayerStat.Exp_Up, PlayerStat.Gold_Up);
    }
    public void player_info_off()
    {
        Player_info.SetActive(false);
    }

}
