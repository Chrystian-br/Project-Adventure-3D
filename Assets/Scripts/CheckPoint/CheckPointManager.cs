using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RysCorp.Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    #region VARIAVEIS
    public int lastCheckPointKey = 0;

    public List<CheckPointBase> checkPoints;
    #endregion


    #region METODOS
    public bool HasCheckpoint()
    {
        return lastCheckPointKey > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if (i > lastCheckPointKey)
        {
            lastCheckPointKey = i;
        }
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {
        var checkpoint = checkPoints.Find(i => i.key == lastCheckPointKey);

        return checkpoint.transform.position;
    }
    #endregion


    #region UNITY-METODOS

    #endregion
}
