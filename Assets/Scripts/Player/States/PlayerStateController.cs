using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RysCorp.StateMachine;

public class PlayerStateController : MonoBehaviour
{
    #region VARIAVEIS
        public enum PlayerStates
        {
            IDLE,
            WALKING,
            RUNNING,
            DEAD
        }

        public StateMachine<PlayerStates> stateMachine;

        public Animator animator;
    #endregion
     
     
    #region METODOS
        public void Init()
        {
            stateMachine = new StateMachine<PlayerStates>();
            stateMachine.Init();

            stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerStateIdle());
            stateMachine.RegisterStates(PlayerStates.WALKING, new PlayerStateWalking());
            stateMachine.RegisterStates(PlayerStates.RUNNING, new PlayerStateRunning());
            stateMachine.RegisterStates(PlayerStates.DEAD, new PlayerStateDead());

            stateMachine.SwitchState(PlayerStates.IDLE);
        }
    #endregion


    #region UNITY-METODOS
        public void Start()
        {
            Init();
        }

        public void Update()
        {
            
        }
    #endregion
}
