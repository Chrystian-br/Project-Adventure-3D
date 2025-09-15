using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        #region VARIAVEIS
        public ClothType clothType;

        public float duration = 2f;

        public string compareTag = "Player";
        #endregion


        #region METODOS
        protected virtual void Collect()
        {
            var setup = ClothManager.Instance.GetSetupByType(clothType);

            Player.Instance.ChangeTexture(setup, duration);

            Destroy(gameObject);
        }
        #endregion
         
         
        #region UNITY-METODOS
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }
        #endregion
    }
}

