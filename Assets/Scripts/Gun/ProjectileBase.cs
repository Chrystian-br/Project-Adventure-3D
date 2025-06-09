using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    #region VARIAVEIS
        public float TimeToDestroy = 2f;

        public int damageAmount = 1;
        public float speed = 50f;

        public List<string> tagsToHit;
    #endregion


    #region METODOS
    public void OnCollisionEnter(Collision collision)
    {
        foreach (var t in tagsToHit)
        {
            if (collision.transform.tag == t)
            {
                var damageable = collision.transform.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageable.Damage(damageAmount, dir);

                    Destroy(gameObject);
                }

                break;
            }
        }
        

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
