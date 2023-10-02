using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] int _offsetX;
    [SerializeField] int _offsetY;
    [SerializeField] Transform[] _objectTransforms; // Change from GameObject[] to Transform[]
    Transform _spawnedObjectTransform; // Change from GameObject to Transform

    int _randomX;
    int _randomY;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawner();
        }
    }

    void Spawner()
    {
        int transformId = Random.Range(0, _objectTransforms.Length);
        Vector2 position = RandomCoordinates();
        _spawnedObjectTransform = Instantiate(_objectTransforms[transformId], position, Quaternion.identity);
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
