using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Script para instaciar prefabs de flock

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior; // los distintos comportamientos

    [Range(1, 10)] //cantidad de Agentes en la escena
    public int staringCount = 5;
    const float AgentDensity = 0.08f; 
    [Range(1f, 100f)] //multiplicador paravel del behavior 
    public float driveFactor = 10f;
    [Range(1f, 100f)] //vel max
    public float maxSpeed = 5f;
    [Range(1f, 10f)]// radios para el neighbors
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]// radios del avoidance
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    float squareAvoidanceRadius;
    // compara los squares porque es más eficiente


    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < staringCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * staringCount * AgentDensity, //va a regular la cantidad de boids en el flock
                Quaternion.Euler(Vector3.forward*Random.Range(0f,360f)),// rotacion
                transform                                                       
                );
            newAgent.name = "AgentConejo" + i;
            agents.Add(newAgent);
        }
    }

    
    void Update()
    {

        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f); //6 vecino
            Vector2 move = behavior.CalculateMove(agent, context, this); // this el flock
            move *= driveFactor;
            //chequear si se excedio el maxspeed
            if(move.sqrMagnitude>squareMaxSpeed)
            {
                move = move.normalized * maxSpeed; 
            }
            agent.Move(move);
        }



    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        //iterar entre los colliders del array y poner en lista
        foreach(Collider2D c in contextColliders)
        {
            if(c!=agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }


}
