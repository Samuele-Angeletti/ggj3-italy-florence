using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FolderClickInteractable : MonoBehaviour
{
    [SerializeField] WindowsManager windowPlatform;

    public Button Button;

    //TextMeshProUGUI textMeshProUGUI;
    //[HideInInspector] public string FolderName;

    //private void Start()
    //{
    //    textMeshProUGUI = gameObject.SearchComponent<TextMeshProUGUI>();
    //    if(textMeshProUGUI != null)
    //        FolderName = textMeshProUGUI.text;
    //}

    public void Interact()
    {
        //UIManager.Instance.AddText(FolderName);
        windowPlatform.gameObject.SetActive(true);
    }
}
