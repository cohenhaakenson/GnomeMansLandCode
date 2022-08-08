using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAnimated : MonoBehaviour
{
    private Animator animator;
    private new AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void OpenLock()
    {
        Debug.Log("opening door");
        animator.SetBool("Open", true);
        if(Time.timeSinceLevelLoad > 5)
        {
            audio.Play();//this is so it doesnt play it at the beginning of the game
        }
        
    }
    public void CloseLock()
    {
        Debug.Log("closing door");
        animator.SetBool("Open", false);
    }
}
