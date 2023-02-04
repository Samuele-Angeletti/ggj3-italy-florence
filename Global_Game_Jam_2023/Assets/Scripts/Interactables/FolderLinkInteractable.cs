using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderLinkInteractable : Interactable
{
    [SerializeField] FolderLinkInteractable link;

    public override void Interact(Player player)
    {
        base.Interact(player);
        if(link != null)
            player.transform.position = link.transform.position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(link != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, link.transform.position);
        }
    }
#endif
}
