using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace RysCorp.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        #region VARIAVEIS
            public Dictionary<T, StateBase> dictionaryState;

            public StateBase _currentState;

            public StateBase CurrentState{ get { return _currentState; } }
        #endregion
        
        
        #region METODOS
            public void Init()
            {
                dictionaryState = new Dictionary<T, StateBase>();
            }

            public void RegisterStates(T typeEnum, StateBase state)
            {
                dictionaryState.Add(typeEnum, state);
            }

            public void SwitchState(T state)
            {
                if(_currentState != null) _currentState.OnStateExit();

                _currentState = dictionaryState[state];

                _currentState.OnStateEnter();
            }
        #endregion

        
        #region UNITY-METODOS
            public void Update()
            {
                if (_currentState != null) _currentState.OnStateStay();
            }
        #endregion
        

        
    }

}

