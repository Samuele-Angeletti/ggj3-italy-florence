using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteractable : Interactable
{
    [SerializeField] EPowerUp typeToActive;

    public override void Interact(Player player)
    {
        base.Interact(player);

        switch (typeToActive)
        {
            case EPowerUp.DragAndDrop:
                GameManager.Instance.EnableMouseDragAndDrop(true);
                break;
            case EPowerUp.ClickOnFolder:
                GameManager.Instance.EnableMouseClickOnFolder(true);
                break;
            case EPowerUp.TypeKeyboard:
                GameManager.Instance.EnablePlayerKeyboard(true);
                break;
        }

        Destroy(gameObject);
    }
}
