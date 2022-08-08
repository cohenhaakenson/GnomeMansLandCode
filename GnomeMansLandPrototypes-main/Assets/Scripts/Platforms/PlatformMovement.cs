using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform targetA, targetB;
    private float speed = 1f; //Change this to suit your game.
    private bool switching = false;

    private void Start()
    {
        targetA = transform;

    }

    void Update()
    {
        if (!switching)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, speed * Time.deltaTime);
        }
        else if (switching)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, speed * Time.deltaTime);
        }
        if (transform.position == targetB.position)
        {
            switching = true;
        }
        else if (transform.position == targetB.position)
        {
            switching = false;
        }
    }
}
