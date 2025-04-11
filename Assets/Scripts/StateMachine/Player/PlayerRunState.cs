using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState//UnitBaseState<PlayerStateMachine.PlayerStates>
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    { }


    public override void EnterState()
    {
        Ctx.PlayerAnimation.SetStance(PlayerAnimation.Stance.Run);
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        HandleRotatation();
        HandleMove();

        CheckSwitchStates();


    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(_factory.GetState(PlayerStates.Idle));
        }
        //if (!_ctx.IsMovementPressed)
        //{
        //    SwitchState(_factory.Idle());
        //}
        //if (_ctx.IsDashPressed && _ctx.CanRepeatDash
        //    && _ctx.CanRepeatDash2 && _ctx.DashReloaded)
        //{
        //    SwitchState(_factory.Dash());
        //}
    }

    private void HandleRotatation()
    {
        //if (_ctx.CurrentMovement.x == 0 && _ctx.CurrentMovement.z == 0) return;
        //float turnSmoothVelocity = 0;
        //float targetAngle = Mathf.Atan2(_ctx.CurrentMovement.x, _ctx.CurrentMovement.z) * Mathf.Rad2Deg + _ctx.CameraTransform.eulerAngles.y;

        //float angle = Mathf.SmoothDampAngle(_ctx.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _ctx.TurnSmoothTime);

        //_ctx.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void HandleMove()
    {/*
        Vector3 cameraForward = _ctx.CameraTransform.forward;
        Vector3 cameraRight = _ctx.CameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 forwardCameraRelativeMovement = cameraForward * _ctx.CurrentMovement.z;
        Vector3 rightCameraRelativeMovement = cameraRight * _ctx.CurrentMovement.x;
        Vector3 cameraRelativeMovement = forwardCameraRelativeMovement + rightCameraRelativeMovement;
        */

        Ctx.AppliedMovementZ = Ctx.CameraRelativeMovement.z * Ctx.MovementSpeed;
        Ctx.AppliedMovementX = Ctx.CameraRelativeMovement.x * Ctx.MovementSpeed;
    }
}
