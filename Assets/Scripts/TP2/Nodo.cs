using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public List<Nodo> _neighbors = new List<Nodo>(); 
    int _cost = 1;
    public int Cost { get { return _cost; } }
    


}
