using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_info : MonoBehaviour
{
    public Slot slot;
    public EquipmentData item;
    public InventoryObject My_EquipmentData;
    public Player_Stat PlayerStat;
    public GameObject Item_in;
    public GameObject Item_out;
    public Image Frame;
    public TMP_Text FrameText;

    public Sprite SS_Name_Frame;
    public Sprite S_Name_Frame;
    public Sprite A_Name_Frame;
    public Sprite B_Name_Frame;
    public Sprite C_Name_Frame;
    public Sprite D_Name_Frame;

    public Sprite SS_Frame;
    public Sprite S_Frame;
    public Sprite A_Frame;
    public Sprite B_Frame;
    public Sprite C_Frame;
    public Sprite D_Frame;

    public Image Info_Frame;
    public Image Info_image;
    public Text itemTypeTxt;
    public Text reinforceNumTxt;
    public Text info;
    public Text DamageTxt;
    public Text MaxHpTxt;

    public void exit()
    {
        item = null;
        Item_in.SetActive(false);
        Item_out.SetActive(false);
        gameObject.SetActive(false);

    }
    public void select_item(EquipmentData data)//팝업창 내용
    {
        this.item = data;
        Change_Frame(data);
        if (data.IsEquipped == false)
        {
            Item_in.SetActive(true);
            Item_out.SetActive(false);
        }
        else if (data.IsEquipped == true)
        {
            Item_in.SetActive(false);
            Item_out.SetActive(true);
        }

        itemTypeTxt.text = data.type.ToString();  //아이템종류
        reinforceNumTxt.text = "+" + data.Upgrade_Level.ToString();  //강화수치
        if (data.basicType == basicStatusType.damage)
        {
            info.text = data.basicType.ToString() + ": " + data.Damage;
        }
        else if (data.basicType == basicStatusType.health)
        {
            info.text = data.basicType.ToString() + ": " + data.Max_Hp;
        }
        else if (data.basicType == basicStatusType.defense)
        {
            info.text = data.basicType.ToString() + ": " + data.Defense;
        }

    }
    public void Item_Equipment_in()// 장착 버튼을 눌렀을때
    {
        for (int i = 0; i < My_EquipmentData.Container.Count; ++i)
        {
            if (item.type == My_EquipmentData.Container[i].Equipment.type)
            {
                My_EquipmentData.Container[i].Equipment.IsEquipped = false;
                My_EquipmentData.Container[i].Equipment.sort_num *= 10;
                slot.my_slots.Find(x => x.item.inventory_index == My_EquipmentData.Container[i].Equipment.inventory_index).text.gameObject.SetActive(false);
                Stat_out(My_EquipmentData.Container[i].Equipment);

                My_EquipmentData.Container.RemoveAt(i);
                item.IsEquipped = true;
                slot.my_slots.Find(x => x.item.inventory_index == item.inventory_index).text.gameObject.SetActive(true);
                item.sort_num /= 10;
                My_EquipmentData.AddItem(item, 0);
                Stat_in(item);
                slot.In_Equipment(item);
                slot.SetInventory();

                DamageTxt.text = "Damage " + PlayerStat.Damage;
                MaxHpTxt.text = "MaxHp " + PlayerStat.Max_Hp;
                exit();
                break;
            }
        }

        if (item != null)//exit함수가 실행되었다면 item은 null값을 가지기때문에 실행되지 않음.
        {
            item.IsEquipped = true;
            item.sort_num /= 10;
            My_EquipmentData.AddItem(item, 0);
            slot.my_slots.Find(x => x.item.inventory_index == item.inventory_index).text.gameObject.SetActive(true);
            Stat_in(item);
            slot.In_Equipment(item);
            slot.SetInventory();

            DamageTxt.text = "Damage " + PlayerStat.Damage;
            MaxHpTxt.text = "MaxHp " + PlayerStat.Max_Hp;
            exit();
        }
    }
    public void Item_Equipment_out()// 해제 버튼을 눌렀을때
    {
        item.IsEquipped = false;
        item.sort_num *= 10;
        slot.my_slots.Find(x => x.item.inventory_index == item.inventory_index).text.gameObject.SetActive(false);
        Stat_out(item);
        My_EquipmentData.DestoryItem(item);
        slot.SetInventory();
        slot.Out_Equipment(item);

        DamageTxt.text = "Damage " + PlayerStat.Damage;
        MaxHpTxt.text = "MaxHp " + PlayerStat.Max_Hp;
        exit();
    }

    public void Stat_in(EquipmentData item)
    {
        slot.player.Player.Damage += item.Damage;
        slot.player.Player.AttackSpeed += item.AttackSpeed;
        slot.player.Player.Attack_Range += item.Attack_Range;
        slot.player.Player.Attack_Duration += item.Attack_Duration;
        slot.player.Player.Max_Hp += item.Max_Hp;
        slot.player.Player.Defense += item.Defense;
        slot.player.Player.Hp_Regen += item.Hp_Regen;
        slot.player.Player.Speed += item.Speed;
        slot.player.Player.Magnet_Range += item.Magnet_Range;
        slot.player.Player.Exp_Up += item.Exp_Up;
        slot.player.Player.Gold_Up += item.Gold_Up;
    }
    public void Stat_out(EquipmentData item)
    {
        slot.player.Player.Damage -= item.Damage;
        slot.player.Player.AttackSpeed -= item.AttackSpeed;
        slot.player.Player.Attack_Range -= item.Attack_Range;
        slot.player.Player.Attack_Duration -= item.Attack_Duration;
        slot.player.Player.Max_Hp -= item.Max_Hp;
        slot.player.Player.Defense -= item.Defense;
        slot.player.Player.Hp_Regen -= item.Hp_Regen;
        slot.player.Player.Speed -= item.Speed;
        slot.player.Player.Magnet_Range -= item.Magnet_Range;
        slot.player.Player.Exp_Up -= item.Exp_Up;
        slot.player.Player.Gold_Up -= item.Gold_Up;
    }

    void Change_Frame(EquipmentData item)
    {
        if (item.grade == ItemGrade.SS)
        {
            Frame.sprite = SS_Name_Frame;
            Info_Frame.sprite = SS_Frame;

            FrameText.text = "SS";
        }
        else if (item.grade == ItemGrade.S)
        {
            Frame.sprite = S_Name_Frame;
            Info_Frame.sprite = S_Frame;
            FrameText.text = "S";
        }
        else if (item.grade == ItemGrade.A)
        {
            Frame.sprite = A_Name_Frame;
            Info_Frame.sprite = A_Frame;
            FrameText.text = "A";
        }
        else if (item.grade == ItemGrade.B)
        {
            Frame.sprite = B_Name_Frame;
            Info_Frame.sprite = B_Frame;
            FrameText.text = "B";
        }
        else if (item.grade == ItemGrade.C)
        {
            Frame.sprite = C_Name_Frame;
            Info_Frame.sprite = C_Frame;
            FrameText.text = "C";
        }
        else
        {
            Frame.sprite = D_Name_Frame;
            Info_Frame.sprite = D_Frame;
            FrameText.text = "D";
        }

        Info_image.sprite = item.texture;
    }
}
