using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Patrol : State
{
    [Header("References")]
    [SerializeField] HunterStateMachine StateMachine;
    [SerializeField] GameObject HunterObj;
    [SerializeField] Rest RestState;

    [Header("variables")]
    [SerializeField] private List<Transform> Waypoints = new List<Transform>();
    [SerializeField] float Speed, arrivaldistance;

    public Transform HunterTransform;

    private float distance;

    private int currentwaypoint;



    public void Start()
    {
        StateMachine = GetComponentInParent<HunterStateMachine>();
        //HunterObj = GetComponentInParent<GameObject>();

        HunterTransform = HunterObj.transform;

        Speed = HunterObj.GetComponent<HunterCore>().speed;
    }

    public override State RunCurrentState()
    {
        CheckEnergy(); 

        MovementLogic();

        return this;

    }

    private void MovementLogic()
    {
        if (distance <= arrivaldistance) // el hunter llego al punto de patrulla
        {
            ArriveWaypoint();
        }

        if (currentwaypoint >= Waypoints.Count) // second catch
        {
            currentwaypoint = 0;
        }
        //calcular distancia entre waypoint y hunter
        distance = Vector2.Distance(Waypoints[currentwaypoint].position, HunterTransform.position);

        // calcular vector director
        Vector2 Director = Waypoints[currentwaypoint].transform.position - HunterTransform.position;
        Director.Normalize();

         // calcular angulo de rotacion
        float DirectorAngle = MathF.Atan2(Director.y,Director.x) * Mathf.Rad2Deg;

        HunterTransform.position = Vector2.MoveTowards(HunterTransform.position, Waypoints[currentwaypoint].transform.position, Speed * Time.deltaTime);
        HunterTransform.rotation = Quaternion.Euler(Vector3.forward * DirectorAngle);

    }

    private void ArriveWaypoint()
    {
        // el hunter llego al waypoint.
        // cambiar waypoint y restar energia
        
        if (currentwaypoint >= Waypoints.Count)
        {
            currentwaypoint = 0;
        }
        else
        {
            currentwaypoint++;
        }

        HunterObj.GetComponent<HunterCore>().energy--;
    }

    private void CheckEnergy()
    {
        // check energia :)
        if (StateMachine.IsCharged == false)
        {
            StateMachine.SwitchtoNewState(RestState);
            
        }

    }



}
