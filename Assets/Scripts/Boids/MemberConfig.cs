using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour
{
    public float maxFieldOfView = 100;
    public float maxAceleration;
    public float maxVelocity;

    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderPriority;

    //cohesion
    public float cohesionRadius;
    public float cohesionPriority;

    //aligment
    public float alignmentRadius;
    public float alignmentPriority;

    //separation
    public float separationRadius;
    public float separationPriority;

    //Avoidance
    public float avoidanceRadius;
    public float avoidancePriority;
}
