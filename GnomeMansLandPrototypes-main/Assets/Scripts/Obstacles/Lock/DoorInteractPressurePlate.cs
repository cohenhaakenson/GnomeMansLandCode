using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractPressurePlate : MonoBehaviour
{
    //[SerializeField] private GameObject doorGameObject;
    [SerializeField] private LockAnimated door;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        //door = doorGameObject.GetComponent<LockAnimated>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                door.CloseLock();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if (collider.GetComponent<CharacterController2D>() != null)
        // {
        //     //Player entered collider
        //     door.OpenLock();
        // }
        door.OpenLock();
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        // if (collider.GetComponent<CharacterController2D>() != null)
        // {
        //     //Player still on top of collider
        //     timer = 1f;
        // }
        timer = 1f;
    }
    // private void OnTriggerExit2D(Collider2D collider)
    // {
    //     if (collider.GetComponent<CharacterController2D>() != null)
    //     {
    //         //player exited collider
    //         door.CloseLock();
    //     }
    // }
}
