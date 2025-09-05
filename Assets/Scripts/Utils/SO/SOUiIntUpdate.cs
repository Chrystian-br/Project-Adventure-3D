using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SOUiIntUpdate : MonoBehaviour
{
    #region VARIAVEIS
        public SOInt SOInt;
        public TextMeshProUGUI uiText;
    #endregion
     
     
    #region METODOS
     
    #endregion
     
     
    #region UNITY-METODOS
        private void Start()
        {
            uiText.text = SOInt.count.ToString();
        }

        private void Update()
        {
            uiText.text = SOInt.count.ToString();
        }
    #endregion
}
