using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGnomeAnimation : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    public LadderMovement ladder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("moveSpeed is: " + controller.moveSpeed);
        if(ladder.isClimbing)
        {
            GetComponent<Animator>().Play("Ladder");
            if (Input.GetAxis("Vertical") != 0)
            {
                GetComponent<Animator>().enabled = true;
            }else
            {
                GetComponent<Animator>().enabled = false;
            }
        }
        else if (controller.isPushing)
        {
            if (controller.moveSpeed != 0)
                GetComponent<Animator>().Play("Pushing");
            else
                GetComponent<Animator>().Play("PushingIdle");
        }
        else if (!controller.m_Grounded)
        {
            GetComponent<Animator>().Play("Jumping");

        }else if (controller.moveSpeed == 0 && controller.m_Grounded)
        {
            GetComponent<Animator>().Play("Idle");
        }
        else if (controller.moveSpeed != 0 && controller.m_Grounded)
        {
            GetComponent<Animator>().Play("Running");
        }
    }
}
