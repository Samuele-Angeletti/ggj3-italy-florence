using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderPasswordInteractable : Interactable
{
    [SerializeField] WindowsManager windowDestination;

    public override void Interact(Player player)
    {
        if (player.PasswordCount > 0)
        {
            player.PasswordCount--;
            player.Dematerialize(windowDestination.CheckPoint);
        }
        else
            Debug.Log("Non hai una password!");
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (windowDestination != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, windowDestination.transform.position);
        }
    }
#endif
}
