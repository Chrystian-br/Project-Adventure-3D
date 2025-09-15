using System.Collections;
using System.Collections.Generic;
using RysCorp.Core.Singleton;
using UnityEngine;


namespace Cloth
{
    public enum ClothType
    {
        SPEED,
        IMORTAL,
        STRONG
    }

    public class ClothManager : Singleton<ClothManager>
    {
        #region VARIAVEIS
        public List<ClothSetup> clothSetups;
        #endregion


        #region METODOS
        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothType == clothType);
        }
        #endregion


        #region UNITY-METODOS

        #endregion
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}
