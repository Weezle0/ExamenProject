using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Inventory
{

    [SerializeField] public List<ItemSlot> items = new();
    ResourceManager resourceManager;

    public Inventory()
    {
        resourceManager = ResourceManager.instance;
    }
    // add item
    public void AddItem(int id, int amount)
    {
        bool newitem = true;
        // check if there already is an item of the same type
        foreach (var item in items)
        {
            if(item.itemID == id)
            {
                // if there is a simlilar item add more of the amount to the excisting item
                item.amount += amount;
                newitem = false;
            }

        }
        if(newitem)
        {
            // if no simular item create a new item with the amount added
            ItemSlot tempItem = new();
            tempItem.itemID = id;
            tempItem.amount = amount;
            items.Add(tempItem);
        }
        
    }
    public void InsertItemAtTop(ItemSlot itemToInsert)
    {
        // insert the item at the top of the inventory
        items.Insert(0, itemToInsert);
    }
    public void RemoveItem(int id, int amount)
    {
        // check if the inventory has the item to remove
        foreach (var item in items)
        {
            if (item.itemID == id)
            {
                // if it has the item remove it
                item.amount -= amount;
                if(item.amount <= 0)
                {
                    // if the item has no amount remove it from the list
                    items.Remove(item);
                }
                return;
            }
        }
    }
    public ResourceData GetItemData(int id)
    {
        // return the ID of the item requested
        return resourceManager.resources[items[id].itemID];
    }
}

[Serializable]
public class ItemSlot
{
    public int itemID;
    public int amount;
}