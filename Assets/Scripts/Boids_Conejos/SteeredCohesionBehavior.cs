using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavoir/SteeredCohesion")]
public class SteeredCohesionBehavior :FlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime=0.5f; //controla el motion
    //acercarse a los vecinos los mas que puedan según su cercanía
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //si no hay boids cerca no retornar nada
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        //agregar todos los puntos cercana y sacar el promedio
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;
        //crear offset de la posicion del agente
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove,ref currentVelocity, agentSmoothTime);
        return cohesionMove;

    }
}
