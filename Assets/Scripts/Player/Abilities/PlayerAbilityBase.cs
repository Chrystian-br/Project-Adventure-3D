using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    #region VARIAVEIS
        protected Player player;

        protected Inputs inputs;
    #endregion


    #region METODOS
        protected virtual void Init()
        {

        }

        protected virtual void RegisterListeners()
        {
            
        }

        protected virtual void RemoveListeners()
        {

        }

    #endregion


    #region UNITY-METODOS
        private void Start()
        {
            inputs = new Inputs();
            inputs.Enable();

            OnValidate();
            Init();
            RegisterListeners();
        }
        
        private void OnValidate()
        {
            if(player == null) player = GetComponent<Player>();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }


        public void OnEnable()
        {
            if(inputs != null) inputs.Enable();
        }

        public void OnDisable()
        {
            inputs.Disable();
        }
    #endregion
}
