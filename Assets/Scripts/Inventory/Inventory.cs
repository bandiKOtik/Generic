using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class Inventory
    {
        public IReadOnlyList<Slot> Items => _slots;
        private readonly int startSize;

        private List<Slot> _slots;
        public int CurrentSize => _slots.Sum(cell => cell.Amount);


        public Inventory(List<(Item, int)> startItems, int maxSize)
        {
            startSize = maxSize;

            _slots = new List<Slot>(startSize);

            for (int i = 0; i < maxSize; i++)
                _slots.Add(new Slot());

            if (startItems.Count > maxSize)
                throw new ArgumentOutOfRangeException($"Items count ({startItems.Count}) " +
                    $"higher than inventory max size ({maxSize}).");

            for (int i = 0; i < startItems.Count; i++)
            {
                Slot slot = _slots[i];

                if (slot.CanAdd(startItems[i].Item1, startItems[i].Item2))
                    slot.AddItem(startItems[i].Item1, startItems[i].Item2);
            }
        }

        public bool CanAddItem(Item item, int amount)
        {
            foreach (var slot in _slots)
                if (slot.CanAdd(item, amount))
                    return true;

            return false;
        }

        public bool AddItem(Item item, int amountToAdd)
        {
            if (amountToAdd <= 0 || item == null)
                return false;

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

            return remaining == 0;
        }

        public bool RemoveItemsByName(string name, int amountToRemove)
        {
            if (amountToRemove <= 0)
                return false;

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

        public List<Slot> GetSlotsWithItemByName(string name)
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

        public List<(Item, int)> GetAllItems()
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

        public bool CanAdd(Item item, int amount)
        {
            if (amount <= 0)
                return false;

            if (Item != null && item != null && Item.Name != item.Name)
                return false;

            if (Amount + amount > ItemStack)
                return false;

            return true;
        }

        public void AddItem(Item item, int amount)
        {
            if (CanAdd(item, amount) == false)
                throw new InvalidOperationException("You are trying to add an item to a slot that " +
                    "cannot add an item of that type or in that quantity.");

            if (Item != null && item != null && Item.Name == item.Name)
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
            if (Item == null)
                throw new ArgumentException("There is no items to remove.");

            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Amount value cannot be zero or below.");

            if (Amount - amount <= 0)
                ClearSlot();
            else
                Amount -= amount;
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