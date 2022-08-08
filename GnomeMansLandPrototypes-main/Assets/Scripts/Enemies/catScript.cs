using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catScript : MonoBehaviour
{
    private enum catState
    {
        idle,
        walkRight,
        walkLeft,
        outOfPostion,
        attack
    }

    private Vector3 initPosition;
    public CharacterController2D controller;
    private float runSpeed = 15f;
    private float attackRunSpeed ;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    float currentPosition;
    bool isIdle = false;
    float maxDist = 3;//maximum distance cat will move from init position
    //if maxDist is too small, code might not work correctly
    private Animator anim;
    bool attackMode = false;
    bool canJump = true;
    bool isAboveBelow = false;

    private catState state = catState.idle;
    private GameObject player;

    float currentTime;
    float distBetweenPlayer;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = this.gameObject.transform.position;
        anim = gameObject.GetComponent<Animator>();
        currentPosition = transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player");
        attackRunSpeed = runSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(initPosition, transform.position) > 23f)
        {
            transform.position = initPosition;
        }
        //Debug.Log("horizontal force is: " + horizontalMove * Time.fixedDeltaTime);
        
        //Debug.Log("attack bool is: " + attackMode);

        distBetweenPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distBetweenPlayer < 4)
        {
            state = catState.attack;
            attackMode = true;
        }
        else
        {
            attackMode = false;
            if (isIdle)
            {
                state = catState.idle;
            }
        }


        if (!attackMode && (transform.position.x < currentPosition - maxDist - .2 ||
            transform.position.x > currentPosition + maxDist + .2))
        {
            state = catState.outOfPostion;//if not attack, go back to positin
        }

        //Debug.Log("Current state is: " + state);
        switch (state)
        {
            
            case catState.idle:
                if (!isIdle)
                {
                    StartCoroutine("idle");
                    isIdle = true;
                }
                break;
            case catState.walkLeft:
                walkLeft();
                break;
            case catState.walkRight:
                walkRight();
                break;
            case catState.outOfPostion:
                outOfPosition();
                break;
            case catState.attack:
                attack();
                break;
        }
    }
    private void attack()
    {
        anim.Play("angry");
        if (canJump)
        {
            jump = true;
            StartCoroutine("catJump");
        }
        
        
        if (isAboveBelow)
        {
            
            if (Input.GetAxisRaw("Horizontal") >= 0)
            {
                horizontalMove = 1 * attackRunSpeed;
            }else
            {
                horizontalMove = -1 * attackRunSpeed;
            }
            
        }
        else if (transform.position.x > player.transform.position.x)
        {
            horizontalMove = -1 * attackRunSpeed;
        }
        else
        {
            horizontalMove = 1 * attackRunSpeed;
        }

        if (transform.position.x > player.transform.position.x + 1 ||
            transform.position.x < player.transform.position.x - 1)
        {
            state = catState.idle;
        }


        if (distBetweenPlayer > 4)
        {
            state = catState.walkLeft;
        }
    }
    IEnumerator idle()
    {
        anim.Play("idle");
        horizontalMove = 0 * runSpeed;
        yield return new WaitForSeconds(2);
        isIdle = false;
        if (transform.position.x > currentPosition + maxDist)
        {
            state = catState.walkLeft;
        }
        else
        {
            state = catState.walkRight;
        }

    }
    IEnumerator catJump()
    {
        canJump = false;
        yield return new WaitForSeconds(2);//4 second cooldown for jumping
        canJump = true;
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }


    private void walkRight()
    {
        anim.Play("movement");

        if (transform.position.x < currentPosition + 3)
        {
            horizontalMove = 1 * runSpeed;
        }
        else
        {
            state = catState.idle;
        }
    }
    private void walkLeft()
    {
        anim.Play("movement");
        if (transform.position.x > currentPosition - 3)
        {
            horizontalMove = -1 * runSpeed;
        }
        else
        {
            state = catState.idle;
        }
    }
    private void outOfPosition()//get back to initial position
    {
        anim.Play("movement");

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
            state = catState.idle;
        }


    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 direction;
        if (collision.gameObject.CompareTag("Player"))
        {
            direction= collision.GetContact(0).normal;
            if(direction.y == 1 || direction.y == -1)
            {
                
                isAboveBelow = true;
            }else
            {
                isAboveBelow = false;
            }
            attackRunSpeed = runSpeed *15;
        }else
        {
            attackRunSpeed = runSpeed * 2;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackRunSpeed = runSpeed * 2;
            isAboveBelow = false;
        }
    }
}
    
