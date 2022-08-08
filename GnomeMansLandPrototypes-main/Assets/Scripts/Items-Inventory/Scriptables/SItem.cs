using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class SItem : ScriptableObject
{
    public enum ItemType
    {
        Spade,
        WateringCan
    }

    public Sprite sprite;
    public new string name;
    public string description;
    public ItemType type;

}

[System.Serializable]
public class SItemInstance
{
    public SItem item;

    public SItemInstance(SItem item)
    {
        this.item = item;
    }
}