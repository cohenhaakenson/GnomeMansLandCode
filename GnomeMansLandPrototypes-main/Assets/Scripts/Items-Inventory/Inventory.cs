using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{

    private List<Item> itemList;
    public event EventHandler OnItemListChange;

    public Inventory()
    {
        itemList = new List<Item>();

    }

    public void addItem(Item item)
    {
        itemList.Add(item);
        item.pickUp();
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> getItems()
    {
        return itemList;
    }

    public void UseItemInSlot(int slot)
    {
        if (slot < itemList.Count)
        {
            if (itemList[slot] != null)
            {
                itemList[slot].useItem();
            }
        }
    }
}
