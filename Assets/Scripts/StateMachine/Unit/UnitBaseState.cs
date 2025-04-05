using System;

public abstract class UnitBaseState
{
    protected bool _isRootState = false;
    private UnitStateMachine _ctx;
    protected UnitStateFactory _factory;
    protected UnitBaseState _currentSuperState;
    protected UnitBaseState  _currentSubState;

    public UnitBaseState(UnitStateMachine currentContext, UnitStateFactory unitStateFactory) 
    {
        _ctx = currentContext;
        _factory = unitStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();

    public void UpdateStates() 
    {
        UpdateState();
        if (_currentSubState != null) _currentSubState.UpdateStates();
    }

    protected void SwitchState(UnitBaseState newState) 
    {
        ExitState();

        newState.EnterState();

        if (_isRootState)
        {
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(UnitBaseState newSuperState) 
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(UnitBaseState newSubState) 
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    public void ExitStates() 
    {
        ExitState();
        if (_currentSubState != null) _currentSubState.ExitStates();
    }

    protected UnitStateMachine GetContext()
    {
        return _ctx;
    }
}
