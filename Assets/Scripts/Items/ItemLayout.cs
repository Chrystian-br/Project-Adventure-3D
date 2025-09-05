using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        #region VARIAVEIS
        private ItemSetup _currSetup;

        public Image uiIcon;
        public TextMeshProUGUI uiValue;
        #endregion


        #region METODOS
        public void Load(ItemSetup setup)
        {
            _currSetup = setup;
            UpdateUI();
        }

        public void UpdateUI()
        {
            uiIcon.sprite = _currSetup.icon;
        }
        #endregion


        #region UNITY-METODOS
        public void Update()
        {
            uiValue.text = _currSetup.soInt.count.ToString();
        }
        #endregion
    }
}

