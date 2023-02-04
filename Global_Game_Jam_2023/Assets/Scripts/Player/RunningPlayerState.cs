using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningPlayerState : State
{
    private Player _Owner;

    public RunningPlayerState(Player owner)
    {
        _Owner = owner;
    }

    public override void OnEnd()
    {

    }

    public override void OnFixedUpdate()
    {
        _Owner.HorizontalMove();

        _Owner.VerticalMove();
    }

    public override void OnStart()
    {
        _Owner.Animator.SetTrigger("IsRunning");

    }

    public override void OnUpdate()
    {
        if(!_Owner.IsRunning)
        {
            if (_Owner.Direction.x == 0 && _Owner.Direction.y == 0)
            {
                _Owner.StateMachine.SetState(EPlayerState.Idle);
                return;
            }

            if (_Owner.Direction.x > 0 || _Owner.Direction.x < 0)
            {
                _Owner.StateMachine.SetState(EPlayerState.Walking);
                return;
            }
        }

        if (_Owner.Direction.y > 0)
        {
            _Owner.StateMachine.SetState(EPlayerState.Jumping);
            return;
        }

        if (_Owner.Direction.y < 0)
        {
            _Owner.StateMachine.SetState(EPlayerState.Falling);
            return;
        }

        if (!_Owner.GroundCheck())
        {
            _Owner.StateMachine.SetState(EPlayerState.Falling);
            return;
        }
    }
}
