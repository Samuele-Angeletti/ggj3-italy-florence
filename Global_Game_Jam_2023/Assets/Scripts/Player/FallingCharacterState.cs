using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


// stato di caduta. Viene chiamata quando la terra sotto i piedi manca o dopo il salto. Anche questa dipende dai designer
public class FallingCharacterState : State
{
    private Player _Owner;

    public FallingCharacterState(Player owner)
    {
        _Owner = owner;
    }
    public override void OnEnd()
    {
        _Owner.IsFalling = false;
    }

    public override void OnFixedUpdate()
    {

        _Owner.HorizontalMove();

        _Owner.VerticalMove();
    }

    public override void OnStart()
    {
        _Owner.Animator.SetTrigger("IsFalling");
        _Owner.MoveVertical(new Vector2(_Owner.Direction.x, -1));
    }

    public override void OnUpdate()
    {
        if (_Owner.GroundCheck())
        {
            _Owner.StateMachine.SetState(EPlayerState.Landing);
            return;
        }
    }

}

