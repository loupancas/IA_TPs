using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoont : State
{
    GameObject target;

    [SerializeField] HunterStateMachine stateMachine;
    [SerializeField] Patrol PatrolState;
    [SerializeField] Rest RestState;


    public override State RunCurrentState()
    {
        if(stateMachine.HoontTime == false)
        {
            stateMachine.SwitchtoNewState(PatrolState);
            return PatrolState;
        }


        return this;
    }

    
    public void SwitchTarget(GameObject NewTarget)
    {
        target = NewTarget;
    }

}
