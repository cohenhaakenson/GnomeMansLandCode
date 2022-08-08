using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Item 
{
    public enum ItemType
    {
        Spade, 
        WateringCan,
        Hammer
    }

    [SerializeField]
    public ItemType itemType;

    public bool pickedup = false;
    public int index;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Spade: return ItemAssets.Instance.SpadeSprite;
            case ItemType.WateringCan: return ItemAssets.Instance.WateringCanSprite;
            case ItemType.Hammer: return ItemAssets.Instance.HammerSprite;
        }
    }

    public void pickUp()
    {
        pickedup = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        StopUsing();
    }
}
