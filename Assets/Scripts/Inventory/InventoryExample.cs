using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryExample : MonoBehaviour
    {
        [SerializeField] private InputField _nameField;
        [SerializeField] private InputField _amountField;
        [SerializeField] private Text _itemListText;

        private Inventory _inventory;

        void Start()
        {
            List<(Item, int)> startItems = new()
                {
                    (new Item("Ìוק"), 1),
                    (new Item("Çוכüו"), 34),
                    (new Item("Áמלבא"), 60),
                };

            _inventory = new Inventory(startItems, 8);

            UpdateItemsList();
        }

        public void AddItem()
        {
            if (int.TryParse(_amountField.text, out int value))
            {
                _inventory.AddItem(new Item(_nameField.text), value);
                UpdateItemsList();
            }
            else
            {
                throw new ArgumentException("Not a value.");
            }
        }

        public void RemoveItem()
        {
            if (int.TryParse(_amountField.text, out int value))
            {
                _inventory.RemoveItemsByName(_nameField.text, value);
                UpdateItemsList();
            }
            else
            {
                throw new ArgumentException("Not a value.");
            }
        }

        private void UpdateItemsList()
        {
            List<(Item, int)> newList = _inventory.GetAllItems();

            string summText = "Items List:";

            for (int i = 0; i < newList.Count; i++)
                summText += $"\n{newList[i].Item1.Name.ToString()} : {newList[i].Item2.ToString()}";

            _itemListText.text = summText;
        }
    }
}
