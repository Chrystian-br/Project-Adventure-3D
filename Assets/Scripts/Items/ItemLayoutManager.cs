using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLayoutManager : MonoBehaviour
    {
        #region VARIAVEIS
        public ItemLayout prefabLayout;
        public Transform container;

        public List<ItemLayout> itemLayouts;
        #endregion


        #region METODOS
        private void CreateItems()
        {
            foreach (var setup in ItemsManager.Instance.itemSetups)
            {
                var item = Instantiate(prefabLayout, container);
                item.Load(setup);
                itemLayouts.Add(item);
            }
        }
        #endregion


        #region UNITY-METODOS
        private void Start()
        {
            CreateItems();
        }
        #endregion
    }

}
