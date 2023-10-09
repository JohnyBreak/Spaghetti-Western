using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState//UnitBaseState<PlayerStateMachine.PlayerStates>, IRootState
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
        if (!Ctx.IsMovementPressed)
        {
            SetSubState(_factory.GetState(PlayerStateMachine.PlayerStates.idle));
        }
        else
        {
            SetSubState(_factory.GetState(PlayerStateMachine.PlayerStates.run));
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
        Ctx.CurrentMovementY = Ctx.GroundedGravity;
        Ctx.AppliedMovementY = Ctx.GroundedGravity;
    }
}
