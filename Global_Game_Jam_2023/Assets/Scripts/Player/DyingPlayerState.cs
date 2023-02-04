using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingPlayerState : State
{

    private Player _Owner;

    public DyingPlayerState(Player owner)
    {
        _Owner = owner;
    }

    public override void OnEnd()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStart()
    {
        _Owner.Animator.SetTrigger("IsDying");

    }

    public override void OnUpdate()
    {
        if (!_Owner.Dying)
        {
            _Owner.StateMachine.SetState(EPlayerState.Idle);
        }

    }


}
