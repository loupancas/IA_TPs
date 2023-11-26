using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState :States
{
    Enemy _enemy;
    List<Vector3> enemyPath = new List<Vector3>();

    public PatrolState(Enemy enemyType, List<Vector3> path)
    {
        _enemy = enemyType;
        enemyPath = path;
    }
    public override void OnEnter()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Empiezo a patrullar");
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        if (_enemy.InFieldOfView(_enemy._target.position) || _enemy._alertAllEnemies == true)
        {
            _enemy._pursuitTarget = true;
            fsm.ChangeState(EnemyStates.Pursuit);
        }
        if (_enemy.InLineOfSight(_enemy.transform.position, _enemy.patrolNodes[_enemy.currentPatrolNode].transform.position))
        {
            MovementPatrolNodes();
        }
        else
        {
            if (enemyPath.Count > 0)
                _enemy.TravelPath(enemyPath);
        }
    }


    void MovementPatrolNodes()
    {
        _enemy.AddForce(_enemy.Seek(_enemy.patrolNodes[_enemy.currentPatrolNode].transform.position));
        if (Vector3.Distance(_enemy.patrolNodes[_enemy.currentPatrolNode].transform.position, _enemy.transform.position) <= 2f) //le puse valor random porque si no empieza a sumar indices sin parar
            _enemy.currentPatrolNode = (_enemy.currentPatrolNode + 1) % _enemy.patrolNodes.Length;
        _enemy.transform.position += _enemy._velocity * Time.deltaTime;
        _enemy.transform.forward = _enemy._velocity;
    }

}
