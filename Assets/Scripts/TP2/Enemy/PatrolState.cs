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
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        if(enemyPath.Count>0)
        {
            _enemy.TravelPath(enemyPath);
        }
    }


    void MovementPatrolNodes()
    {
        _enemy.AddForce(_enemy.Seek(_enemy.patrolNodes[_enemy.currentPatrolNode].transform.position));

    }

}
