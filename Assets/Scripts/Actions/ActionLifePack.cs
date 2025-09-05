using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{

    public class ActionLifePack : MonoBehaviour
    {
        #region VARIAVEIS
        public KeyCode keyCode = KeyCode.X;
        public SOInt lifePackAmount;
        public Color recoverColor = Color.green;

        public int amount = 10;
        #endregion


        #region METODOS
        public void UseLifePack()
        {
            if (lifePackAmount.count > 0)
            {
                lifePackAmount.count--;
                Player.Instance.healthBase.RecoverLife(amount);
                Player.Instance.healthBase.UpdateUI();

                Player.Instance.flashColors.ForEach(i => i.Flash(recoverColor));
            }
        }
        #endregion


        #region UNITY-METODOS
        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                UseLifePack();
            }
        }
        #endregion
    }

}
