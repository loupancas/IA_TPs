using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBunny : SteeringAgent
{
    
    [SerializeField, Range(0f, 2.5f)] float _alignmentWeight = 1;
    [SerializeField, Range(0f, 2.5f)] float _separationWeight = 1;
    [SerializeField, Range(0f, 2.5f)] float _cohesionWeight = 1;
    [SerializeField] Transform _target;
    [SerializeField] SteeringAgent _cazador;
    bool isRover; //deambulando
    [SerializeField] Camera _camera;
    [SerializeField] int _offsetX;
    [SerializeField] int _offsetY;
    int _randomX;
    int _randomY;
    public Vector2 currentTargetPoint;
    //private Renderer renderer;
    void Start()
    {

        //isRover = true;
        //RefreshTargetPoint();
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        var dir = new Vector3(x, y);

        _velocity = dir.normalized * _maxSpeed;

        GameManager.instance.allAgents.Add(this);
    }

    void Update()
    {
        Move();
       
        Flocking();
        
        AddForce(Evade(_cazador));
        
        if (!HastToUseObstacleAvoidance()) AddForce(Arrive(_target.position));


       /* if (isRover)
        {
            // transform.position = Vector2.MoveTowards(transform.position, currentTargetPoint, speed * Time.deltaTime); //deambular

            Vector2 direction = (currentTargetPoint - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * _maxSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, currentTargetPoint) < 0.01f)
            {
                RefreshTargetPoint();

            }

            GetComponent<Renderer>().material.color = Color.green;
        }*/

    }

    private void Flocking()
    {
        var boids = GameManager.instance.allAgents;
        AddForce(Alignment(boids) * _alignmentWeight);
        AddForce(Separation(boids) * _separationWeight); //Se aplique un radio mas chico al actual
        AddForce(Cohesion(boids) * _cohesionWeight);
        Debug.Log("Flocking");
        //renderer.material.color = Color.blue;
    }

    /*protected void RefreshTargetPoint()
    {
        currentTargetPoint = RandomCoordinates(); //buscar otro punto
        Debug.Log(currentTargetPoint);
    }

    Vector2 RandomCoordinates()
    {
        _randomX = Random.Range(0 + _offsetX, Screen.width - _offsetX);
        _randomY = Random.Range(0 + _offsetY, Screen.height - _offsetY);

        Vector2 coordinates = new Vector2(_randomX, _randomY);

        Vector2 screenToWorldPosition = _camera.ScreenToWorldPoint(coordinates);
        return screenToWorldPosition;
    }*/


}
