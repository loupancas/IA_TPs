using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavoir/Aligment")]
public class AlignmentBehavoir : FlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //si no hay boids cerca mantener el alineamiento
        if (context.Count == 0)
        {
            return agent.transform.up; //magnitud1
        }
        //agregar todos los puntos cercana y sacar el promedio
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform item in context)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= context.Count; //dividir por el numero de vecinos
       
        return alignmentMove;
    }
}
