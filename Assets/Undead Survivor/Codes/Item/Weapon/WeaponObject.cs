using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon Object",menuName ="Equipment/Weapon")]
public class WeaponObject : EquipmentData
{
    public void Awake()
    {
        type = ItemType.Weapon;
        basicType = basicStatusType.damage;
    }
}
