using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FlowerBehavior : MonoBehaviour
{
    
    private enum FlowerStates
    {
        start,
        grow,
        fadeIn,
        platform,

    }

    private FlowerStates state = FlowerStates.start;

    private float flowerHeight = 70f * 2; //size of blocks times 2
    private float growFrames = 120f; // 1 sec for grow
    private float growRate = 1f / 60f; // 2 units per sec
    private float inFadeFrames = 60f;
    private float fadeRate = .6f / 60f;

    private float frameTracker = 0; 

    private void updateFSM()
    {
        switch (state)
        {
            case FlowerStates.start:
                serviceStart();
                break;
            case FlowerStates.grow:
                serviceGrow();
                break;
            case FlowerStates.fadeIn:
                serviceFadeIn();
                break;
            case FlowerStates.platform:
                servicePlatform();
                break;
        }
    }

    private void serviceStart()
    {
        // may not need
    }

    private void serviceGrow()
    {
        if (frameTracker > growFrames)
        {
            state = FlowerStates.fadeIn;
            frameTracker = 0;
        }
        else
        {
            frameTracker++;
            float s = transform.localScale.x;
            s += growRate;
            transform.localScale = new Vector3(s, s, 1);
        }
    }

    private void serviceFadeIn()
    {
        flower.GetComponent<BoxCollider2D>().enabled = true;
        // activate stem and flower platform
        //StartCoroutine("Fade");
        if (frameTracker > growFrames)
        {
            growing = false;
            state = FlowerStates.platform;
        }
        else
        {
            frameTracker++;
            float s = transform.localScale.x;
            s -= growRate;
            transform.localScale = new Vector3(s, s, 1);

            Renderer renderer = flower.GetComponent<Renderer>();
            Color c = renderer.material.color;
            c.a += fadeRate;
            renderer.material.color = c;
        }

        flower.GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        
    }

    private void servicePlatform()
    {
        flower.GetComponent<BoxCollider2D>().enabled = true;
    }
}
