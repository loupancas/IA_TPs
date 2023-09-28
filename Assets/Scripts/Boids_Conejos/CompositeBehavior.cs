using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavoir/CompositeBehavior")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //para la data error
        if(weights.Length!=behaviors.Length)
        {
            Debug.Log("error" + name, this);
            return Vector2.zero;
        }
        //setear movimiento
        Vector2 move = Vector2.zero;
        //iterar en lo comportamientos
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];
            if(partialMove!=Vector2.zero)
            {
                if(partialMove.sqrMagnitude>weights[i]*weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];

                }
                move += partialMove;
            }

        }
        return move;
    }

    
}
