using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private LockAnimated door;
    private bool switchOn = false;
    private bool canPress = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E))
        {
            switchOn = !switchOn;

            if (switchOn)
                SwitchOn();
            else
                SwitchOff();
        }
    }

    public void SwitchOn()
    {
        Debug.Log("switch on");
        animator.SetBool("On", true);
        door.OpenLock();
    }
    public void SwitchOff()
    {
        Debug.Log("switch off");
        animator.SetBool("On", false);
        door.CloseLock();
    }
    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     Debug.Log("Touching switch");
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         switchOn = !switchOn;
    //         if (switchOn)
    //             SwitchOn();
    //         else
    //             SwitchOff();
    //     }
        
    // }
    // private void OnTriggerStay2D(Collider2D collider)
    // {
    //     Debug.Log("Stay switch");
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         switchOn = !switchOn;
    //         if (switchOn)
    //             SwitchOn();
    //         else
    //             SwitchOff();
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPress = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPress = false;
        }
    }

}
