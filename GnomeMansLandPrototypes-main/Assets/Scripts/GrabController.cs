using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private CharacterController2D gnome;
    public Transform grabDetect;
    public Transform boxHolder;
    Transform obstacleParent;
    public float rayDist;
    public GameObject obstacles;
    private bool isHolding = false;

    void Start()
    {
        gnome = GetComponentInParent<CharacterController2D>();
        
    }

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        if (grabCheck.collider != null && grabCheck.collider.tag == "Movable")
        {
            Debug.Log("Found movable object");
            GameObject g = grabCheck.collider.gameObject;
            //Rigidbody2D rigidbody = g.GetComponent<Rigidbody2D>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                isHolding = !isHolding;
                
                if (!isHolding)
                {
                    gnome.isPushing = false;
                    gnome.canFlip = true;
                    gnome.canJump = true;
                    // rigidbody.isKinematic = false;
                    if (g.GetComponent<Rigidbody2D>() == null)
                    {
                        Debug.Log("adding rigidbody");
                        g.AddComponent<Rigidbody2D>();
                        g.GetComponent<Rigidbody2D>().mass = 100f;
                    }

                    if (obstacles == null)
                        g.transform.parent = null;
                    else
                        g.transform.parent = obstacles.transform;
                    // grabCheck.collider.gameObject.transform.parent = obstacleParent;
                }
            }

            if (isHolding)
            {
                Debug.Log("Holding box");
                if (obstacles == null)
                {
                    // obstacles = g;
                    // obstacles = g.transform.parent;
                }
                g.transform.parent = boxHolder;
                // rigidbody.isKinematic = true;
                if (g.GetComponent<Rigidbody2D>() != null)
                    Destroy(g.GetComponent<Rigidbody2D>());
                
                gnome.isPushing = true;
                gnome.canFlip = false;
                Debug.Log(gnome.canFlip);
                gnome.canJump = false;
            }

        }

    }
}
