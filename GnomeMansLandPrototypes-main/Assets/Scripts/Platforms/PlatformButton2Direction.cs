using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton2Direction : MonoBehaviour
{
    [SerializeField] private GreenButtonBehavior button;
    [SerializeField] private PlatformUpandDown upDown;
    [SerializeField] private PlatformLeftandRight leftRight;
    [SerializeField] private bool goLrFirst = false; //this will be equivalent to inital button status (pressed/not)
    private bool buttonStart;
    // Start is called before the first frame update
    void Start()
    {
        buttonStart = goLrFirst;

        if (goLrFirst)
        {
            upDown.enabled = false;
            leftRight.enabled = true;
        }
        else
        {
            upDown.enabled = true;
            leftRight.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!buttonStart)
        {
            if (goLrFirst != button.isActivated)
            {
                switchDirection();
            }
        } else
        {
            if (goLrFirst == button.isActivated)
            {
                switchDirection();
            }
        }

    }

    private void switchDirection()
    {
        if (goLrFirst)
        {
            goLrFirst = false;
            upDown.enabled = true;
            leftRight.enabled = false;
        } else
        {
            goLrFirst = true;
            upDown.enabled = false;
            leftRight.enabled = true;
        }
    }
}
