using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPopup : MonoBehaviour
{
    //[SerializeField] private Collider2D playerCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spriteRenderer.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spriteRenderer.enabled = false;
        }
    }
}
