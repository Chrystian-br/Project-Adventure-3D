using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Items
{
    public class CollectableBase : MonoBehaviour
    {
        #region VARIAVEIS
        [Header("Item")]
        public ItemType itemType;
        public int amount = 1;

        [Header("Tag")]
        public string compareTag = "Player";

        [Header("VFX")]
        public float animationCollectDelay = 0.5f;
        public ParticleSystem pSystem;

        [Header("Sounds")]
        public AudioSource audioSource;

        #endregion


        #region METODOS
        protected virtual void Collect()
        {
            OnCollect();
            Destroy(gameObject);
        }

        protected virtual void OnCollect()
        {
            if (pSystem != null) pSystem.Play();
            if (audioSource != null) audioSource.Play();

            ItemsManager.Instance.AddItemByType(itemType, amount);
        }
        #endregion


        #region UNITY-METODOS
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Debug.Log(collision);
                gameObject.transform.GetComponent<SphereCollider>().enabled = false;
                gameObject.transform.DOScale(0, animationCollectDelay);
                //gameObject.transform.DOLocalMoveX(collision.transform.position.x, animationCollectDelay);

                Invoke(nameof(Collect), animationCollectDelay - 0.2f);
            }
        }

        private void Awake()
        {
            /*if(pSystem != null){
                pSystem.transform.SetParent(null);
                pSystem.collision.AddPlane(floor.transform);
            }*/
        }
        #endregion
    }
}
