using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : UnitStateMachine<EnemyStateMachine.EnemyStates>
{
    public enum EnemyStates
    {
        idle,
        run,
        grounded
    }
}
