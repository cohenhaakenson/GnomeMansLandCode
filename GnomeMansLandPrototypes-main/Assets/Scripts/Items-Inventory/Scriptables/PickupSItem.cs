using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSItem : MonoBehaviour
{

    public GameObject item;
    public SInventory inventory;

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {

        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            item = collision.gameObject;
            inventory.AddItem(new SItemInstance(item: item.GetComponent<PhysicalItem>().scriptableObjectRep));
            
            Destroy(collision.gameObject);
        }

    }

}
