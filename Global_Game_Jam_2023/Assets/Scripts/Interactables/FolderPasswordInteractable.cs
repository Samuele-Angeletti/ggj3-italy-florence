using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderPasswordInteractable : Interactable
{
    public override void Interact(Player player)
    {
        if (player.PasswordCount > 0)
            player.PasswordCount--;
        else
            Debug.Log("Non hai una password!");
    }
}
