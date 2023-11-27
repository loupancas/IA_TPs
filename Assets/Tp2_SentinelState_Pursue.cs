using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp2_SentinelState_Pursue : State
{
    [Header("References")]
    [SerializeField] TP2_Manager _Manager;
    [SerializeField] Tp2_SentinelStateMachine _Tp2StateMachine; // ref. al statemachine
    [SerializeField] GameObject _Tp2SentinelOBJ; // ref. gameobject del sentinel

    [Header("State References")]
    [SerializeField] Tp2Sentinel_StatePatrol _SentinelPatrol;
    [SerializeField] Tp2_SentinelState_Search _SentinelAlarm;


    [Header("Variables")]
    [SerializeField] float speed;
    [SerializeField] GameObject _Player;
    private void Start()
    {
        _Tp2StateMachine = GetComponentInParent<Tp2_SentinelStateMachine>();
        _Manager = _Tp2StateMachine._Manager;

    }


    public override State RunCurrentState()
    {
        if(_Tp2StateMachine.Enemyspotted== false)
        {
            _Tp2StateMachine.SwitchToNewState(_SentinelPatrol);
            return _SentinelPatrol;
        }
        else if(_Tp2StateMachine.Alarm == true && _Tp2StateMachine.Enemyspotted == false)
        {
            _Tp2StateMachine.SwitchToNewState(_SentinelAlarm);
            return _SentinelAlarm;
        }
        else
        {
            PursueLogic();
            return this;
        }
    }

    private void PursueLogic()
    {
        //calcular director
        Vector3 _Director = (_Player.transform.position - _Tp2SentinelOBJ.transform.position).normalized;
        float _DirectorAngle = Mathf.Atan2(_Director.y, _Director.x) * Mathf.Rad2Deg;
        _Tp2SentinelOBJ.transform.position += Vector3.ClampMagnitude(_Director, speed);
        _Tp2SentinelOBJ.transform.rotation = Quaternion.Euler(Vector3.forward * _DirectorAngle);
    }
}
