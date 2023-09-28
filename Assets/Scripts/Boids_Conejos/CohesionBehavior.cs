using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Flock/Behavoir/Cohesion")]
public class CohesionBehavior : FlockBehavior
{
    //acercarse a los vecinos los mas que puedan según su cercanía
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
       //si no hay boids cerca no retornar nada
       if(context.Count==0)
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
        return cohesionMove;

    }
}
