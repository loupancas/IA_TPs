using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterCore : MonoBehaviour
{
    [Header("References")]
    public List<GameObject> Boids= new List<GameObject>();
    CircleCollider2D Collider2D;

    [Header("State Machine")]
    public HunterStateMachine StateMachine;

    [Header ("variables")]

    public float speed, energy,MaxEnegy, VisionRadius,rotspeed;
    protected float TimerEnergy, Pulse;
    public bool HasTarget;
  



    // Start is called before the first frame update
    void Start()
    {
        StateMachine = GetComponent<HunterStateMachine>();

        Collider2D = GetComponent<CircleCollider2D>();

        Collider2D.radius= VisionRadius;

        energy = MaxEnegy;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(energy > 1)
        {
            StateMachine.IsCharged= true;
            StateMachine.RunStateMachine();
        }
        else
        {
            StateMachine.IsCharged = false;

            StateMachine.RunStateMachine();
        }

        CheckforTargets();


    }

    private void CheckforTargets()
    {
       if(Boids.Count> 0)
       {
            HasTarget= true;
            StateMachine.HoontTime = true;
       }
        else
        {
            HasTarget = false;
            StateMachine.HoontTime = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        print("See somfin");
        if (collision.CompareTag("Flocker") || collision.tag == "Flocker")
        {
           Boids.Add(collision.gameObject);
        }
      
    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Flocker"))
        {
            Boids.Remove(collision.gameObject);
        }
        else
        {
            return;
        }
    }


}
