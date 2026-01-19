using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class Inventory
    {
        private List<Slot> _slots;
        private readonly int _startSize;
        public int CurrentSize => _slots.Sum(cell => cell.Amount);
        public IReadOnlyList<Slot> Items => _slots;

        public Inventory(List<(Item, int)> startItems, int maxSize)
        {
            _startSize = maxSize;

            _slots = new List<Slot>(_startSize);

            for (int i = 0; i < maxSize; i++)
                _slots.Add(new Slot());

            if (startItems.Count > maxSize)
                throw new ArgumentOutOfRangeException($"Items count ({startItems.Count}) " +
                    $"higher than inventory max size ({maxSize}).");

            for (int i = 0; i < startItems.Count; i++)
            {
                Slot slot = _slots[i];

                if (slot.CanModify(startItems[i].Item1, startItems[i].Item2))
                    slot.AddItem(startItems[i].Item1, startItems[i].Item2);
            }
        }

        public bool CanAddItem(Item item, int amount)
        {
            foreach (var slot in _slots)
                if (slot.CanModify(item, amount))
                    return true;

            return false;
        }

        public int AddItem(Item item, int amountToAdd)
        {
            if (item == null)
                throw new ArgumentNullException("Item cannot be null.");

            if (amountToAdd <= 0)
                throw new ArgumentOutOfRangeException("Item amount cannot be zero or below.");

            int remaining = amountToAdd;

            foreach (var slot in _slots)
            {
                if (remaining <= 0) break;

                if (slot.Item != null && slot.Item.Name == item.Name)
                {
                    int spaceInSlot = Slot.ItemStack - slot.Amount;
                    if (spaceInSlot > 0)
                    {
                        int toAdd = Math.Min(remaining, spaceInSlot);
                        slot.AddItem(item, toAdd);
                        remaining -= toAdd;
                    }
                }
            }

            foreach (var slot in _slots)
            {
                if (remaining <= 0) break;

                if (slot.Item == null)
                {
                    int toAdd = Math.Min(remaining, Slot.ItemStack);
                    slot.AddItem(item, toAdd);
                    remaining -= toAdd;
                }
            }

            return remaining;
        }

        public bool RemoveItemsByName(string name, int amountToRemove)
        {
            if (amountToRemove <= 0)
                throw new ArgumentOutOfRangeException("Item amount cannot be zero or below.");

            var slotsWithItem = GetSlotsWithItemByName(name);
            if (slotsWithItem.Count == 0)
                return false;

            int remainingToRemove = amountToRemove;

            foreach (var slot in slotsWithItem)
            {
                if (remainingToRemove <= 0)
                    break;

                if (slot.Amount <= remainingToRemove)
                {
                    remainingToRemove -= slot.Amount;
                    slot.ClearSlot();
                }
                else
                {
                    slot.RemoveItem(remainingToRemove);
                    remainingToRemove = 0;
                }
            }

            return true;
        }

        public IReadOnlyList<Slot> GetSlotsWithItemByName(string name)
        {
            if (_slots == null)
                throw new InvalidOperationException("Slots List not initialized.");

            List<Slot> slotsToReturn = new List<Slot>();

            foreach (Slot slot in _slots)
            {
                if (slot.Item?.Name == name)
                    slotsToReturn.Add(slot);
            }

            return slotsToReturn;
        }

        public IReadOnlyList<(Item, int)> GetAllItems()
        {
            List<(Item, int)> items = new();

            foreach (var slot in _slots)
                if (slot.Item != null)
                    items.Add((slot.Item, slot.Amount));

            return items;
        }
    }

    public class Slot
    {
        public const int ItemStack = 64;
        public Item Item { get; private set; }
        public int Amount { get; private set; }

        public bool CanModify(Item item, int amount)
        {
            if (item == null)
                throw new ArgumentNullException("Item cannot be null.");

            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Item amount cannot be zero or below.");

            if (Item != null && Item.Name != item.Name)
                return false;

            return true;
        }

        public void AddItem(Item item, int amount)
        {
            if (CanModify(item, amount) == false)
                throw new InvalidOperationException("You are trying to modify an item to a slot that " +
                    "cannot add an item of that type or in that quantity.");

            if (Item != null && Item.Name == item.Name)
            {
                Amount += amount;
            }
            else if (Item == null && item != null)
            {
                Item = item;
                Amount = amount;
            }
            else
            {
                throw new InvalidOperationException("Unexpected exception while adding item.");
            }

            Debug.Log($"{item.Name} added to inventory.");
        }

        public void RemoveItem(int amount)
        {
            if (CanModify(Item, amount))
            {
                if (Amount - amount <= 0)
                    ClearSlot();
                else
                    Amount -= amount;
            }
        }

        public void ClearSlot()
        {
            Item = null;
            Amount = 0;
        }
    }

    public class Item
    {
        public string Name { get; private set; }
        public Item(string name) => Name = name;
    }
}