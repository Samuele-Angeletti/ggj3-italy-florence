using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderLinkInteractable : Interactable
{
    [SerializeField] FolderLinkInteractable link;

    Player Player;
    public override void Interact(Player player)
    {
        Player = player;
        if (link != null)
        {
            base.Interact(player);

            player.transform.position = link.transform.position;
        }
        Invoke(nameof(DelayMethod), 0.1f);
    }


    public void DelayMethod()
    {
        Player.Interacting = false;
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
