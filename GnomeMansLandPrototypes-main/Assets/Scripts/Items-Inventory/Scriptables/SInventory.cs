using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName = "Inventory", fileName = "New Inventory")]
[System.Serializable]
public class SInventory : ScriptableObject
{

    public SItemInstance[] inventory;
    public event EventHandler OnItemListChange;

    public bool SlotEmpty(int index)
    {
        if (inventory[index] == null)
        {
            return true;
        }
        return false;
    }

    public bool getItem(int index, out SItemInstance item)
    {
        if (SlotEmpty(index))
        {
            item = null;
            return false;
        }
        item = inventory[index];
        return true;
    }

    public bool RemoveItem(int index)
    {
        if (SlotEmpty(index))
        {
            return false;
        }
        inventory[index] = null;
        OnItemListChange?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public int AddItem(SItemInstance item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (SlotEmpty(i))
            {
                inventory[i] = item;
                OnItemListChange?.Invoke(this, EventArgs.Empty);
                return i;
            }
        }
        return -1;
    }

    public SItemInstance[] getItems()
    {
        return inventory;
    }

}