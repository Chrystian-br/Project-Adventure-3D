using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{

    public class ClothItemStrong : ClothItemBase
    {
        #region VARIAVEIS
        public float damageMultiplier = .5f;
        #endregion


        #region METODOS
        protected override void Collect()
        {
            base.Collect();
            Player.Instance.healthBase.ChangeDamageMultiplier(damageMultiplier, duration);
        }
        #endregion
         
         
        #region UNITY-METODOS
         
        #endregion
    }
}
