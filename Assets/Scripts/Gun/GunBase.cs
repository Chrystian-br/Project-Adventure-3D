using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    #region VARIAVEIS
        public ProjectileBase prefabProjectile;

        public Transform positionToShoot;
        public float timeBetweenShoot = .3f;
        public float speed = 50;

        private Coroutine _currentCoroutine;
    #endregion


    #region METODOS
        protected virtual IEnumerator ShootCoroutine()
        {
            while(true)
            {
                Shoot();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }

        public virtual void Shoot()
        {
            var projectile = Instantiate(prefabProjectile);
            projectile.transform.position = positionToShoot.position;
            projectile.transform.rotation = positionToShoot.rotation;
            projectile.speed = speed;
        }

        public void StartShoot()
        {
            StopShoot();
            _currentCoroutine = StartCoroutine(ShootCoroutine());
        }

        public void StopShoot()
        {
            if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        }
    #endregion


    #region UNITY-METODOS
    #endregion
}
