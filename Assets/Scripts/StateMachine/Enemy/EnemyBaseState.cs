using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : UnitBaseState
{
    protected EnemyStateMachine Ctx => (EnemyStateMachine)GetContext();

    protected EnemyBaseState(EnemyStateMachine currentContext, EnemyStateFactory unitStateFactory) : base(currentContext, unitStateFactory)
    {
    }
}
