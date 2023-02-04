using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    public Checkpoint CheckPoint;

    public List<Collider2D> InsideColliderList;
    public List<Collider2D> Walls;
    private void Start()
    {
        foreach (var collider in InsideColliderList)
        {
            var clickFolder = collider.gameObject.SearchComponent<FolderClickInteractable>();
            if(clickFolder != null)
            {
                clickFolder.Button.interactable = false;
            }
        }
    }

    public void EnableColliders(bool enabled)
    {
        Walls.ForEach(x => x.enabled = enabled);
        InsideColliderList.ForEach(x => x.enabled = enabled);
    }

    private void OnEnable()
    {
        EnableColliders(true);
    }
}
