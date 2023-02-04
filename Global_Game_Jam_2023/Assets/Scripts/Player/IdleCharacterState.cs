using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// stato idle
public class IdleCharacterState : State
{
    private Player _Owner;

    public IdleCharacterState(Player owner)
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
        _Owner.Animator.SetTrigger("IsIdle");
        _Owner.Stop();
    }

    public override void OnUpdate()
    {

        if (_Owner.Direction.x > 0 || _Owner.Direction.x < 0)
        {
            if (_Owner.IsRunning)
            {
                _Owner.StateMachine.SetState(EPlayerState.Running);
                return;
            }

            _Owner.StateMachine.SetState(EPlayerState.Walking);
            return;
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

