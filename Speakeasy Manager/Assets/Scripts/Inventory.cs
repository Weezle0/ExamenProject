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
    public void AddItem(int id, int amount)
    {
        bool newitem = true;
        foreach (var item in items)
        {
            if(item.itemID == id)
            {
                item.amount += amount;
                newitem = false;
            }

        }
        if(newitem)
        {
            ItemSlot tempItem = new();
            tempItem.itemID = id;
            tempItem.amount = amount;
            items.Add(tempItem);
        }
        
    }
    public void InsertItemAtTop(ItemSlot itemToInsert)
    {
        items.Insert(0, itemToInsert);
    }
    public void RemoveItem(int id, int amount)
    {
        foreach (var item in items)
        {
            if (item.itemID == id)
            {
                item.amount -= amount;
                if(item.amount <= 0)
                {
                    items.Clear();
                }
                return;
            }
        }
    }
    public ResourceData GetItemData(int id)
    {
        return resourceManager.resources[items[id].itemID];
    }
}

[Serializable]
public class ItemSlot
{
    public int itemID;
    public int amount;
}