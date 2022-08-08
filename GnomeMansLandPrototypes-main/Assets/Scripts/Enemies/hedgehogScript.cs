using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hedgehogScript : MonoBehaviour
{
    private enum hedgeState
    {
        idle,
        outOfPostion,
        attack
    }

    
    public CharacterController2D controller;
    private float runSpeed = 10f;
    private float attackRunSpeed;
    float horizontalMove;
    bool jump = false;
    bool crouch = false;
    float currentPosition;
    //if maxDist is too small, code might not work correctly
    private Animator anim;
    bool attackMode = false;
    bool startAttack = false;
    private Rigidbody2D body;
    private bool canMove = true;
    private new AudioSource audio;
    private int maxDist = 0;

    private hedgeState state = hedgeState.idle;
    private GameObject player;
    float distBetweenPlayer;
    private Vector3 initPosition;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        audio = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        currentPosition = transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player");
        attackRunSpeed = runSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void attack()
    {
        
        if(startAttack)
        {
            anim.Play("attack");
            body = player.gameObject.GetComponent<Rigidbody2D>();
            player.GetComponent<PlayerMovement>().horizontalMove = 0;
            player.GetComponent<PlayerMovement>().jump = false;
            if (transform.position.x > player.transform.position.x)
            {
                body.AddForce(new Vector2(-3500, 0));
            }
            else
            {
                body.AddForce(new Vector2(3500, 0));
            }
            StartCoroutine("waitForAttack");
        }
        

        if(canMove)
        {
            anim.Play("movement");
            if (transform.position.x > player.transform.position.x)
            {
                horizontalMove = -1 * attackRunSpeed;
            }
            else
            {
                horizontalMove = 1 * attackRunSpeed;
            }
        }
        else
        {
            horizontalMove = 0;
            
        }
    }

    IEnumerator waitForAttack()
    {
        
        canMove = false;
        yield return new WaitForSeconds(.5f);
        canMove = true;
    }

    
   
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;


        if (Vector3.Distance(initPosition, transform.position) > 5f)
        {
            transform.position = initPosition;
        }
        // Debug.Log(canMove);
        distBetweenPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distBetweenPlayer < 4)
        {
            state = hedgeState.attack;
            attackMode = true;
        }
        else
        {
            attackMode = false;
        }


        if (!attackMode && (transform.position.x < currentPosition - maxDist - .1 ||
            transform.position.x > currentPosition + maxDist + .1))
        {
            state = hedgeState.outOfPostion;//if not attack, go back to positin
        }
        if (!canMove)
        {
            state = hedgeState.attack;
        }
        //Debug.Log(state);
        switch (state)
        {
            case hedgeState.idle:
                anim.Play("idle");
                horizontalMove = 0 * runSpeed;
                break;
            case hedgeState.outOfPostion:
                anim.Play("movement");
                outOfPosition();
                break;
            case hedgeState.attack:
                attack();
                break;
        }
    }


    
    private void outOfPosition()//get back to initial position
    {
        
        if (transform.position.x > currentPosition)
        {
            horizontalMove = -1 * runSpeed;
        }
        else
        {
            horizontalMove = 1 * runSpeed;
        }
        if (transform.position.x > currentPosition + .1 ||
            transform.position.x < currentPosition - .1)
        {
            state = hedgeState.idle;
        }


    }


    
    private void OnCollisionStay2D(Collision2D collision)
    {
        //attack animation and code
        if(collision.gameObject.CompareTag("Player"))
        {
            startAttack = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //attack animation and code
        if (collision.gameObject.CompareTag("Player"))
        {
            audio.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startAttack = false;
        }
    }

}

