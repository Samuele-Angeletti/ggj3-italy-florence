using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(WindowsManager))]
public class WindowManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WindowsManager windowsManager = (WindowsManager)target;
        if(GUILayout.Button("Prendi tutti i collider dentro"))
        {
            Collider2D collider = windowsManager.GetComponent<Collider2D>();
            windowsManager.InsideColliderList = FindObjectsOfType<Collider2D>()
                .Where(x => collider.bounds.Contains(x.transform.position) && x.GetComponent<Player>() == null && x != collider).ToList();
            EditorUtility.SetDirty(windowsManager);
        }
    }
}
