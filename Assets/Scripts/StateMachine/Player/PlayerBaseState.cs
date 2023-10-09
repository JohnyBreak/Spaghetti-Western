using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : UnitBaseState<PlayerStateMachine.PlayerStates>
{
    protected PlayerStateMachine Ctx => (PlayerStateMachine)GetContext();

    protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory unitStateFactory) : base(currentContext, unitStateFactory)
    {
    }
}
