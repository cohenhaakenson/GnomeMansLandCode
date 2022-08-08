using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private bool isLadder;
    [HideInInspector]public bool isClimbing;
    private float climbSpeed = 8F;
    private float vertical;

    private SpriteRenderer spriteRenderer;
    private Sprite climbSprite;
    public Sprite[] spriteArray;

    [SerializeField] private Rigidbody2D rb;
	[SerializeField] private CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //climbSprite = spriteArray[2];
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
            //ChangeSprite();
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
        else
        {
            rb.gravityScale = 3f;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ladder"))
    //    {
    //        isLadder = true;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
			controller.canFlip = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            controller.canFlip = true;
            GetComponent<Animator>().enabled = true;
            //ChangeSprite();
        }
    }

//    private void ChangeSprite()
//    {
//        if (isClimbing)
//            spriteRenderer.sprite = climbSprite;
//        else
//            spriteRenderer.sprite = spriteArray[0];
//    }
}
