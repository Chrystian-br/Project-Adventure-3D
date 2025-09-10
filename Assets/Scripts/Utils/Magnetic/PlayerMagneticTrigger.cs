using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class PlayerMagneticTrigger : MonoBehaviour
{
    #region VARIAVEIS

    #endregion


    #region METODOS

    #endregion


    #region UNITY-METODOS
    private void OnTriggerEnter(Collider other)
    {
        CollectableBase i = other.transform.GetComponent<CollectableBase>();

        if (i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
            i.gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }
    #endregion
}
