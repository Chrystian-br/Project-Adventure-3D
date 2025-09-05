using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    #region VARIAVEIS
    public GameObject boss;
    public bool lookAtPlayer = false;

    public GameObject bossCamera;
    
    public Color gizmoColor = Color.yellow;
    public GameObject bossHealthBar;
    #endregion


    #region METODOS
    private void Awake()
    {
        TurnCameraOff();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            boss.SetActive(true);
            lookAtPlayer = true;

            TurnCameraOn();

            bossHealthBar.SetActive(true);
        }
    }

    public void OnTriggerStay(Collider col)
    {
        lookAtPlayer = true;
    }

    public void OnTriggerExit(Collider col)
    {
        lookAtPlayer = false;
        TurnCameraOff();
    }

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }

    private void TurnCameraOff()
    {
        bossCamera.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
    #endregion

}
