using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Inventory",menuName ="Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container=new List<InventorySlot> ();
    public void AddItem(EquipmentData equipment,int _amount)
    {
        bool hasItem = false;
        for(int i = 0; i < Container.Count; i++)
        {
            if(Container[i].Equipment == equipment)
            {
                Container[i].Addamount(_amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem)
        {
            Container.Add(new InventorySlot(equipment, _amount));

        }
    }
    public void DestoryItem(EquipmentData equipment)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].Equipment == equipment)
            {

                Container.RemoveAt(i);
                break;
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public EquipmentData Equipment;
    public int amount;
    public InventorySlot(EquipmentData _equipment,int _amount)
    {
        Equipment = _equipment;
        amount = _amount;
    }
    public void Addamount(int value)
    {
        amount += value;
    }
}