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
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    private void HandleRotatation()
    {
    }

    public override void CheckSwitchStates()
    {
    }
}
