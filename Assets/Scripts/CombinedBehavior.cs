using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombinedBehavior : MonoBehaviour
{
    public Transform target;
    public Transform obstacle;
    public Transform cazador;
    public float seekSpeed = 5f;
    public float arriveSpeed = 2f;
    public float slowingRadius = 3f;
    public float neighborRadius = 2f;
    public float separationWeight = 1f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float evadeRadius = 5f;
    public float fleeRadius = 7f;  // Radius to start fleeing from the target
   
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Flee behavior
        if (cazador != null)
        {
            Vector3 toTarget = cazador.position - transform.position;
            float distanceToTarget = toTarget.magnitude;

            if (distanceToTarget < fleeRadius)
            {
                Vector3 fleeDirection = (transform.position - cazador.position).normalized;
                transform.position += fleeDirection * seekSpeed * Time.deltaTime;

                // Change color for flee behavior
                renderer.material.color = Color.magenta;

                return;  // Skip other behaviors when fleeing
            }
        }

        // Evade behavior
        if (obstacle != null)
        {
            Vector3 toTarget = obstacle.position - transform.position;
            float distanceToTarget = toTarget.magnitude;

            if (distanceToTarget < evadeRadius)
            {
                Vector3 evasionDirection = (transform.position - obstacle.position).normalized;
                transform.position += evasionDirection * seekSpeed * Time.deltaTime;

                // Change color for evade behavior
                renderer.material.color = Color.yellow;

                return;  // Skip other behaviors when evading
            }
        }

        

        // Seek and Arrive behavior
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;
          
            if (distance > 0)
            {
                float targetSpeed = distance > slowingRadius ? seekSpeed : arriveSpeed * (distance / slowingRadius);
                targetSpeed = Mathf.Min(targetSpeed, seekSpeed);

                direction.Normalize();
                transform.position += direction * targetSpeed * Time.deltaTime;

                // Change color for seek/arrive behavior
                renderer.material.color = Color.red;

                //Eat
            }
        }

        // Flocking behavior
        Vector2 separation = Vector2.zero;
        Vector2 alignment = Vector2.zero;
        Vector2 cohesion = Vector2.zero;

        GameObject[] flockers = GameObject.FindGameObjectsWithTag("Flocker");

        foreach (GameObject flocker in flockers)
        {
            if (flocker != gameObject)
            {
                float distance = Vector2.Distance(transform.position, flocker.transform.position);

                // Separation
                if (distance < neighborRadius)
                {
                    Vector2 toNeighbor = transform.position - flocker.transform.position;
                    separation += toNeighbor.normalized / distance;
                }

                // Alignment
                if (distance < neighborRadius)
                {
                    alignment += (Vector2)flocker.transform.position - (Vector2)transform.position;
                }

                // Cohesion
                if (distance < neighborRadius)
                {
                    cohesion += (Vector2)flocker.transform.position;
                }
            }
        }

        // Apply the three rules
        separation *= separationWeight;
        alignment *= alignmentWeight;
        cohesion = ((cohesion / flockers.Length) - (Vector2)transform.position).normalized * cohesionWeight;

        // Combine the forces
        Vector2 combinedForce = separation + alignment + cohesion;

        // Move the object
        transform.position += (Vector3)combinedForce * Time.deltaTime;

        // Change color for flocking behavior
        renderer.material.color = Color.blue;

        


    }

    void Eat(Collision2D other)
    {

        if (other.gameObject.CompareTag("Food"))
        {
            // Eat
            Destroy(gameObject);
        }
    }


}


