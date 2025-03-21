using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(All_Item))]
public class All_ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        All_Item all_Item = (All_Item)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Items by Grade", EditorStyles.boldLabel);

        ShowGradeItemsList("SS Grade Items", all_Item.SS_Item);
        ShowGradeItemsList("S Grade Items", all_Item.S_Item);
        ShowGradeItemsList("A Grade Items", all_Item.A_Item);
        ShowGradeItemsList("B Grade Items", all_Item.B_Item);
        ShowGradeItemsList("C Grade Items", all_Item.C_Item);
        ShowGradeItemsList("D Grade Items", all_Item.D_Item);
    }

    private void ShowGradeItemsList(string title, List<EquipmentData> items)
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField(title, EditorStyles.boldLabel);

        foreach (EquipmentData item in items)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(item, typeof(EquipmentData), false);
            string assetPath = AssetDatabase.GetAssetPath(item);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
            EditorGUILayout.TextField("Name: " + fileName);
            EditorGUILayout.TextField("Grade: " + item.grade.ToString());
            EditorGUILayout.TextField("Index: " + item.Item_index.ToString());
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }
}
