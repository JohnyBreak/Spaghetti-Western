
using System.Collections.Generic;

public class PlayerStateFactory : UnitStateFactory
{
    private PlayerStateMachine _context;
    private Dictionary<int, UnitBaseState> _states = new ();

    public PlayerStateFactory(PlayerStateMachine currentContext) 
    {
        _context = currentContext;
        _states[PlayerStates.Idle] = new PlayerIdleState(_context, this);
        _states[PlayerStates.Run] = new PlayerRunState(_context, this);
        _states[PlayerStates.Grounded] = new PlayerGroundedState(_context, this);

    }

    public override UnitBaseState GetState(int state)
    {
        if (_states[state] is IRootState) RootState = state;
        else SubState = state;

        return _states[state];
    }
}

