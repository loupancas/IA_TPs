using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavoir/Avoidance")]
public class AvoidanceBehavoir : FlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //si no hay boids cerca no retornar nada
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        //agregar todos los puntos cercana y sacar el promedio
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            if(Vector2.SqrMagnitude(item.position-agent.transform.position)<flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position-item.position);
            }
            
        }
        if (nAvoid > 0)
        avoidanceMove /= nAvoid;
        return avoidanceMove;
    }
}
