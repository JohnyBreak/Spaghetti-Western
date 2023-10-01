using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : UnitBaseState, IRootState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory)
    {
        _isRootState = true;
    }
    

    public override void EnterState()
    {

        
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        if (!((PlayerStateMachine)_ctx).IsMovementPressed)
        {
            SetSubState(((PlayerStateFactory)_factory).Idle());
        }
        else
        {
            SetSubState(((PlayerStateFactory)_factory).Run());
        }
    }

    public override void CheckSwitchStates()
    {
        //if (_ctx.IsAttackPressed && _ctx.CanRepeatAttack) 
        //{
        //    SwitchState(_factory.GroundAttack());
        //}

        ////if (_ctx.IsAiming) 
        ////{
        ////    SwitchState(_factory.Aim());
        ////}

        //if (_ctx.IsJumpPressed && !_ctx.IsDashing)
        //{
        //    _ctx.PlayerAnimation.SetGrounded(false);
        //    SwitchState(_factory.Jump());
        //}

        //if (!_ctx.IsGrounded)
        //{
        //    _ctx.PlayerAnimation.SetGrounded(false);
        //    _ctx.PlayerAnimation.SetGroundDifference(_ctx.GroundDiff);
        //    SwitchState(_factory.Fall());
        //}
    }

    public void HandleGravity()
    {
        ((PlayerStateMachine)_ctx).CurrentMovementY = ((PlayerStateMachine)_ctx).GroundedGravity;
        ((PlayerStateMachine)_ctx).AppliedMovementY = ((PlayerStateMachine)_ctx).GroundedGravity;
    }
}
