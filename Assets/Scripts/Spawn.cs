using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] int _offsetX;
    [SerializeField] int _offsetY;
    [SerializeField] GameObject _foodPrefab;
    [SerializeField] GameObject _obstaclePrefab;
    public int maxFood = 5;
    public int minFood = 3;

    public int maxObstacle = 5;
    public int minObstacle = 3;


    int _randomX;
    int _randomY;

    void Start()
    {
        int instantiateCountFood = Random.Range(minFood, maxFood);
        int instantiateCountObstacles = Random.Range(minObstacle, maxObstacle);


        for (int i = 0; i < instantiateCountFood; i++)
        {
            Spawner(_foodPrefab);
        }

        for (int i = 0; i < instantiateCountObstacles; i++)
        {
            Spawner(_obstaclePrefab);
        }

        
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawner();
        }*/
    }

    void Spawner(GameObject prefab)
    {
        Vector2 position = RandomCoordinates();
        GameObject go = Instantiate(prefab, position, Quaternion.identity);
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
