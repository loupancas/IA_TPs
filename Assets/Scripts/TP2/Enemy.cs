using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PathFinding pathFinding =new PathFinding();
    List<Vector3> _path = new List<Vector3>();
    List<Vector3> _pathFinding = new List<Vector3>();


    [SerializeField] List<Nodo> pathNodes = new List<Nodo>();

    public Nodo[] patrolNodes;
    public LayerMask obstacles;

    public float _speed;

    private void Update()
    {
        if (_pathFinding.Count>0)
        {
            TravelPath(_pathFinding);
        }
    }

    private void TravelPath(List<Vector3> _path)
    {
        Vector3 target = _path[0] - Vector3.forward;
        Vector3 dir = target - transform.position;
        transform.position += dir.normalized * _speed * Time.deltaTime;

        if (Vector3.Distance(target, transform.position) <= 0.1f) _path.RemoveAt(0);
    }

}
