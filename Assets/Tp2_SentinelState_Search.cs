using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float speed;
    [SerializeField] GameObject _Player;
    public Node_Script _PlayerNode;
    public Node_Script _SentinelNode;
    [SerializeField] List <Transform> _SearchPath = new List<Transform>();

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
        if(_Tp2StateMachine.Enemyspotted == true)
        {
            _Tp2StateMachine.SwitchToNewState(_SentinelPursue);
            return _SentinelPursue;
        }
        else
        {
            AlarmLogic();
            return this;
        }
    }

<<<<<<< HEAD
  private void AlarmLogic()
  {
        if(_SearchPath.Count <= 0)
        {
            _Manager.PathFinding(_SearchPath, _SentinelNode, _PlayerNode);
        }


  }


=======
    public List<Vector3> GetPathBasedOnPFTypePlayer()
    {
        //return _pf.AStar(StartNode(), GoalNodePlayerPos());
        return default;
    }
>>>>>>> 5c796e52699fad5611b7e5bef351ac82cb8c6ab5
}
