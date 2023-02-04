using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InteractingCharacterState : State
{
    private Player _Owner;

    public InteractingCharacterState(Player owner)
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
        _Owner.Animator.SetTrigger("IsInteracting");
        
        _Owner.Interacting = _Owner.Interact();
    }

    public override void OnUpdate()
    {
        if(!_Owner.Interacting)
        {
            _Owner.StateMachine.SetState(EPlayerState.Idle);
        }
    }
}

