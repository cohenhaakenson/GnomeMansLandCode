using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : MonoBehaviour
{
    public SItem scriptableObjectRep;
    private SpriteRenderer render;

    private void Awake()
    {
        render = this.GetComponent<SpriteRenderer>();
        render.sprite = scriptableObjectRep.sprite;
    }
}
