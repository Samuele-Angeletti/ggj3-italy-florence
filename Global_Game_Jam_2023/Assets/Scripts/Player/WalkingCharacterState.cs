
using UnityEngine;
// stato di walk. prende la direzione dall'owner (cioè dall'input)
public class WalkingCharacterState : State
{
    private Player _Owner;

    public WalkingCharacterState(Player owner)
    {
        _Owner = owner;
    }

    public override void OnStart()
    {
        _Owner.Animator.SetTrigger("IsWalking");

    }

    public override void OnUpdate()
    {
        if (_Owner.IsRunning)
        {
            _Owner.StateMachine.SetState(EPlayerState.Running);
            return;
        }

        if (_Owner.Direction.x == 0 && _Owner.Direction.y == 0)
        {
            _Owner.StateMachine.SetState(EPlayerState.Idle);
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

    public override void OnFixedUpdate()
    {
        _Owner.HorizontalMove();

        _Owner.VerticalMove();
    }

    public override void OnEnd()
    {

    }

}
