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
                .Where(x => collider.bounds.Contains(x.transform.position)
                && x.GetComponent<Player>() == null
                && x != collider
                && x.GetComponent<StartBar>() == null).ToList();
            EditorUtility.SetDirty(windowsManager);
        }

        if(GUILayout.Button("Rendi i collider Figli di questa Window"))
        {

            foreach (var collider in windowsManager.InsideColliderList)
            {
                if(collider.transform.parent != null)
                {
                    collider.transform.parent.transform.SetParent(windowsManager.transform);
                }
                else
                {
                    collider.transform.SetParent(windowsManager.transform);
                }
            }

            foreach(var collider in windowsManager.InsideColliderList)
            {
                var renderer = collider.gameObject.SearchComponent<SpriteRenderer>();
                if(renderer != null)
                {
                    renderer.sortingLayerName = windowsManager.SpriteRenderer.sortingLayerName;
                    renderer.sortingOrder = windowsManager.SpriteRenderer.sortingOrder + 1;
                }

                var canvas = collider.gameObject.SearchComponent<Canvas>();
                if(canvas != null)
                {
                    canvas.sortingLayerName = windowsManager.SpriteRenderer.sortingLayerName;
                    canvas.sortingOrder = windowsManager.SpriteRenderer.sortingOrder + 1;
                }
            }

            EditorUtility.SetDirty(windowsManager);
        }
    }
}
