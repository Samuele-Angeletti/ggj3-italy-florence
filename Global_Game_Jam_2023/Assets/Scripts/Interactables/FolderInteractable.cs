using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderInteractable : Interactable
{

    public override void Interact(Player player)
    {
        base.Interact(player);
        player.Dematerialize();
    }
}
