using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordInteractable : Interactable
{
    Player Player;
    public override void Interact(Player player)
    {
        base.Interact(player);
        player.PasswordCount++;
        Player = player;
        Invoke(nameof(DelayMethod), 0.1f);
    }

    public void DelayMethod()
    {
        Player.Interacting = false;
        Destroy(gameObject);
    }
}
