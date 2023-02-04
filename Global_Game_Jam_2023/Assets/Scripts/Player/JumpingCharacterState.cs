using System;

public class JumpingCharacterState : State
{
    private Player _Owner;

    public JumpingCharacterState(Player owner)
    {
        _Owner = owner;
    }
    public override void OnEnd()
    {
        _Owner.IsJumping = false;
    }

    public override void OnFixedUpdate()
    {
        _Owner.HorizontalMove();

        _Owner.VerticalMove();
    }

    public override void OnStart()
    {
        _Owner.Animator.SetTrigger("IsJumping");
        _Owner.Landed = false;

    }

    public override void OnUpdate()
    {
        if (_Owner.transform.position.y > _Owner.JumpDestination.y || _Owner.transform.position.y < _Owner.LastTransform.y)
        {
            _Owner.IsJumping = false;
            _Owner.IsFalling = true;
            _Owner.GroundCheck();
            _Owner.StateMachine.SetState(EPlayerState.Falling);
        }
        _Owner.LastTransform = _Owner.transform.position;
        _Owner.GroundCheck();
    }


}

