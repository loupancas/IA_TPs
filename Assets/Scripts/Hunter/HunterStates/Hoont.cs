using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoont : State
{
    GameObject target;


    public override State RunCurrentState()
    {
        return this;
    }

    
    public void SwitchTarget(GameObject NewTarget)
    {
        target = NewTarget;
    }

}
