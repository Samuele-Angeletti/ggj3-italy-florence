using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderClickInteractable : MonoBehaviour
{
    [SerializeField] WindowsManager windowPlatform;
    public Button Button;

    public void Interact()
    {
        windowPlatform.gameObject.SetActive(true);
    }
}
