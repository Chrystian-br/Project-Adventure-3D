using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RysCorp.Core.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region VARIAVEIS
            public static T Instance;
        #endregion
        
        
        #region METODOS
        
        #endregion
        
        
        #region UNITY-METODOS
            private void Awake()
            {
                if(Instance == null){
                    Instance = GetComponent<T>();
                } else {
                    Destroy(gameObject);
                }
            }
        #endregion
    }
}


