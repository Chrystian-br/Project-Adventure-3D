using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    #region VARIAVEIS

    #endregion


    #region METODOS
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    #endregion


    #region UNITY-METODOS

    #endregion
}
