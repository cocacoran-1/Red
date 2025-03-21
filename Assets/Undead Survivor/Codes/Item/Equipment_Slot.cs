using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Slot : MonoBehaviour
{
    public EquipmentData player;
    public GameObject Iteminfo;
    public void open_item_info()
    {
        if(player!=null)
        {
            Iteminfo.SetActive(true);
            Iteminfo.GetComponent<Item_info>().select_item(player);
        }
        
    }
}
