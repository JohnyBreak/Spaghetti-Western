using System;

public abstract class UnitBaseState<Estate> where Estate : Enum
{
    protected bool _isRootState = false;
    private UnitStateMachine<Estate> _ctx;
    protected UnitStateFactory<Estate> _factory;
    protected UnitBaseState<Estate> _currentSuperState;
    protected UnitBaseState<Estate> _currentSubState;

    public UnitBaseState(UnitStateMachine<Estate> currentContext, UnitStateFactory<Estate> unitStateFactory) 
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

    protected void SwitchState(UnitBaseState<Estate> newState) 
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

    protected void SetSuperState(UnitBaseState<Estate> newSuperState) 
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(UnitBaseState<Estate> newSubState) 
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    public void ExitStates() 
    {
        ExitState();
        if (_currentSubState != null) _currentSubState.ExitStates();
    }

    protected UnitStateMachine<Estate> GetContext()
    {
        return _ctx;
    }
}
