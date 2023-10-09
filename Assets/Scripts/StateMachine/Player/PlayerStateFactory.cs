
using System.Collections.Generic;

public class PlayerStateFactory : UnitStateFactory<PlayerStateMachine.PlayerStates>
{
    private PlayerStateMachine _context;
    private Dictionary<PlayerStateMachine.PlayerStates, UnitBaseState<PlayerStateMachine.PlayerStates>> _states = new Dictionary<PlayerStateMachine.PlayerStates, UnitBaseState<PlayerStateMachine.PlayerStates>>();

    public PlayerStateFactory(PlayerStateMachine currentContext) 
    {
        _context = currentContext;
        _states[PlayerStateMachine.PlayerStates.idle] = new PlayerIdleState(_context, this);
        _states[PlayerStateMachine.PlayerStates.run] = new PlayerRunState(_context, this);
        _states[PlayerStateMachine.PlayerStates.grounded] = new PlayerGroundedState(_context, this);

    }

    public override UnitBaseState<PlayerStateMachine.PlayerStates> GetState(PlayerStateMachine.PlayerStates state)
    {
        if (_states[state] is IRootState) RootState = state;
        else SubState = state;

        return _states[state];
    }
}

