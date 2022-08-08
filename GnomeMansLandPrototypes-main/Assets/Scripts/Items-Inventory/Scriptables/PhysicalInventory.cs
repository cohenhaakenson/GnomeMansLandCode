using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhysicalInventory : MonoBehaviour
{
    public SInventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlot;


    private void Awake()
    {
    }

    public void SetInventory(SInventory inventory)
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
        foreach (SItemInstance item in inventory.getItems())
        {
            itemSlot = transform.Find("Inventory" + i.ToString());
            itemSlotContainer = itemSlot.Find("ItemSlotContainer");
            //set image
            Image image = itemSlotContainer.Find("Image").GetComponent<Image>();
            image.sprite = item.item.sprite;
            itemSlotContainer.gameObject.SetActive(true);
            i++;
        }

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Debug.Log("Collided with item");
            SItemInstance item = new SItemInstance(item: collision.gameObject.GetComponent<PhysicalItem>().scriptableObjectRep);
            Destroy(collision.gameObject);
            Debug.Log(item);
            inventory.AddItem(item);
        }


        // ATTEMPTING TO ADD SCRIPTABLE IMPLEMENTATION
        *//*if (collision.CompareTag("Item"))
        {
            ScriptableItem something = collision.gameObject;
        }*//*

    }*/
}
