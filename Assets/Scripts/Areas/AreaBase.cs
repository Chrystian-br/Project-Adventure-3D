using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBase : MonoBehaviour
{
    #region VARIAVEIS
    public GameObject boss;
    public bool lookAtPlayer = false;
    #endregion


    #region METODOS
    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            boss.SetActive(true);
            lookAtPlayer = true;
        }
    }

    public void OnTriggerStay(Collider col)
    {
        lookAtPlayer = true;
    }

    public void OnTriggerExit(Collider col)
    {
        lookAtPlayer = false;
    }
    #endregion


    #region UNITY-METODOS
    #endregion
}
