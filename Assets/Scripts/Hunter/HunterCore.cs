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

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flocker"))
        {
           Boids.Add(collision.gameObject);
        }
        else
        {
            return;
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
