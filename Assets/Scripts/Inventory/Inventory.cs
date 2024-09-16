using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler<Item> OnItemListChanged;

    private List<Item> items;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        items = new List<Item>();

        // AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 2 });
        // AddItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
        // AddItem(new Item { itemType = Item.ItemType.Coin, amount = 3 });
    }

    public void AddItem(Item item, bool isNewItem = true)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;

            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }

        if (isNewItem)
        {
            OnItemListChanged?.Invoke(this, item);
        }
    }


    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;

            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                items.Remove(itemInInventory);
            }
        }
        else
        {
            items.Remove(item);
        }

        OnItemListChanged?.Invoke(this, item);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return items;
    }

}
