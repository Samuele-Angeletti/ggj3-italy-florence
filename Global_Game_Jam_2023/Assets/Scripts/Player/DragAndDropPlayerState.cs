using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropPlayerState : State
{
    private Player _Owner;
    Camera mainCam;
    public DragAndDropPlayerState(Player owner)
    {
        _Owner = owner;
    }

    public override void OnEnd()
    {

    }

    public override void OnFixedUpdate()
    {
        _Owner.Rigidbody.MovePosition(mainCam.ScreenToWorldPoint(Input.mousePosition));
    }

    public override void OnStart()
    {
        mainCam = Camera.main;
    }

    public override void OnUpdate()
    {

    }
}
