using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "All_Item", menuName = "All_Item")]
public class All_Item : ScriptableObject
{
    public List<EquipmentData> SS_Item = new List<EquipmentData>();
    public List<EquipmentData> S_Item = new List<EquipmentData>();
    public List<EquipmentData> A_Item = new List<EquipmentData>();
    public List<EquipmentData> B_Item = new List<EquipmentData>();
    public List<EquipmentData> C_Item = new List<EquipmentData>();
    public List<EquipmentData> D_Item = new List<EquipmentData>();
}
