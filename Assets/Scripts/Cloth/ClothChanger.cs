using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        #region VARIAVEIS
        public List<SkinnedMeshRenderer> meshRenderers;
        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;
        #endregion


        #region METODOS
        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            for (var i = 0; i < meshRenderers.Count; i++)
            {
                meshRenderers[i].materials[0].SetTexture(shaderIdName, texture); 
            }
        }

        public void ChangeTexture(ClothSetup setup)
        {
            for (var i = 0; i < meshRenderers.Count; i++)
            {
                meshRenderers[i].materials[0].SetTexture(shaderIdName, setup.texture); 
            }
        }

        public void ResetTexture()
        {
            for (var i = 0; i < meshRenderers.Count; i++)
            {
                meshRenderers[i].materials[0].SetTexture(shaderIdName, _defaultTexture);
            }
        }
        #endregion


        #region UNITY-METODOS
        private void Awake()
        {
            _defaultTexture = (Texture2D)meshRenderers[0].materials[0].GetTexture(shaderIdName);
        }
        #endregion
    }
}
