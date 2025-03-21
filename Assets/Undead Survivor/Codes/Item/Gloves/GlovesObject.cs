using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Gloves Object", menuName = "Equipment/Gloves")]
public class GlovesObject : EquipmentData
{
    public void Awake()
    {
        type = ItemType.Gloves;
        basicType = basicStatusType.health;
    }
}
