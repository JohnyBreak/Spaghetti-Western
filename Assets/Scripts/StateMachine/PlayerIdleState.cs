using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : UnitBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    { }
    

    public override void EnterState()
    {
        ((PlayerStateMachine)_ctx).AppliedMovementX = 0;
        ((PlayerStateMachine)_ctx).AppliedMovementZ = 0;
        ((PlayerStateMachine)_ctx).PlayerAnimation.SetStance(PlayerLocomotion.Stance.Run);
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        //if(_ctx.IsAiming) HandleRotatation();
        CheckSwitchStates();
    }

    private void HandleRotatation()
    {
        float turnSmoothVelocity = 0;
        float targetAngle = Mathf.Atan2(((PlayerStateMachine)_ctx).CurrentMovement.x, ((PlayerStateMachine)_ctx).CurrentMovement.z) * Mathf.Rad2Deg + ((PlayerStateMachine)_ctx).CameraTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(_ctx.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, ((PlayerStateMachine)_ctx).TurnSmoothTime);

        _ctx.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public override void CheckSwitchStates()
    {
        if (((PlayerStateMachine)_ctx).IsMovementPressed)
        {
            SwitchState(((PlayerStateFactory)_factory).Run());
        }
    }
}
