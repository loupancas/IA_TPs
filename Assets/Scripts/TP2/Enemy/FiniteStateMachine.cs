using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    States _currentState;
    //Diccionario para los estados
    Dictionary<EnemyStates, States> _allStates = new Dictionary<EnemyStates, States>();
    //Si este codigo quiero que sea generico, no deberia estar llamando a un enum en especifico
    
    public void OnUpdate()
    {
        //if(_currentState != null) _currentState.OnUpdate(); //Evita llamados nulos

        //Versiones mas recientes
        _currentState?.OnUpdate(); //? evita llamados nulos
    }

    public void AddState(EnemyStates nameKey, States state)
    {
        if (!_allStates.ContainsKey(nameKey))
        {
            _allStates.Add(nameKey, state);
            state.fsm = this;
        }
        else
        {
            _allStates[nameKey] = state;
        }
    }

    public void ChangeState(EnemyStates nameKey) //Para ir cambiando de estados, los vamos pidiendo
    {
        if (!_allStates.ContainsKey(nameKey)) return; //Si la clave no está presente, el código sale inmediatamente de la función 

        _currentState?.OnExit(); //Si o si necesario preguntar por null
        if (_allStates.ContainsKey(nameKey)) _currentState = _allStates[nameKey];
        _currentState?.OnEnter();
    }
}
