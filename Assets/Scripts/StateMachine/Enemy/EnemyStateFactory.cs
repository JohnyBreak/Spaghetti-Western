using System.Collections.Generic;

public class EnemyStateFactory : UnitStateFactory<EnemyStateMachine.EnemyStates>
{
    private EnemyStateMachine _context;
    private Dictionary<EnemyStateMachine.EnemyStates, UnitBaseState<EnemyStateMachine.EnemyStates>> _states = new Dictionary<EnemyStateMachine.EnemyStates, UnitBaseState<EnemyStateMachine.EnemyStates>>();

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        _context = currentContext;
        //_states[EnemyStateMachine.EnemyStates.idle] = new EnemyIdleState(_context, this);
        //_states[EnemyStateMachine.EnemyStates.run] = new EnemyRunState(_context, this);
        //_states[EnemyStateMachine.EnemyStates.grounded] = new EnemyGroundedState(_context, this);

    }

    public override UnitBaseState<EnemyStateMachine.EnemyStates> GetState(EnemyStateMachine.EnemyStates state)
    {
        if (_states[state] is IRootState) RootState = state;
        else SubState = state;

        return _states[state];
    }
}
