
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

    //public UnitBaseState Grounded() { RootState = States.grounded; return _states[PlayerStateMachine.PlayerStates.grounded]; }
   // public UnitBaseState Idle() { SubState = States.idle; return _states[PlayerStateMachine.PlayerStates.idle]; }
   // public UnitBaseState Run() { SubState = States.run; return _states[PlayerStateMachine.PlayerStates.run]; }
}
