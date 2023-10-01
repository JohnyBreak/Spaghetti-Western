using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateFactory
{
    public enum States
    {
        idle,
        run,
        grounded,
    }

    public States RootState;
    public States SubState;

}
