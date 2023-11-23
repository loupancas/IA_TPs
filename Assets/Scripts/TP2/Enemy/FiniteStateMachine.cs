using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    States _currentState;
    //Diccionario para los estados
    Dictionary<EnemyStates, States> _allStates = new Dictionary<EnemyStates, States>();
    //Si este codigo quiero que sea generico, no deberia estar llamando a un enum en especifico
    
    public void Update()
    {
        //if(_currentState != null) _currentState.OnUpdate(); //Evita llamados nulos

        //Versiones mas recientes
        _currentState?.OnUpdate(); //Evita llamados nulos
    }

    public void AddState(EnemyStates name, States state)
    {
        if (!_allStates.ContainsKey(name))
        {
            _allStates.Add(name, state);
            state.fsm = this;
        }
        else
        {
            _allStates[name] = state;
        }
    }

    public void ChangeState(EnemyStates name) //Para ir cambiando de estados, los vamos pidiendo
    {
        _currentState?.OnExit(); //Si o si necesario preguntar por null
        if (_allStates.ContainsKey(name)) _currentState = _allStates[name];
        _currentState?.OnEnter();
    }
}
