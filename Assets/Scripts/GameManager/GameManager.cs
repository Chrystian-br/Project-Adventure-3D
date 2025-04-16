using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RysCorp.Core.Singleton;
using RysCorp.StateMachine;

public class GameManager : Singleton<GameManager>
{
    #region VARIAVEIS
        public enum GameStates
        {
            INTRO,
            GAMEPLAY,
            PAUSE,
            WIN,
            LOSE
        }

        public StateMachine<GameStates> stateMachine;
    #endregion


    #region METODOS
        public void Init()
        {
            stateMachine = new StateMachine<GameStates>();
            stateMachine.Init();

            stateMachine.RegisterStates(GameStates.INTRO, new GMStateIntro());
            stateMachine.RegisterStates(GameStates.GAMEPLAY, new StateBase());
            stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
            stateMachine.RegisterStates(GameStates.WIN, new StateBase());
            stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

            stateMachine.SwitchState(GameStates.INTRO);
        }

        public void InitGame()
        {

        }
    #endregion


    #region UNITY-METODOS
        public void Start()
        {
            Init();
        }
    #endregion
}
