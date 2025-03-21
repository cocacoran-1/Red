using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reinforce : MonoBehaviour
{
    public Image item_image;
    public Image Frame;
    public EquipmentData item;
    public Player_Stat stat;
    public TMP_Text beforeLevelTxt;
    public TMP_Text afterLevelTxt;
    public PoolManager pool;
    public Text Gold_text;
    public Text Upgrade_text;
    public Sprite SS_Frame;
    public Sprite S_Frame;
    public Sprite A_Frame;
    public Sprite B_Frame;
    public Sprite C_Frame;
    public Sprite D_Frame;
    public GameObject btn;
    public GameObject back_btn;
    public Player_Status player_stat;
    public Text beforeStatTxt;
    public Text afterStatTxt;
    public Slot slot;
    public Item_info info;

    public void image_change(EquipmentData data)
    {
        this.item = data;
        beforeLevelTxt.gameObject.SetActive(true);
        beforeLevelTxt.text = "+" + data.Upgrade_Level;
        afterLevelTxt.text = "+" + (data.Upgrade_Level + 1);
        if (data.basicType == basicStatusType.damage)  //만약 강화시 기본스탯이 데미지타입이라면
        {
            beforeStatTxt.text = data.basicType.ToString() + " " + data.Damage;
            afterStatTxt.text = data.basicType.ToString() + " " + (data.Damage + data.upgradeStatusAmount);
        }
        else if (data.basicType == basicStatusType.health)
        {
            beforeStatTxt.text = data.basicType.ToString() + " " + data.Max_Hp;
            afterStatTxt.text = data.basicType.ToString() + " " + (data.Max_Hp + data.upgradeStatusAmount);
        }
        else if (data.basicType == basicStatusType.defense)
        {
            beforeStatTxt.text = data.basicType.ToString() + " " + data.Defense;
            afterStatTxt.text = data.basicType.ToString() + " " + (data.Defense + data.upgradeStatusAmount);
        }
        item_image.gameObject.SetActive(true);
        item_image.sprite = data.texture;
        text_change();
        Frame_Change();
    }

    public void upgrade()
    {
        if (item != null)
        {
            if (stat.Gold >= item.Gold_cost && stat.Upgrade_Item >= item.UpgradeItem_cost)
            {
                GameObject effect = pool.GetEnemy(0);
                effect.transform.position = item_image.transform.position;

                stat.Gold -= item.Gold_cost;
                stat.Upgrade_Item -= item.UpgradeItem_cost;
                item.Upgrade();
                player_stat.PlayerHistory.Reinforcement_Count++;

                player_stat.Daily.Reinforcement_Count++;

                if (item.basicType == basicStatusType.damage)  //만약 강화시 기본스탯이 데미지타입이라면
                {
                    item.Damage += item.upgradeStatusAmount;
                }
                else if (item.basicType == basicStatusType.health)
                {
                    item.Max_Hp += item.upgradeStatusAmount;
                }
                else if (item.basicType == basicStatusType.defense)
                {
                    item.Defense += item.upgradeStatusAmount;
                }

                beforeLevelTxt.text = "+" + FormatNumber(item.Upgrade_Level);
                afterLevelTxt.text = "+" + FormatNumber((item.Upgrade_Level + 1));
                if (item.basicType == basicStatusType.damage)  //만약 강화시 기본스탯이 데미지타입이라면
                {
                    beforeStatTxt.text = item.basicType.ToString() + " " + item.Damage;
                    afterStatTxt.text = item.basicType.ToString() + " " + (item.Damage + item.upgradeStatusAmount);
                }
                else if (item.basicType == basicStatusType.health)
                {
                    beforeStatTxt.text = item.basicType.ToString() + " " + item.Max_Hp;
                    afterStatTxt.text = item.basicType.ToString() + " " + (item.Max_Hp + item.upgradeStatusAmount);
                }
                else if (item.basicType == basicStatusType.defense)
                {
                    beforeStatTxt.text = item.basicType.ToString() + " " + item.Defense;
                    afterStatTxt.text = item.basicType.ToString() + " " + (item.Defense + item.upgradeStatusAmount);
                }

                text_change();
            }
        }
    }

    public void text_change()
    {
        Gold_text.color = Color.white;
        Upgrade_text.color = Color.white;
        Gold_text.text = FormatNumber(stat.Gold) + "/" + FormatNumber(item.Gold_cost);
        if (stat.Gold < item.Gold_cost)
        {
            Gold_text.color = Color.red;
        }
        Upgrade_text.text = FormatNumber(stat.Upgrade_Item) + "/" + FormatNumber(item.UpgradeItem_cost);
        if (stat.Upgrade_Item < item.UpgradeItem_cost)
        {
            Upgrade_text.color = Color.red;
        }
    }
    
    void Frame_Change()
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
    public void active_btn()
    {
        btn.SetActive(true);
        back_btn.SetActive(true);
    }
    public void hide_btn()
    {
        btn.SetActive(false);
        back_btn.SetActive(false);
    }

    string FormatNumber(int num)
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
