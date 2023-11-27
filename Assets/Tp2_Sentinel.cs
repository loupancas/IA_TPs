using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp2_Sentinel : MonoBehaviour
{
    TP2_Manager _Manager;
    Tp2_SentinelStateMachine _StateMachine;
    public List<Enemy> EnemiesToAlert = new List<Enemy>();

    public bool EnemySpotted;

    


    public void Start()
    {
        _Manager = GetComponent<TP2_Manager>();
        _StateMachine = GetComponent<Tp2_SentinelStateMachine>();
    }

    public void Update()
    {

        _StateMachine.Enemyspotted = EnemySpotted;
    
        _StateMachine.RunStateMachine();
    }



}
