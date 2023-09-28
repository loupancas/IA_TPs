using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    public Spawner spawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Cazador"))
        {
            spawner.SpawnObject();
        }
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
