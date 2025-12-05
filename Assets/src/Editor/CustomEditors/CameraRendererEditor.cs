using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraRenderer))]
public class CameraRendererEditor : Editor
{
    public override void OnInspectorGUI() {        
        base.OnInspectorGUI();
        CameraRenderer cameraRenderer= (CameraRenderer)target;
        if (GUILayout.Button("Take Capture"))
        {
            cameraRenderer.Capture();
        }
        if (GUILayout.Button("Take Hi Res Capture"))
        {
            cameraRenderer.TakeHiResShot();
        }
    }
}