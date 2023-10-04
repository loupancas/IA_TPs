using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBehaviors : MonoBehaviour
{
    public Transform target;
    public Transform obstacle;
    public Transform cazador;
    public float seekSpeed = 5f;
    public float arriveSpeed = 2f;
    public float slowingRadius = 3f;
    public float neighborRadius = 2f;
    public float separationWeight = 1f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float evadeRadius = 5f;
    public float fleeRadius = 7f;  // Radius to start fleeing from the target
    bool isRover; //deambulando

    [SerializeField] Camera _camera;
    [SerializeField] int _offsetX;
    [SerializeField] int _offsetY;
    int _randomX;
    int _randomY;
    public Vector2 currentTargetPoint;
    public float speed = 1.5f;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        isRover = true;
        RefreshTargetPoint();

    }

    void RefreshTargetPoint()
    {
        currentTargetPoint = RandomCoordinates(); //buscar otro punto
        Debug.Log(currentTargetPoint);
    }


    protected void Move()
    {
        
       // transform.position = Vector2.MoveTowards(transform.position, currentTargetPoint, speed * Time.deltaTime); //deambular

       Vector2 direction = (currentTargetPoint - (Vector2)transform.position).normalized;
       transform.position += (Vector3)(direction * speed * Time.deltaTime);
       
       if (Vector2.Distance(transform.position, currentTargetPoint) < 0.01f)
       {
         RefreshTargetPoint();
                
       }

            
        
    }


    void Update()
    {
       



        // Flee behavior
        if (cazador != null)
        {
            Vector3 toTarget = cazador.position - transform.position;
            float distanceToTarget = toTarget.magnitude;

            if (distanceToTarget < fleeRadius)
            {
                isRover = false;
                Vector3 fleeDirection = (transform.position - cazador.position).normalized;
                transform.position += fleeDirection * seekSpeed * Time.deltaTime;

                // Cambia color para flee behavior
                renderer.material.color = Color.magenta;

                return;  // Skip other behaviors when fleeing
            }
            else
            {
                isRover = true;

            }
        }

        // Evade behavior
        if (obstacle != null)
        {
            Vector3 toTarget = obstacle.position - transform.position;
            float distanceToTarget = toTarget.magnitude;

            if (distanceToTarget < evadeRadius)
            {
                Vector3 evasionDirection = (transform.position - obstacle.position).normalized;
                transform.position += evasionDirection * seekSpeed * Time.deltaTime;

                // Cambia color para evade behavior
                renderer.material.color = Color.yellow;

                return;  // Skip other behaviors when evading
            }
        }



        // Seek and Arrive behavior
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;

            if (distance > 0)
            {
                float targetSpeed = distance > slowingRadius ? seekSpeed : arriveSpeed * (distance / slowingRadius);
                targetSpeed = Mathf.Min(targetSpeed, seekSpeed);

                direction.Normalize();
                transform.position += direction * targetSpeed * Time.deltaTime;

                // Cambia color para seek/arrive behavior
                renderer.material.color = Color.red;

                //Eat
            }
        }

        // Flocking behavior
        Vector2 separation = Vector2.zero;
        Vector2 alignment = Vector2.zero;
        Vector2 cohesion = Vector2.zero;

        GameObject[] flockers = GameObject.FindGameObjectsWithTag("Flocker");

        foreach (GameObject flocker in flockers)
        {
            if (flocker != gameObject)
            {
                float distance = Vector2.Distance(transform.position, flocker.transform.position);

                // Separation
                if (distance < neighborRadius)
                {
                    Vector2 toNeighbor = transform.position - flocker.transform.position;
                    separation += toNeighbor.normalized / distance;
                }

                // Alignment
                if (distance < neighborRadius)
                {
                    alignment += (Vector2)flocker.transform.position - (Vector2)transform.position;
                }

                // Cohesion
                if (distance < neighborRadius)
                {
                    cohesion += (Vector2)flocker.transform.position;
                }
            }
        }

        // Combina separation aligment y cohesion
        separation *= separationWeight;
        alignment *= alignmentWeight;
        cohesion = ((cohesion / flockers.Length) - (Vector2)transform.position).normalized * cohesionWeight;

        // Combine fuerzas
        Vector2 combinedForce = separation + alignment + cohesion;

        // Mueve el objeto
        transform.position += (Vector3)combinedForce * Time.deltaTime;

        // Cambia color para flocking behavior
        renderer.material.color = Color.blue;




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Debug.Log("detecto comida");
            isRover = false;
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, arriveSpeed * Time.deltaTime);
            Destroy(gameObject);
        }
    }

    void Eat(Collision2D other)
    {

        if (other.gameObject.CompareTag("Food"))
        {
            // Eat
            Destroy(gameObject);
        }
    }

    Vector2 RandomCoordinates()
    {
        _randomX = Random.Range(0 + _offsetX, Screen.width - _offsetX);
        _randomY = Random.Range(0 + _offsetY, Screen.height - _offsetY);

        Vector2 coordinates = new Vector2(_randomX, _randomY);

        Vector2 screenToWorldPosition = _camera.ScreenToWorldPoint(coordinates);
        return screenToWorldPosition;
    }
}
