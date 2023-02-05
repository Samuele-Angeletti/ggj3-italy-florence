using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteractable : Interactable
{
    [SerializeField] EPowerUp typeToActive;
    Player Player;
    public override void Interact(Player player)
    {
        base.Interact(player);

        switch (typeToActive)
        {
            case EPowerUp.DragAndDrop:
                player.DragAndDropAvailable = true;
                break;
            case EPowerUp.ClickOnFolder:
                player.ClickAvailable = true;
                break;
            case EPowerUp.TypeKeyboard:
                player.KeyboardTypeAvailable = true;
                break;
        }
        Player = player;
        Invoke(nameof(DelayMethod), 0.1f);

        GameManager.Instance.PlayPickableTaken();
    }

    public void DelayMethod()
    {
        Player.Interacting = false;
        Destroy(gameObject);
    }
}
