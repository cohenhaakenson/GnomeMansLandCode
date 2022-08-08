using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotes : MonoBehaviour
{
    [SerializeField]
    private Transform emotes;
    private Transform emote;
    private int emoteTime = 120;
    private int timer = 0;
    private bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        emoteActive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            Debug.Log("EMOTE HIT");
            emote = emotes.GetChild(0);
            emote.gameObject.SetActive(true);
            isActive = true;
        }

        if (collision.CompareTag("Spade") && collision.gameObject.transform.parent == null)
        {
            emote = emotes.GetChild(1);
            emote.gameObject.SetActive(true);
            isActive = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            emote = emotes.GetChild(0);
            emote.gameObject.SetActive(true);
            isActive = true;
        }

        if (collision.CompareTag("Spade") && collision.gameObject.transform.parent == null)
        {
            emote = emotes.GetChild(1);
            emote.gameObject.SetActive(true);
            isActive = true;
        }
    }

    private void emoteActive()
    {
        if (emote != null)
        {
            if (isActive && timer <= emoteTime)
            {
                timer++;
            }
            else
            {
                emote.gameObject.SetActive(false);
                isActive = false;
                emote = null;
                timer = 0;
            }
        }
    }
}
