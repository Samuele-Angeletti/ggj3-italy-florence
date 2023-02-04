using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowsManagerHandler : MonoBehaviour
{
    public List<WindowsManager> windowsManagerList;
    public WindowsManager CurrentActive;

    public void Open(WindowsManager windowsManager)
    {
        CurrentActive = windowsManager;
        foreach (var activeWindows in windowsManagerList.Where(x => x.gameObject.activeSelf && x != CurrentActive))
        {
            activeWindows.EnableColliders(false);
        }
        CurrentActive.gameObject.SetActive(true);
    }
}
