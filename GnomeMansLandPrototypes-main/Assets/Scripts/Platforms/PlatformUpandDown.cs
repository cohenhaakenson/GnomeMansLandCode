using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpandDown : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float distance;
    [SerializeField] private bool goUpFirst;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        if (goUpFirst)
        {
            Vector2 temp = transform.position;
            temp.y += distance;
            startPos = temp;
        }
        else
        {
            Vector2 temp = transform.position;
            temp.y -= distance;
            startPos = temp;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goUpFirst)
        {
            transform.Translate(0, speed * 1f * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, speed * -1f * Time.deltaTime, 0);
        }

        if (goUpFirst)
        {
            if (transform.position.y > (startPos.y + distance))
            {
                goUpFirst = !goUpFirst;
            }
        }
        else
        {
            if (transform.position.y < (startPos.y - distance))
            {
                goUpFirst = !goUpFirst;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !goUpFirst)
        {
            goUpFirst = !goUpFirst;
        }
    }
}
