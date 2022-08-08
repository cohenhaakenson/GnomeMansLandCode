using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLeftandRight : MonoBehaviour
{

    [SerializeField] private int speed;
    [SerializeField] private float distance;
    [SerializeField] private bool goRightFirst;
    private Vector2 startPos;
    private bool PlayerTouching = false;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (goRightFirst)
        {
            Vector2 temp = transform.position;
            temp.x += distance;
            startPos = temp;
        }
        else
        {
            Vector2 temp = transform.position;
            temp.x -= distance;
            startPos = temp;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goRightFirst)
        {
            transform.Translate(speed * 1f * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(speed * -1f * Time.deltaTime, 0, 0);
        }

        if (goRightFirst)
        {
            if (transform.position.x > (startPos.x + distance))
            {
                goRightFirst = !goRightFirst;
            }
        }
        else
        {
            if (transform.position.x < (startPos.x - distance))
            {
                goRightFirst = !goRightFirst;
            }
        }
        if (PlayerTouching)
        {
            CarryPlayer();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == playerCollider)
        {
            PlayerTouching = true;
            //Debug.Log("PlatformLeftandRight OnCollisionEnter2D");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerTouching = false;
        //Debug.Log("PlatformLeftandRight OnCollisionExit2D");
    }
    private void CarryPlayer()
    {
        //Debug.Log("PlatformLeftandRight CarryPlayer");
        if (goRightFirst)
        {
            player.transform.Translate(speed * 1f * Time.deltaTime, 0, 0);
        }
        else
        {
            player.transform.Translate(speed * -1f * Time.deltaTime, 0, 0);
        }
    }

}
