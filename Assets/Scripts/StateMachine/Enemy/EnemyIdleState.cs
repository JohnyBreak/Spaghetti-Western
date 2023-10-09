using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine currentContext, EnemyStateFactory unitStateFactory) 
        : base(currentContext, unitStateFactory)
    {
    }
    public override void EnterState()
    {
        //Ctx.AppliedMovementX = 0;
        //Ctx.AppliedMovementZ = 0;
        //Ctx.PlayerAnimation.SetStance(PlayerLocomotion.Stance.Run);
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

    public override void CheckSwitchStates()
    {
        //if (Ctx.IsMovementPressed)
        //{
        //    SwitchState(_factory.GetState(EnemyStateMachine.EnemyStates.run));
        //}
    }
}
