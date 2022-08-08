using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenButtonBehavior : MonoBehaviour
{
    [SerializeField] private Sprite notPressed;
    [SerializeField] private Sprite pressed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool isActivated = false;
    [SerializeField] private AudioSource source;
    private bool canPress = false;

    void Update()
    {
        if (canPress && Input.GetKeyUp(KeyCode.E))
        {
            source.Play();
            if (isActivated)
            {
                isActivated = false;
                spriteRenderer.sprite = notPressed;
            }
            else
            {
                isActivated = true;
                spriteRenderer.sprite = pressed;
            }
        } //end if
    }
    //if player, button can be pressed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPress = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPress = false;
        }
    }
    public void forceTrigger()
    {
        source.Play();
        if (isActivated)
        {
            isActivated = false;
            spriteRenderer.sprite = notPressed;
        }
        else
        {
            isActivated = true;
            spriteRenderer.sprite = pressed;
        }
    }
}
