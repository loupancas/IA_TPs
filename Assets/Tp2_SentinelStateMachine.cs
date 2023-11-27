using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp2_SentinelStateMachine : MonoBehaviour
{
    [Header("References")]
    public TP2_Manager _Manager;

    [Header("SentinelRef")]
    public Tp2_Sentinel _Sentinel;

    [Header("StateMachine")]
    public State CurrentState;
    [SerializeField] Tp2Sentinel_StatePatrol _StatePatrol;
    [SerializeField] Tp2_SentinelState_Pursue _StatePursue;


    [Header("Variable logic")]
    public bool Enemyspotted;
    public Node_Script _SentinelNearestNode;
    public Node_Script _PlayernearestNode;
    public bool Alarm;


    private void Start()
    {
        _Manager = FindObjectOfType<TP2_Manager>();
        SwitchToNewState(_StatePatrol);
    }

    public void update()
    {
        Enemyspotted = _Sentinel.EnemySpotted;
        _PlayernearestNode = _Manager._NearestPlayerNode;
    }

    public void RunStateMachine()
    {
        State NextState = CurrentState?.RunCurrentState();
        if (NextState != null)
        {
            SwitchToNewState(NextState);
        }
    }

    public void SwitchToNewState(State newState)
    {
        //funcion para cambiar el State ejecutado :)
        CurrentState = newState;
    }

}
