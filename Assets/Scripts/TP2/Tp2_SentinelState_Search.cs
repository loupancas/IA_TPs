using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tp2_SentinelState_Search : State
{
    [SerializeField] Node_Script StartNode, EndNode;

    [Header("References")]
    [SerializeField] TP2_Manager _Manager;
    [SerializeField] Tp2_SentinelStateMachine _Tp2StateMachine; // ref. al statemachine
    [SerializeField] GameObject _Tp2SentinelOBJ; // ref. gameobject del sentinel
    

    [Header("State References")]
    [SerializeField] Tp2Sentinel_StatePatrol _SentinelPatrol;
    [SerializeField] Tp2_SentinelState_Pursue _SentinelPursue;


    [Header("Variables")]
    [SerializeField] float speed,ArriveDist;
    [SerializeField] GameObject _Player;
    public Node_Script _PlayerNode;
    public Node_Script _SentinelNode;
    [SerializeField] List <Transform> _SearchPath = new List<Transform>();
    public bool ReachedDest = false;

    //Pathfinding _pf = new Pathfinding(); del codigo A estrella del profe


    private void Start()
    {
        _Tp2StateMachine = GetComponentInParent<Tp2_SentinelStateMachine>();
        _Manager = _Tp2StateMachine._Manager;

    }

    private void LateUpdate()
    {
        StartNode = _Tp2StateMachine._SentinelNearestNode;
        EndNode = _Tp2StateMachine._PlayernearestNode;
    }

    public override State RunCurrentState()
    {
         if (CurrentWaypoints < _SearchPath.Count)
         {
                ReachedDest = false;
         }

        if (_Tp2StateMachine.Enemyspotted == true)
        {
            _Tp2StateMachine.SwitchToNewState(_SentinelPursue);
            _Tp2StateMachine.Alarm = false;
            Reset();
            return _SentinelPursue;
        }
        else if(ReachedDest == true)
        {
            _Tp2StateMachine.SwitchToNewState(_SentinelPatrol);
            _Tp2StateMachine.Alarm = false;
            _Tp2SentinelOBJ.GetComponent<Tp2_Sentinel>()._Alarmed= false;
            _SentinelPatrol.ReturnPatrol(_SearchPath);
            Reset();
            return _SentinelPatrol;
        }
        else
        {
            AlarmLogic();
            return this;
        }
    }

    // <<<<<<< HEAD

    int CurrentWaypoints;
  private void AlarmLogic()
  {
        if(_SearchPath.Count <= 0)
        {
            _Manager.PathFinding(_SearchPath, _SentinelNode, _PlayerNode);
            CurrentWaypoints = 0;
        }

        if (CurrentWaypoints >= _SearchPath.Count)
        {
            CurrentWaypoints = _SearchPath.Count;
            ReachedDest = true;

        }
        else
        {
            //calculo distancia
            float distance = Vector2.Distance(_SearchPath[CurrentWaypoints].transform.position, _Tp2SentinelOBJ.transform.position);

            Vector3 Director = (_SearchPath[CurrentWaypoints].transform.position - _Tp2SentinelOBJ.transform.position) * speed;

            float DirectorAngle = MathF.Atan2(Director.y, Director.x) * Mathf.Rad2Deg;

            _Tp2SentinelOBJ.transform.position += Vector3.ClampMagnitude(Director, speed);

            _Tp2SentinelOBJ.transform.rotation = Quaternion.Euler(Vector3.forward * DirectorAngle);

            if (distance < ArriveDist)
            {
                CurrentWaypoints++;
            }
        }
  }

    public void Reset()
    {
        CurrentWaypoints = 0;
        ReachedDest = false;
        _SearchPath.Clear();

    }

}
