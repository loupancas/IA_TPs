using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : States
{
    Enemy _enemy;
    List<Transform> _PathtoTarget;
    TP2_Manager _Manager;

    public Pursuit(Enemy enemy, List<Transform> Path)
    {
        _enemy= enemy;
        _PathtoTarget = Path;
        _Manager = enemy._Manager;
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        Debug.Log("Pursuing target");

        if (_enemy.InFieldOfView(_enemy._target.position) || _enemy._alertAllEnemies == true)
        {
            _enemy._pursuitTarget = true;
            fsm.ChangeState(EnemyStates.Pursuit);
        }
        else 
        {
            _enemy._pursuitTarget = false;
            fsm.ChangeState(EnemyStates.Patrol);
        }

    }

    public void PursuitLogic(List<Transform> _PursuePath)
    {
        _enemy.transform.position = _Manager._Player.transform.position;
    }
}
