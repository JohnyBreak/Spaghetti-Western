using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    { }
    

    public override void EnterState()
    {
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
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
        //if(_ctx.IsAiming) HandleRotatation();
        CheckSwitchStates();
    }

    private void HandleRotatation()
    {
        float turnSmoothVelocity = 0;
        float targetAngle = Mathf.Atan2(Ctx.CurrentMovement.x, Ctx.CurrentMovement.z) * Mathf.Rad2Deg + Ctx.CameraTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(Ctx.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, Ctx.TurnSmoothTime);

        Ctx.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsMovementPressed)
        {
            SwitchState(_factory.GetState(PlayerStates.Run));
        }
    }
}
