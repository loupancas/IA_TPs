using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float timeToSpawn;
    private float currentTimeToSpawn;
    public bool isTimer;
    void Start()
    {
        currentTimeToSpawn = timeToSpawn;
    }

    private void Update()
    {
        if(isTimer)
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    public void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
