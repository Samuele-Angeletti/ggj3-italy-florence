using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderPasswordInteractable : Interactable
{
    [SerializeField] WindowsManager windowDestination;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite normalFolder;

    bool islocked = true;

    public override void Interact(Player player)
    {
        base.Interact(player);
        if (player.PasswordCount > 0 || !islocked)
        {
            if(islocked)
                player.PasswordCount--;
            islocked = false;
            spriteRenderer.sprite = normalFolder;
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
