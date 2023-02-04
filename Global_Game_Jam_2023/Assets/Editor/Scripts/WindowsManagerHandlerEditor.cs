using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WindowsManagerHandler))]
public class WindowsManagerHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WindowsManagerHandler handler = (WindowsManagerHandler)target;

        if(GUILayout.Button("Set SortingLayers"))
        {
            int counter = 1;
            for (int i = 0; i < handler.windowsManagerList.Count; i++)
            {
                handler.windowsManagerList[i].SpriteRenderer.sortingOrder = i + counter;
                counter++;
            }

            EditorUtility.SetDirty(handler);
        }
    }
}
