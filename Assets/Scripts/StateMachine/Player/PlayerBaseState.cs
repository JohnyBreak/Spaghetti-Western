public abstract class PlayerBaseState : UnitBaseState
{
    protected PlayerStateMachine Ctx => (PlayerStateMachine)GetContext();

    protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory unitStateFactory) : base(currentContext, unitStateFactory)
    {
    }
}
