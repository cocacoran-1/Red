using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Armor Object", menuName = "Equipment/Armor")]
public class ArmorObject : EquipmentData
{
    public void Awake()
    {
        type = ItemType.Armor;
        basicType = basicStatusType.defense;
    }
}
