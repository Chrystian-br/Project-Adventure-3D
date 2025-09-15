using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{

    public class ClothItemImortal : ClothItemBase
    {
        #region VARIAVEIS
        public float damageMultiplier = 0f;
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
