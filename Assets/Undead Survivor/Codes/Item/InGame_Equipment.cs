using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_Equipment : MonoBehaviour
{
    public InventoryObject player;
    public Image Weapon;
    public Image Helmet;
    public Image Armor;
    public Image Boots;
    public Image Gloves;
    public Image Earring;

    public void Set_Equipment()
    {
        for (int i = 0; i < player.Container.Count; ++i)
        {
            if (player.Container[i].Equipment.type == ItemType.Weapon)
            {
                Weapon.gameObject.SetActive(true);
               
                Weapon.sprite = player.Container[i].Equipment.texture;
            }
            else if (player.Container[i].Equipment.type == ItemType.Armor)
            {
                Armor.gameObject.SetActive(true);
               
                Armor.sprite = player.Container[i].Equipment.texture;
            }
            else if (player.Container[i].Equipment.type == ItemType.Helmet)
            {
                Helmet.gameObject.SetActive(true);
               
                Helmet.sprite = player.Container[i].Equipment.texture;
            }
            else if (player.Container[i].Equipment.type == ItemType.Gloves)
            {
                Gloves.gameObject.SetActive(true);
                
                Gloves.sprite = player.Container[i].Equipment.texture;
            }
            else if (player.Container[i].Equipment.type == ItemType.Boots)
            {
                Boots.gameObject.SetActive(true);
               
                Boots.sprite = player.Container[i].Equipment.texture;
            }
            else if (player.Container[i].Equipment.type == ItemType.Earring)
            {
                Earring.gameObject.SetActive(true);
               
                Earring.sprite = player.Container[i].Equipment.texture;
            }
        }
    }
}
