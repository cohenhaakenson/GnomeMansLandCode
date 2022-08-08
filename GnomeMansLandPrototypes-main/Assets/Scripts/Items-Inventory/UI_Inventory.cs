using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlot;

    private void Awake()
    {
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChange += Inventory_OnItemListChanged;
        RefreshInventory();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        int i = 1;
        foreach (Item item in inventory.getItems())
        {
            itemSlot = transform.Find("Inventory" + i.ToString());
            itemSlotContainer = itemSlot.Find("ItemSlotContainer");
            //set image
            Image image = itemSlotContainer.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            itemSlotContainer.gameObject.SetActive(true);
            i++;
        }

    }

    public void UseItemInSlot(int slot)
    {
        inventory.UseItemInSlot(slot);
    }


}
