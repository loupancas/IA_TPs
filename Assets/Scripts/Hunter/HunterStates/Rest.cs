using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : State
{
    [SerializeField] float pulse;
    [SerializeField] float Timer;
    [SerializeField] bool a, b;

    [SerializeField] HunterCore hunterCore;
    [SerializeField] HunterStateMachine HunterStateMachine;

    [SerializeField] float MinDistancetoHoont;

    [SerializeField] Patrol PatrolState;
    [SerializeField] Hoont HoontState;

    float distance, closestboiddistance;

    public void Start()
    {
        pulse = 0;
       hunterCore = GetComponentInParent<HunterCore>();
        closestboiddistance = float.MaxValue;
    }

    public override State RunCurrentState()
    {
        if(pulse >= Timer)
        {
            hunterCore.energy = hunterCore.MaxEnegy;
            ChooseNextState();
        }
        else
        {
            pulse += Time.deltaTime;
            
        }
   
        return this;

    }


    GameObject ChosenBoid;
    private void ChooseNextState() // timer finalizado, comienza a elegir nuevo estado
    {
        if(hunterCore.Boids.Count == 0) // no hay boids en la lista del hunter
        {
            b = true; a = false;
            HunterStateMachine.SwitchtoNewState(PatrolState); 
            pulse = 0;
           
         
        }
        else // hay boids en la lista
        {
            // comparo la distancia entre cada boid de la lista y el hunter
            foreach(GameObject BOID in hunterCore.Boids) 
            {
                distance = Vector3.Distance(BOID.transform.position, hunterCore.transform.position);
                if(distance < closestboiddistance)
                {
                    ChosenBoid = BOID;
                    closestboiddistance= distance;
                }
            }
            if(ChosenBoid!= null) // compruebo que se eligio un boid correcto
            {
                HoontState.SwitchTarget(ChosenBoid);
                HunterStateMachine.SwitchtoNewState(HoontState);
                a = true; b = false;
               
            }
            else // en el caso de que no se pudo elegir un boid, iniciar patrol 
            {
                HunterStateMachine.SwitchtoNewState(PatrolState);
                pulse = 0;
                b = true; a = false;
         
            }
            
            

        }

       
    }
}
