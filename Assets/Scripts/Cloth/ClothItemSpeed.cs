using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{

    public class ClothItemSpeed : ClothItemBase
    {
        #region VARIAVEIS
        public float targetSpeed = 2f;
        #endregion


        #region METODOS
        protected override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeSpeed(targetSpeed, duration);
        }
        #endregion
         
         
        #region UNITY-METODOS
         
        #endregion
    }
}
