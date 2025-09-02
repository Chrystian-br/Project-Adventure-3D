using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    #region VARIAVEIS
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActived = false;
    private string checkpointKey = "CheckPointKey";
    #endregion


    #region METODOS
    private void Init()
    {
        TurnItOff();
    }

    private void CheckCheckPoint()
    {
        TurnItOn();
        SaveCheckPoint();
    }

    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void SaveCheckPoint()
    {
        /*if (PlayerPrefs.GetInt(checkpointKey, 0) > key)
        {
            PlayerPrefs.SetInt(checkpointKey, key);
        }*/

        CheckPointManager.Instance.SaveCheckPoint(key);

        checkpointActived = true;
    }
    #endregion


    #region UNITY-METODOS
    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointActived && other.transform.tag == "Player") CheckCheckPoint();

        Debug.Log(other.transform.tag);
    }

    private void Awake()
    {
        Init();
    }
    #endregion
}
