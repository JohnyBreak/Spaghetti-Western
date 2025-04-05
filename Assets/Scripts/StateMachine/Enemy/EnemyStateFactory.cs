using System.Collections.Generic;

public class EnemyStateFactory : UnitStateFactory
{
    private EnemyStateMachine _context;
    private Dictionary<int, UnitBaseState> _states = new ();

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        _context = currentContext;
        //_states[EnemyStates.idle] = new EnemyIdleState(_context, this);
        //_states[EnemyStates.run] = new EnemyRunState(_context, this);
        //_states[EnemyStates.grounded] = new EnemyGroundedState(_context, this);

    }

    public override UnitBaseState GetState(int state)
    {
        if (_states[state] is IRootState) RootState = state;
        else SubState = state;

        return _states[state];
    }
}
