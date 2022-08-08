using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Animator animator;
    
    //public GameObject player;
    [SerializeField] private float jumpForce = 400f;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     Rigidbody2D body = collider.aaattachedRigidbody;

    //     // no rigidbody
    //     if (body == null || body.isKinematic)
    //         return;
    //     if (collider.GetComponent<CharacterController2D>() != null)
    //     {
    //         Debug.Log("player is standing on trampoline");
    //         animator.SetBool("isStepped", true);
    //         body.AddForce(new Vector2(0f, jumpForce));
    //         // Jump(collider.gameObject);
    //     }

    // }
    // private void OnTriggerExit2D(Collider2D collider)
    // {
    //     animator.SetBool("isStepped", false);
    // }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("enter trampoline");
            // animator.SetBool("isStepped", true);
        }

        // Debug.Log("enter trampoline");
        // animator.SetBool("isStepped", true);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("player is standing on trampoline");
        if(collision.gameObject.tag == "Player")
        {
            Jump(collision.gameObject);
        }

        if (collision.gameObject.tag == "Movable")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce*3));
        }

        // animator.SetBool("isStepped", true);

        //Jump(collision.gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //animator.SetBool("isStepped", false);
    }



    public void ActivateTrampoline()
    {
        animator.SetBool("isStepped", true);
    }
    void Jump(GameObject jumper)
    {
        //jumper.GetComponent<Animator>().Play("Idle");
        
        jumper.GetComponent<Animator>().Play("Jumping",-1,0f);
        jumper.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
    }

}
