using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class Patrol : State
{
    [Header("References")]
    [SerializeField] HunterStateMachine StateMachine;
    [SerializeField] GameObject HunterObj;
    [SerializeField] Rest RestState;
    [SerializeField] Transform target;

    [Header("variables")]
    [SerializeField] private List<Transform> Waypoints = new List<Transform>();
    [SerializeField] float Speed, arrivaldistance, rotspeed, obstacledist;
    [SerializeField] LayerMask obstacles;

    public Transform HunterTransform;

    private float distance;

    private int currentwaypoint;



    public void Start()
    {
        StateMachine = GetComponentInParent<HunterStateMachine>();
        //HunterObj = GetComponentInParent<GameObject>();

        HunterTransform = HunterObj.transform;

        Speed = HunterObj.GetComponent<HunterCore>().speed;
        rotspeed = HunterObj.GetComponent<HunterCore>().rotspeed;
    }

    public override State RunCurrentState()
    {
        CheckEnergy();

        if(Physics2D.Raycast(this.transform.position + this.transform.up * 0.6f, transform.right, obstacledist, obstacles))
        {
            print("dodge");
            ObstacleAvoid(1);

        }
        else if(Physics2D.Raycast(this.transform.position + -this.transform.up * 0.6f, transform.right, obstacledist, obstacles))
        {
            print("dodge");
            ObstacleAvoid(-1);
        }
        else
        {
            MovementLogic();
        }


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
        Vector3 Director = (Waypoints[currentwaypoint].transform.position - HunterTransform.position) * Speed;
        
        // calcular angulo de rotacion
         float DirectorAngle = MathF.Atan2(Director.y,Director.x) * Mathf.Rad2Deg;
 

        HunterTransform.position += Vector3.ClampMagnitude(Director, Speed) * Time.deltaTime;

        //HunterTransform.position = Vector2.MoveTowards(HunterTransform.position, Waypoints[currentwaypoint].transform.position, Speed * Time.deltaTime);

        //HunterTransform.right = Director;
        HunterTransform.rotation = Quaternion.Euler(Vector3.forward * DirectorAngle);
    }

    private void ObstacleAvoid(float Detector)
    {
        print("Dodge");

        Vector3 Director = ((HunterTransform.up) - HunterTransform.position) * Speed;
        Director = Director * Detector;

        HunterTransform.position += Vector3.ClampMagnitude(Director, Speed) * Time.deltaTime;

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



    private void OnDrawGizmos()
    {
        Debug.DrawRay(HunterTransform.position + HunterTransform.up * 0.5f, HunterTransform.right, Color.green);

        Debug.DrawRay(this.transform.position + -this.transform.up * 0.6f, transform.right, Color.green);
    }

}
