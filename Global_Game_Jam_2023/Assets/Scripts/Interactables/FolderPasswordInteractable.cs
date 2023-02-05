using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderPasswordInteractable : Interactable
{
    [SerializeField] WindowsManager windowDestination;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite normalFolder;

    bool islocked = true;

    Player Player;
    public override void Interact(Player player)
    {
        Player = player;

        if (player.PasswordCount > 0 || !islocked)
        {
            if(islocked)
                player.PasswordCount--;
            base.Interact(player);
            islocked = false;
            spriteRenderer.sprite = normalFolder;
            player.Dematerialize(windowDestination.CheckPoint);
        }
        else
        {
            Invoke(nameof(DelayMethod), 0.1f);

            GameManager.Instance.PlayPotectedFolder();
        }
    }


    public void DelayMethod()
    {
        Player.Interacting = false;
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
