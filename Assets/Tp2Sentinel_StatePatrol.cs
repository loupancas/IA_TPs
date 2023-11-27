using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp2Sentinel_StatePatrol : State
{
    [Header("References")]
    [SerializeField] TP2_Manager _Manager;
    [SerializeField] Tp2_SentinelStateMachine _Tp2StateMachine; // ref. al statemachine
    [SerializeField] GameObject _Tp2SentinelOBJ; // ref. gameobject del sentinel

    [Header("State References")]
    [SerializeField] Tp2_SentinelState_Pursue _SentinelPursue;
    
    
    [Header("Variables")]
    [SerializeField] List<Transform> _PatrolWaypoints = new List<Transform>();
    [SerializeField] float Speed, ArriveDist; // Velocidad del Sentinel y la distancia maxima para que corra arrive

    private void Start()
    {
        _Tp2StateMachine = GetComponentInParent<Tp2_SentinelStateMachine>();
        _Manager = _Tp2StateMachine._Manager;
        
    }

     
    public override State RunCurrentState()
    {
        if(_Tp2StateMachine.Enemyspotted == true)
        {
            return _SentinelPursue;
        }
        else
        {
            PatrolLogic();
            return this;
        }
    }


    int CurrentWaypoint;

    private void PatrolLogic()
    {
        print("Patrolling...");
        //reset del listado de waypoints
        if(CurrentWaypoint >= _PatrolWaypoints.Count)
        {
            CurrentWaypoint = 0;
        }

        //calculo distancia
        float distance = Vector2.Distance(_PatrolWaypoints[CurrentWaypoint].transform.position, _Tp2SentinelOBJ.transform.position);

        Vector3 Director = (_PatrolWaypoints[CurrentWaypoint].transform.position - _Tp2SentinelOBJ.transform.position) * Speed;

        float DirectorAngle = MathF.Atan2(Director.y, Director.x) * Mathf.Rad2Deg;

        _Tp2SentinelOBJ.transform.position += Vector3.ClampMagnitude(Director, Speed);

        _Tp2SentinelOBJ.transform.rotation = Quaternion.Euler(Vector3.forward * DirectorAngle);

        if(distance < ArriveDist)
        {
            CurrentWaypoint++;
        }
    }

}
