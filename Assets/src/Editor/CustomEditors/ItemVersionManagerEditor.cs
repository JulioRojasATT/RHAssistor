using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemInfo))]
public class ItemVersionManagerEditor : Editor
{
    public override void OnInspectorGUI() {        
        base.OnInspectorGUI();
        ItemInfo colliderCreator = (ItemInfo)target;
        if (GUILayout.Button("Extract Materials"))
        {
            colliderCreator.ExtractModelMaterials();
        }
        if (GUILayout.Button("Calculate renderers & materials."))
        {
            colliderCreator.CalculateDefaultVersionAndMaterials();
        }
        if (GUILayout.Button("Generate new version"))
        {
            colliderCreator.GenerateNewVersion();
        }
        if (GUILayout.Button("Update Current Version"))
        {
            colliderCreator.CalculateMaterialsAndColorsOfVersion(colliderCreator.currentVersion);
        }
        if (GUILayout.Button("Swap to current version"))
        {
            colliderCreator.SwapToVersion(colliderCreator.currentVersion);
        }
    }
}