using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class Hoont : State
{

    [Header("StateMachineRefs")]
    [SerializeField] HunterStateMachine stateMachine;
    [SerializeField] Patrol PatrolState;
    [SerializeField] Rest RestState;

    [Header("References")]
    [SerializeField] HunterCore hunterCore;
    [SerializeField] Transform HunterTransform;
    [SerializeField] GameObject target;

    [Header("Variables")]
    [SerializeField] float obstacleDist;
    [SerializeField] LayerMask Obstacle;
    [SerializeField] float EatDistance;

    public void Start()
    {
        bnnuydistance = float.MaxValue;
        HunterTransform = hunterCore.transform;
    }

    private void LateUpdate()
    {
        if(target!= null)
        {
            StartCoroutine(Check()) ;
        }
    }

    public override State RunCurrentState()
    {

        if(stateMachine.HoontTime == false)
        {
            stateMachine.SwitchtoNewState(PatrolState);
            return PatrolState;
        }
   
        if(target == null || hunterCore.Boids.Contains(target) == false)
        {
            bnnuydistance= float.MaxValue;
            ChooseTarget();
        }

        if (Physics2D.Raycast(HunterTransform.position + HunterTransform.up * 0.6f, HunterTransform.right, obstacleDist, Obstacle))
        {
   
            ObstacleAvoid(-1);
            return this;
     

        }
        else if (Physics2D.Raycast(HunterTransform.position, HunterTransform.right, obstacleDist, Obstacle))
        {
       
            ObstacleAvoid(1);
            return this;
        
        }
        else if (Physics2D.Raycast(HunterTransform.position + -HunterTransform.up * 0.6f, HunterTransform.right, obstacleDist, Obstacle))
        {

            ObstacleAvoid(1);
            return this;
        
        }
        else
        {
            MovementLogic();

        }

        if(Vector3.Distance(target.transform.position, HunterTransform.position) < EatDistance)
        {
            //target.GetComponent<BoidBunny>().Die();
            Destroy(target.gameObject);
        }

        return this;
    }

   

    private void MovementLogic()
    {
        //ChooseTarget();

        //calcular distancia entre waypoint y hunter
        distance = Vector2.Distance(target.transform.position, hunterCore.transform.position);

        // calcular vector director
        Vector3 Director = (ProjectedMovement(1) - hunterCore.transform.position) * hunterCore.speed;

        // calcular angulo de rotacion
        float DirectorAngle = MathF.Atan2(Director.y, Director.x) * Mathf.Rad2Deg;

        hunterCore.transform.position += Vector3.ClampMagnitude(Director, hunterCore.speed) * Time.deltaTime;

        //HunterTransform.right = Director;
        hunterCore.transform.rotation = Quaternion.Euler(Vector3.forward * DirectorAngle);


    }



    Vector3 V3PreviousVel, V3PreviousAccel, TargetPrevious;
    [SerializeField] Vector3 V3AvgAccl, V3AvgVel;
    IEnumerator Check()
    {
        yield return new WaitForEndOfFrame();

        Vector3 V3Velocity = (target.transform.position - TargetPrevious) / Time.deltaTime;

        Vector3 V3Accel = V3Velocity - V3PreviousVel;

        V3AvgVel = V3Velocity;
        V3AvgAccl = V3Accel;

        ProjectedMovement(1);

        V3PreviousVel = V3Velocity;
        V3PreviousAccel = V3Accel;
        TargetPrevious = target.transform.position;


    }

    private Vector3 ProjectedMovement(float Ftime)
    {
        Vector3 V3Ret = new Vector3();

        V3Ret = target.transform.position + (V3AvgVel * Time.deltaTime * (Ftime / Time.deltaTime)) + (0.5f * V3AvgAccl * Time.deltaTime * Mathf.Pow(Ftime / Time.deltaTime, 2));

        return V3Ret;
    }

    private void OnDrawGizmos()
    {
        if(target == null) return;  

        Debug.DrawLine(HunterTransform.position,ProjectedMovement(1),Color.yellow);
    }

    float distance;
    float bnnuydistance;
    GameObject bnnuy;
    private void ChooseTarget()
    {
        foreach(GameObject bunny in hunterCore.Boids)
        {
            distance = Vector3.Distance(hunterCore.transform.position, bunny.transform.position);

            if(distance < bnnuydistance)
            {
                bnnuy = bunny;
                bnnuydistance = distance;
            }
        }
        hunterCore.energy--;
        target = bnnuy;
        return;
    }

     private void ObstacleAvoid(float Detector)
     {
            if(target != null)
            {
                RaycastHit2D BnnuyHit = Physics2D.Raycast(HunterTransform.position + hunterCore.transform.up * 0.6f, (target.transform.position - hunterCore.transform.position), 20f);
                RaycastHit2D BnnuyHit2 = Physics2D.Raycast(HunterTransform.position + (-hunterCore.transform.up * 0.6f), (target.transform.position - hunterCore.transform.position), 20f);
                print(BnnuyHit.collider.tag);

                if (BnnuyHit.collider.transform == target.transform && BnnuyHit2.collider.transform == target.transform)
                {
                    print(BnnuyHit.collider.gameObject.tag);
                    MovementLogic();
                    return;
                }
            }
      

            Vector3 Director = new Vector3(HunterTransform.position.x, HunterTransform.transform.position.y + Detector, HunterTransform.position.z) * hunterCore.speed;
            HunterTransform.position += Vector3.ClampMagnitude(Director, hunterCore.speed) * Time.deltaTime;
 
            return;
     }

 
}
