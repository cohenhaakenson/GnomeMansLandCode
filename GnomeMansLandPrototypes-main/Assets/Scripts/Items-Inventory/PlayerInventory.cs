using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInventory : MonoBehaviour
{
    
    [SerializeField] private UI_Inventory uiInventory;
    private new AudioSource[] audio;

    [SerializeField] private Inventory inventory;

    private void Awake()
    {
        audio = GetComponents<AudioSource>();
        inventory = new Inventory();
        if(uiInventory != null)
            uiInventory.SetInventory(inventory);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Item item;
        item = collision.gameObject.GetComponent<Item>();
        
        if (item != null && !inventory.getItems().Contains(item))
        {
            audio[0].Play();
            inventory.addItem(item);
            item.index = inventory.getItems().Count;
            collision.gameObject.transform.localPosition = this.transform.parent.localPosition;
            //set transparent or inactive
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.transform.parent = this.transform.parent;

            Vector3 rot = collision.transform.localScale;

            rot.x *= transform.localScale.x;
            collision.transform.localScale = rot;
        }
        
    }
}
