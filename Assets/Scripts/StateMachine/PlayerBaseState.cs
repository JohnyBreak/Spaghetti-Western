
public abstract class PlayerBaseState : UnitBaseState
{

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory unitStateFactory) : base(currentContext, unitStateFactory)
    {
        _ctx = currentContext;
        _factory = unitStateFactory;
    }

    public override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
