using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RysCorp.StateMachine;


public class FSMExample : MonoBehaviour
{
    #region VARIAVEIS
        public enum ExampleEnum
        {
            STATE_ONE,
            STATE_TWO,
            STATE_THREE
        }

        public StateMachine<ExampleEnum> stateMachine;
    #endregion


    #region METODOS

    #endregion


    #region UNITY-METODOS
    public void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExampleEnum.STATE_ONE, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.STATE_TWO, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.STATE_THREE, new StateBase());
    }
    #endregion
}
