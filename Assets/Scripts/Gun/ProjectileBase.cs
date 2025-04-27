using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    #region VARIAVEIS
        public float TimeToDestroy = 2f;

        public int damageAmount = 1;
        public float speed = 50f;
    #endregion


    #region METODOS
        public void OnCollisionEnter(Collision collision)
        {
            
        }
    #endregion


    #region UNITY-METODOS
    public void Awake()
        {
            Destroy(gameObject, TimeToDestroy);
        }

        public void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    #endregion
}
