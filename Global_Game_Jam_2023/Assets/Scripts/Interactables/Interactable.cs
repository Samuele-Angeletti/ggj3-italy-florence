using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent OnInteract;

    TextMeshProUGUI textMeshProUGUI;
    [HideInInspector] public string FolderName;

    private void Start()
    {
        textMeshProUGUI = gameObject.SearchComponent<TextMeshProUGUI>();
        if (textMeshProUGUI != null)
            FolderName = textMeshProUGUI.text;
    }

    public virtual void Interact(Player player)
    {
        if(!string.IsNullOrEmpty(FolderName))
            UIManager.Instance.AddText(FolderName);

        OnInteract.Invoke();
    }


}

public enum EPowerUp
{
    DragAndDrop,
    ClickOnFolder,
    TypeKeyboard
}
