using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractController : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private Tilemap map;
    [SerializeField] private TileBase change;
    [SerializeField] private Grid grid;
    private new AudioSource[] audio;

    [SerializeField] private Inventory inventory;

    CharacterController2D gnome;
    private GameObject interaction;
    public Transform boxHolder;

    private void Awake()
    {
        audio = GetComponents<AudioSource>();
        inventory = new Inventory();
        if(uiInventory != null)
            uiInventory.SetInventory(inventory);
        gnome = GetComponentInParent<CharacterController2D>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.getItems().Count != 0)
        //{
        //    Vector3Int pos = grid.WorldToCell(this.transform.position);
        //    pos.x++;
        //    pos.y--;
        //    map.SetTile(pos, change);
        //}
        if (interaction)
        {
            //CharacterController2D gnome = GetComponentInParent<CharacterController2D>();
            if (Input.GetKey(KeyCode.E))
            {
                interaction.transform.parent = gnome.transform;
                gnome.canFlip = false;

                // var pushDir = new Vector3(hit.moveDirection.x, 0, 0);
                // hit.collider.attachedRigidbody.velocity = pushDir * pushPower;
                interaction.transform.parent = boxHolder;
                interaction.transform.position = boxHolder.position;
            }
            else
            {
                interaction.transform.parent = null;
                interaction = null;
                gnome.canFlip = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.getItems().Count != 0)
        {
            Item toUse = inventory.getItems()[0];
            switch (toUse.itemType)
            {
                case Item.ItemType.Spade:
                    audio[1].Play();
                    Vector3Int pos = grid.WorldToCell(this.transform.position);
                    pos.x++;
                    pos.y--;
                    map.DeleteCells(pos, 1, 1, 1);
                    break;
                case Item.ItemType.WateringCan:
                    // ITEM LOGIC
                    break;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.getItems().Count != 0)
        {
            Item toUse = inventory.getItems()[1];
            switch (toUse.itemType)
            {
                case Item.ItemType.Spade:
                    audio[1].Play();
                    Vector3Int pos = grid.WorldToCell(this.transform.position);
                    pos.x++;
                    pos.y--;
                    map.DeleteCells(pos, 1, 1, 1);
                    break;
                case Item.ItemType.WateringCan:
                    // ITEM LOGIC
                    break;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && inventory.getItems().Count != 0)
        {
            Item toUse = inventory.getItems()[2];
            switch (toUse.itemType)
            {
                case Item.ItemType.Spade:
                    audio[1].Play();
                    Vector3Int pos = grid.WorldToCell(this.transform.position);
                    pos.x++;
                    pos.y--;
                    map.DeleteCells(pos, 1, 1, 1);
                    break;
                case Item.ItemType.WateringCan:
                    // ITEM LOGIC
                    break;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.getItems().Count != 0)
        {
            Item toUse = inventory.getItems()[1];
            switch (toUse.itemType)
            {
                case Item.ItemType.Spade:
                    audio[1].Play();
                    Vector3Int pos = grid.WorldToCell(this.transform.position);
                    pos.x++;
                    pos.y--;
                    map.DeleteCells(pos, 1, 1, 1);
                    break;
                case Item.ItemType.WateringCan:
                    // ITEM LOGIC
                    break;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("At Enter Collision");
        Item item = new Item();
        /*if (collision.CompareTag("Spade"))
        {
            audio[0].Play();
            item.itemType = Item.ItemType.Spade;
            Destroy(collision.gameObject);
            Debug.Log("Collided with item");
            inventory.addItem(item);
        }
*/
        switch (collision.tag)
        {
            case "Spade":
                audio[0].Play();
                item.itemType = Item.ItemType.Spade;
                Destroy(collision.gameObject);
                inventory.addItem(item);
                break;
            case "WateringCan":
                //audio[0].Play();
                Debug.Log("WATERINGCAN");
                item.itemType = Item.ItemType.WateringCan;
                Destroy(collision.gameObject);
                inventory.addItem(item);
                break;
            case "Moveable":
                Debug.Log("At box collision");
                interaction = collision.gameObject;
                if (interaction.GetComponent<Rigidbody>() == null) return;
                break;
        }
    }
}
