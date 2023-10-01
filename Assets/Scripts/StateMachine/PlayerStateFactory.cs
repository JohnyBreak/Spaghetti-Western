
using System.Collections.Generic;

public class PlayerStateFactory : UnitStateFactory
{
    //public enum States 
    //{
    //    idle,
    //    run,
    //    grounded,
    //    //jump,
    //    //fall,
    //    //dash,
    //    //glide, 
    //    //aim,
    //    //aimRun,
    //    //aimIdle,
    //    //groundAttack,
    //    //attackIdle,
    //    //damagedGround,
    //    //death,
    //}

    private PlayerStateMachine _context;
    private Dictionary<States, UnitBaseState> _states = new Dictionary<States, UnitBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext) 
    {
        _context = currentContext;
        _states[States.idle] = new PlayerIdleState(_context, this);
        _states[States.run] = new PlayerRunState(_context, this);
        _states[States.grounded] = new PlayerGroundedState(_context, this);
        //_states[PlayerStates.jump] = new PlayerJumpState(_context, this);
        //_states[PlayerStates.fall] = new PlayerFallState(_context, this);
        //_states[PlayerStates.dash] = new PlayerDashState(_context, this);
        //_states[PlayerStates.glide] = new PlayerGlideState(_context, this);
        //_states[PlayerStates.aim] = new PlayerAimState(_context, this);
        //_states[PlayerStates.aimRun] = new PlayerAimRunState(_context, this);
        //_states[PlayerStates.aimIdle] = new PlayerAimIdleState(_context, this);
        //_states[PlayerStates.groundAttack] = new PlayerGroundAttackState(_context, this);
        //_states[PlayerStates.attackIdle] = new PlayerAttackIdleState(_context, this);
        //_states[PlayerStates.damagedGround] = new PlayerDamageGroundState(_context, this);
        //_states[PlayerStates.death] = new PlayerDeathState(_context, this);
    }

    public UnitBaseState Grounded() { RootState = States.grounded; return _states[States.grounded]; }
    public UnitBaseState Idle() { SubState = States.idle; return _states[States.idle]; }
    public UnitBaseState Run() { SubState = States.run; return _states[States.run]; }
    //public PlayerBaseState AimIdle() { SubState = PlayerStates.aimIdle; return _states[PlayerStates.aimIdle]; }
    //public PlayerBaseState AimRun() { SubState = PlayerStates.aimRun; return _states[PlayerStates.aimRun]; }
    //public PlayerBaseState Jump() { RootState = PlayerStates.jump; return _states[PlayerStates.jump]; }
    //public PlayerBaseState Fall() { RootState = PlayerStates.fall; return _states[PlayerStates.fall]; }
    //public PlayerBaseState Dash() { RootState = PlayerStates.dash; return _states[PlayerStates.dash]; }
    //public PlayerBaseState Glide() { RootState = PlayerStates.glide; return _states[PlayerStates.glide]; }
    //public PlayerBaseState Aim() { RootState = PlayerStates.aim; return _states[PlayerStates.aim]; }
    //public PlayerBaseState GroundAttack() { RootState = PlayerStates.groundAttack; return _states[PlayerStates.groundAttack]; }

    //public PlayerBaseState AttackIdle() { SubState = PlayerStates.attackIdle; return _states[PlayerStates.attackIdle]; }
    //public PlayerBaseState DamageGround() { RootState = PlayerStates.damagedGround; return _states[PlayerStates.damagedGround]; }
    //public PlayerBaseState Death() { RootState = PlayerStates.death; return _states[PlayerStates.death]; }
}
