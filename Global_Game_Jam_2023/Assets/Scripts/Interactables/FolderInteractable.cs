using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderInteractable : Interactable
{
    [SerializeField] WindowsManager windowDestination;


    public override void Interact(Player player)
    {
        base.Interact(player);
        player.Dematerialize(windowDestination.CheckPoint);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(windowDestination != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, windowDestination.transform.position);
        }
    }
#endif
}
