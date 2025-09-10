using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    #region VARIAVEIS
    public float dist = .2f;
    public float coinSpeed = 3f;
    #endregion


    #region METODOS

    #endregion


    #region UNITY-METODOS
    private void Update()
    {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) > dist)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * coinSpeed);
        }
    }
    #endregion
}
