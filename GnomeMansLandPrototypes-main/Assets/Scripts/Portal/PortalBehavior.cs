using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    public GameObject newBoundMin; //Min Game Bound if new location is a new level
    public GameObject newBoundMax; //Max Game Bound if new location is a new level
    public GameObject playerNewStart; //Position player should move to
    public GameObject cameraNewStart; //Position cam should move to
    public bool outLevel; //true if the player will move to a location outside the level area
    public bool withTouch; //true if player will teleport upon touch; else, player will need to press E
    private CamSupport myCam;
    private CamMovement camMove;
    private bool canActivate = false;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main.GetComponent<CamSupport>();
        camMove = player.GetComponent<CamMovement>();
    }
    // Update is called once per frame
    void Update()
    { 
        if (canActivate && Input.GetKeyUp(KeyCode.E))
        {
            //Debug.Log("Portal Update yay");
            teleport();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            //Debug.Log("Portal OnTriggerEnter2D");
            //if not with touch, do nothing if E is not pressed
            if (!withTouch)
            {
                canActivate = true;
            } else
                teleport();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            canActivate = false;
            //Debug.Log("Portal OnTriggerExit2D");
        }
    }
    private void teleport()
    {
        //move player; ignore z
        player.transform.position = new Vector3(playerNewStart.transform.position.x,
            playerNewStart.transform.position.y, player.transform.position.z);
        //change camera bounds if leaving level area
        if (outLevel)
        {
            myCam.GameBound_Min = newBoundMin;
            myCam.GameBound_Max = newBoundMax;
            //move camera; ignore z
            myCam.transform.position = new Vector3(cameraNewStart.transform.position.x,
                cameraNewStart.transform.position.y, myCam.transform.position.z);
            //StartCoroutine("fasterLerp");
        }
    }
    //While moving the cam to the area in the next level, 
    //IEnumerator fasterLerp()
    //{
    //    myCam.SetLerpParameters(2, 60);
    //    bool inGameBounds = false;
    //    while (!inGameBounds)
    //    {
    //        inGameBounds = myCam.inGameBounds();
    //        yield return null;
    //    }
    //    myCam.SetLerpParameters(2, 4);
    //}
}
