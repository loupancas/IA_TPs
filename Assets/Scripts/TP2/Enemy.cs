using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PathFinding pathFinding =new PathFinding();
    List<Vector3> _path = new List<Vector3>();
    List<Vector3> _pathFinding = new List<Vector3>();
    public List<Enemy> EnemiesToAlert = new List<Enemy>();
    public FiniteStateMachine _fsm;

    [SerializeField] List<Nodo> pathNodes = new List<Nodo>();

    public Nodo[] patrolNodes;
    public LayerMask obstacles;
    public int currentPatrolNode;

    public bool _alertAllEnemies;
    public bool _pursuitTarget;

    [SerializeField] public Transform _target;


    public Vector3 _velocity;
    public float _speed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxForce;
    [SerializeField] float _viewRadius;
    [SerializeField] float _viewAngle;

    private void Start()
    {
        _fsm = new FiniteStateMachine();
        _fsm.AddState(EnemyStates.Patrol, new PatrolState(this,_path));
        // _fsm.AddState(EnemyStates.Pursuit, new PatrolState());
        //_fsm.AddState(EnemyStates.Return, new PatrolState());
        _fsm.ChangeState(EnemyStates.Patrol);

    }

    private void Update()
    {
        _fsm.OnUpdate();

        if (_pathFinding.Count>0)
        {
            TravelPath(_pathFinding);
        }
    }

    public void TravelPath(List<Vector3> _path)
    {
        Vector3 target = _path[0] - Vector3.forward;
        Vector3 dir = target - transform.position;
        transform.forward = dir;
        transform.position += dir.normalized * _speed * Time.deltaTime;

        if (Vector3.Distance(target, transform.position) <= 0.1f) _path.RemoveAt(0);
    }

    //FOV (Field of View)
    public bool InFieldOfView(Vector3 endPos)
    {
        Vector3 dir = endPos - transform.position;
        if (dir.magnitude > _viewRadius) return false;
        if (!InLineOfSight(transform.position, endPos)) return false;
        if (Vector3.Angle(transform.forward, dir) > _viewAngle / 2) return false;
        return true;
    }

    //LOS (Line of Sight)
    public bool InLineOfSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        return !Physics.Raycast(start, dir, dir.magnitude, obstacles);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Vector3 DirA = GetAngleFromDir(_viewAngle / 2 + transform.eulerAngles.y);
        Vector3 DirB = GetAngleFromDir(-_viewAngle / 2 + transform.eulerAngles.y);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + DirA.normalized * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + DirB.normalized * _viewRadius);

        Gizmos.color = Color.green;

        Vector3 leftRayPos = transform.position + transform.up * 0.5f;
        Vector3 rightRayPos = transform.position - transform.up * 0.5f;

        Gizmos.DrawLine(leftRayPos, leftRayPos + transform.right * _viewRadius);
        Gizmos.DrawLine(rightRayPos, rightRayPos + transform.right * _viewRadius);

    }

    Vector3 GetAngleFromDir(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desired = (targetPos - transform.position).normalized * _maxSpeed;

        Vector3 steering = desired - _velocity;

        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        return steering;
    }

    public void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }
}


public enum EnemyStates
{
    Patrol,
    Pursuit,
    Return
}