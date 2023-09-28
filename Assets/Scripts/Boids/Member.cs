using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public Level Level;
    public MemberConfig conf;

    Vector3 wanderTarget;

    private void Start()
    {
        Level = FindObjectOfType<Level>();
        conf= FindObjectOfType<MemberConfig>();

        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3),0);
    }

    private void Update()
    {
        acceleration = Combine();
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAceleration);
        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity);
        position = position + velocity * Time.deltaTime;
        WrapAround(ref position, -Level.bounds,Level.bounds);
        transform.position = position;
    }

    protected Vector3 Wander()
    {
        float jitter = conf.wanderJitter * Time.deltaTime;
        wanderTarget += new Vector3(RandomBinomial() * jitter, RandomBinomial() * jitter, 0);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= conf.wanderRadius;
        Vector3 targetInLocalSpace = wanderTarget + new Vector3(0, conf.wanderDistance, 0);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);
        targetInWorldSpace -= this.position;
        return targetInWorldSpace.normalized;

    }

    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        var neighbors = Level.GetNeighbors(this, conf.cohesionRadius);
        if (neighbors.Count == 0)
            return cohesionVector;
        foreach (var member in neighbors)
        {
            if(isInFieldOfVision(member.position))
            {
                cohesionVector += member.position;
                countMembers++;
            }
        }

        if(countMembers==0)
        {
            return cohesionVector;
        }
        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;

    }

    Vector3 Alignment()
    {
        Vector3 alingVector = new Vector3();
        var members = Level.GetNeighbors(this, conf.alignmentRadius);
        if (members.Count == 0)
            return alingVector;
        foreach (var member in members)
        {
            if (isInFieldOfVision(member.position))
                alingVector += member.velocity;
        }

        return alingVector.normalized;
    }

    Vector3 Separation()
    {
        Vector3 separateVector = new Vector3();
        var members = Level.GetNeighbors(this, conf.separationRadius);
        if (members.Count == 0)
            return separateVector;

        foreach (var member in members)
        {
            if(isInFieldOfVision(member.position))
            {
                Vector3 mobingTowards = this.position - member.position;
                if(mobingTowards.magnitude>0)
                {
                    separateVector += mobingTowards.normalized / mobingTowards.magnitude;
                }
            }

        }

        return separateVector.normalized;
    }

    virtual protected Vector3 Combine()
    {
        Vector3 finalVec = conf.cohesionPriority * Cohesion() + conf.wanderPriority * Wander() + conf.alignmentPriority * Alignment()+conf.separationPriority*Separation();
        return finalVec;
    }

    void WrapAround(ref Vector3 vector, float min, float max) //para el screen
    {
        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
        vector.z = WrapAroundFloat(vector.z, min, max);
    }

    float WrapAroundFloat(float value, float min, float max)
    {
        if (value > max)
            value = min;
        else if (value < min)
            value = max;
        return value;
    }

    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    bool isInFieldOfVision(Vector3 vec)
    {
        return Vector3.Angle(this.velocity, vec - this.position) <= conf.maxFieldOfView;
    }

}
