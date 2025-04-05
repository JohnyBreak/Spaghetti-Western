using System;

public abstract class UnitStateFactory
{
    public int RootState { get; internal set; }
    public int SubState { get; internal set; }

    public abstract UnitBaseState GetState(int state);

}
