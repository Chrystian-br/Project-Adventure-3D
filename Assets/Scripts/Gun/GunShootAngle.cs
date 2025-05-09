using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    #region VARIAVEIS
        public int amountPerShoot = 4;
        public float angle = 15f;
    #endregion


    #region METODOS
    public override void Shoot()
    {
        int mult = 0;

        for(int i = 0; i < amountPerShoot; i++)
        {
            if(i%2 == 0)
            {
                mult++;
            }
            Debug.Log("atirou");
            var projectile = Instantiate(prefabProjectile, positionToShoot);
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i%2 == 0 ? angle : -angle) * mult;

            projectile.speed = speed;
            projectile.transform.parent = null;
        }
    }
    #endregion
     
     
    #region UNITY-METODOS
     
    #endregion
}
