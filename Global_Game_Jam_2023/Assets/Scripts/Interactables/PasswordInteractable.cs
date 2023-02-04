using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordInteractable : Interactable
{
    public override void Interact(Player player)
    {
        base.Interact(player);
        player.PasswordCount++;
        Destroy(gameObject);
    }
}
