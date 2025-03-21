using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Earring Object", menuName = "Equipment/Earring")]
public class EarringObject : EquipmentData
{
    public void Awake()
    {
        type = ItemType.Earring;
        basicType = basicStatusType.health;
    }
}
