using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class My_Slot : MonoBehaviour
{
    public Image image;
    public GameObject item_info;
    public Reinforce ReinforceMenu;
    public EquipmentData item;
    public Image text;
    public Image Item_Frame;
    public Sprite SS_Frame;
    public Sprite S_Frame;
    public Sprite A_Frame;
    public Sprite B_Frame;
    public Sprite C_Frame;
    public Sprite D_Frame;

    private void Awake()
    {
        item_info = GameObject.Find("Popup_UI").transform.Find("item_info").gameObject;
        ReinforceMenu = GameObject.Find("UseItem").transform.Find("ReinforceMenu").GetComponent<Reinforce>();

    }

    public void setItem(EquipmentData Inventory)
    {
        if (Inventory.IsEquipped == true)
        {
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }
        item = Inventory;
        image.sprite = item.texture;
        if (item.grade == ItemGrade.SS)
        {
            Item_Frame.sprite = SS_Frame;
        }
        else if (item.grade == ItemGrade.S)
        {
            Item_Frame.sprite = S_Frame;
        }
        else if (item.grade == ItemGrade.A)
        {
            Item_Frame.sprite = A_Frame;
        }
        else if (item.grade == ItemGrade.B)
        {
            Item_Frame.sprite = B_Frame;
        }
        else if (item.grade == ItemGrade.C)
        {
            Item_Frame.sprite = C_Frame;
        }
        else
        {
            Item_Frame.sprite = D_Frame;
        }

    }

    public void exit()
    {
        gameObject.SetActive(false);
    }
    public void open_item_info()
    {
        if (ReinforceMenu.gameObject.activeSelf && item.IsEquipped == false)
        {

            ReinforceMenu.image_change(item);
        }
        else if(item.IsEquipped == true)
        {
            return;
        }
        else
        {
            item_info.SetActive(true);
            item_info.GetComponent<Item_info>().select_item(item);
        }
    }
}
