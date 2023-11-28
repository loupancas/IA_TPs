using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tp2_Sentinel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TP2_Manager _Manager;
    [SerializeField] Tp2_SentinelStateMachine _StateMachine;
    public List<Tp2_Sentinel> EnemiesToAlert = new List<Tp2_Sentinel>();
    [SerializeField] FieldOfViewVisualComp _FoVScript;


    [Header("Variables")]
    public Node_Script NearestNode;
    public Node_Script _PlayerNearest;
    public bool EnemySpotted;

    public bool _Alarmed;

    [Header("Events")]
    public UnityEvent AlarmTrigger;

    public void Start()
    {
        _Manager = FindObjectOfType<TP2_Manager>();
        _StateMachine = GetComponent<Tp2_SentinelStateMachine>();
        _FoVScript= GetComponent<FieldOfViewVisualComp>();
        StartCoroutine(CoorutineFindNearestNode());
        _Manager._SentinelList.Add(this);
    }

    public void Update()
    {
        EnemySpotted = _FoVScript.CanSeePlayer;
        _StateMachine.Enemyspotted = EnemySpotted;
        _StateMachine._SentinelNearestNode = NearestNode;

        _PlayerNearest = _Manager._NearestPlayerNode;
        _StateMachine._PlayernearestNode = _PlayerNearest;
        _StateMachine.Alarm = _Alarmed;

        if (EnemySpotted)
        {
            _Manager.RaiseAlarm(this);
        }
        _StateMachine.RunStateMachine();
    }

    float NearestVal = float.MaxValue;
    IEnumerator CoorutineFindNearestNode()
    {
        float Delay = 0.25f;
        WaitForSeconds wait = new WaitForSeconds(Delay);

        while (true)
        {
            yield return wait;
            NearestVal = float.MaxValue;
            NearestNode = FindNearestNode();
        }
    }

    Node_Script nearest;
    private Node_Script FindNearestNode()
    {
        foreach (Node_Script CurrentNode in _Manager._NodeList)
        {
            float CurrentDis = Vector3.Distance(CurrentNode.NodeTransform.position, transform.position);
            if (CurrentDis < NearestVal)
            {
                NearestVal = CurrentDis;
                nearest = CurrentNode;
            }
        }
        return nearest;

    }

    public void SetAlarmStatus()
    {
        _Alarmed = true;
    }

}
