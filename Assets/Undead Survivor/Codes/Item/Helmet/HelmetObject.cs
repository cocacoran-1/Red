using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Helmet Object", menuName = "Equipment/Helmet")]
public class HelmetObject : EquipmentData
{
    public void Awake()
    {
        type = ItemType.Helmet;
        basicType = basicStatusType.health;
        //강화를 할때마다 체력이 얼만큼올라가야하는지 빈공간이 잇어야댐
        
    }
}
