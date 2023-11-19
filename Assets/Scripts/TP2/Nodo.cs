using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public List<Nodo> _neighbors = new List<Nodo>(); 
    int _cost = 1;
    public int Cost { get { return _cost; } }
    public LayerMask obstacleMask;
    private void OnDrawGizmos()
    {
        foreach (var element in _neighbors)
        {
            Vector3 dir = element.transform.position - transform.position;
            Gizmos.color = Color.white;

            Gizmos.DrawLine(transform.position, element.transform.position);

        }

    }
}
