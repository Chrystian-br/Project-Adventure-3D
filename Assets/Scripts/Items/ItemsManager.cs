using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RysCorp.Core.Singleton;

namespace Items
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK,
        KILLS,
        MUNITION
    }

    public class ItemsManager : Singleton<ItemsManager>
    {
        #region VARIAVEIS
        public List<ItemSetup> itemSetups;

        public GameObject inventory;
        public GameObject baseUI;

        public int initialMunition = 5;
        #endregion


        #region METODOS
        private void Reset()
        {
            foreach (var i in itemSetups)
            {
                i.soInt.count = 0;
            }

            itemSetups.Find(i => i.itemType == ItemType.MUNITION).soInt.count = initialMunition;
        }

        public void AddItemByType(ItemType itemType, int amount = 1)
        {
            itemSetups.Find(i => i.itemType == itemType).soInt.count += amount;
        }

        public void RemoveItemByType(ItemType itemType, int amount = -1)
        {
            if (amount > 0) return;
            var item = itemSetups.Find(i => i.itemType == itemType);

            item.soInt.count += amount;

            if (item.soInt.count < 0) item.soInt.count = 0;

            UpdateUI();
        }

        private void UpdateUI()
        {
            foreach (var i in itemSetups)
            {
                i.itemText.text = i.soInt.count.ToString();
            }
        }


        public void OpenInventory()
        {
            Debug.Log("Abriu");
            inventory.SetActive(true);
            baseUI.SetActive(false);
        }

        public void CloseInventory()
        {
            inventory.SetActive(false);
            baseUI.SetActive(true);
        }
        #endregion


        #region UNITY-METODOS
        private void Start()
        {
            Reset();
            inventory.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!inventory.activeSelf)
                {
                    OpenInventory();
                }
                else
                {
                    CloseInventory();
                }
            }

            UpdateUI();
        }
        #endregion
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
        public TextMeshProUGUI itemText;
    }
}