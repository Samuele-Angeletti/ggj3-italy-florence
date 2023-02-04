using System;


public class LandedCharacterState : State
{
    private Player _Owner;

    public LandedCharacterState(Player owner)
    {
        _Owner = owner;
    }
    public override void OnEnd()
    {
        _Owner.Landed = false;
    }

    public override void OnFixedUpdate()
    {
        _Owner.HorizontalMove();

        _Owner.VerticalMove();
    }

    public override void OnStart()
    {
        _Owner.Animator.SetTrigger("IsLanding");

    }

    public override void OnUpdate()
    {
        if(_Owner.Landed)
        {
            if (_Owner.IsRunning)
            {
                _Owner.StateMachine.SetState(EPlayerState.Running);
                return;
            }

            if (_Owner.Direction.x > 0 || _Owner.Direction.x < 0)
            {
                _Owner.StateMachine.SetState(EPlayerState.Walking);
                return;
            }

            if (_Owner.Direction.y > 0)
            {
                _Owner.StateMachine.SetState(EPlayerState.Jumping);
                return;
            }

            if(_Owner.GroundCheck())
            {
                _Owner.StateMachine.SetState(EPlayerState.Idle);
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

   
}

