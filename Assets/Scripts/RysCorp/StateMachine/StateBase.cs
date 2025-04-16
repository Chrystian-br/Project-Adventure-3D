using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RysCorp.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(object o = null)
        {
            Debug.Log("State Enter");
        }

        public virtual void OnStateStay()
        {
            Debug.Log("State Stay");
        }

        public virtual void OnStateExit()
        {
            Debug.Log("State Exit");
        }
    }
}

