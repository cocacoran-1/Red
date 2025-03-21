using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Boots Object", menuName = "Equipment/Boots")]
public class BootsObject : EquipmentData
{
    public void Awake()
    {
        type = ItemType.Boots;
        basicType = basicStatusType.defense;
    }
}
