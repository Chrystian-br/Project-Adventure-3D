using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        #region VARIAVEIS
        public GunBase gunBase;
        #endregion


        #region METODOS
        protected override void Init()
        {
            base.Init();

            gunBase.StartShoot();
            PlayAnimationByTrigger(Animation.AnimationType.ATTACK);
        }
        #endregion


        #region UNITY-METODOS
        
        
        #endregion
    }
}

