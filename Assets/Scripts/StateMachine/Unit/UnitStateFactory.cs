using System;

public abstract class UnitStateFactory<Estate> where Estate : Enum
{
    public Estate RootState { get; internal set; }
    public Estate SubState { get; internal set; }

    public abstract UnitBaseState<Estate> GetState(Estate state);

}
