using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForCavePuzzle : MonoBehaviour
{

    [SerializeField] private GameObject horisontalWood;
    [SerializeField] private GameObject verticalWood;
    [SerializeField] private GameObject redButton;
    [SerializeField] private LockAnimated door;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    private double currentPos;
    private Vector2 startPos;

    [SerializeField] private Camera cam;
    private bool canPress = true;
    private bool onRightSide = false;
    private bool boxIsDropped = false;
    // Start is called before the first frame update
    void Start()
    {
        door.OpenLock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && canPress)
        {
            //Debug.Log("In here");
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine("dropDoor");
                //Debug.Log("BUTTON PRESSED");
                
                StartCoroutine("redButtonPressed");
                if (onRightSide)
                {
                    StartCoroutine("goLeft");
                }else
                {
                    StartCoroutine("goRight");
                }
                onRightSide = !onRightSide;
            }
        }
    }


    IEnumerator dropDoor()
    {
        float currentPosV = verticalWood.transform.localPosition.y;
        while (verticalWood.transform.localPosition.y > 0)
        {
            //this is backwards because it is rotated 
            verticalWood.transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator goLeft()
    {
        
        currentPos = horisontalWood.transform.position.x;
        canPress = false;
        while (horisontalWood.transform.position.x > (currentPos - distance))
        {
            horisontalWood.transform.Translate(-1f * speed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        canPress = true;
    }
    IEnumerator goRight()
    {
        if(!boxIsDropped)
        {
            door.CloseLock();
            StartCoroutine("changeCam", 20);
            boxIsDropped = true;
        }
        
        currentPos = horisontalWood.transform.position.x;
        canPress = false;
        while (horisontalWood.transform.position.x < (currentPos+distance)) {
            horisontalWood.transform.Translate(01f * speed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("changeCam", 10);
        canPress = true;
    }
    IEnumerator changeCam(double newCamSize)
    {
        while (cam.orthographicSize < newCamSize)
        {
            cam.orthographicSize += .05f;
            yield return new WaitForEndOfFrame();
        }

        while (cam.orthographicSize > newCamSize)
        {
            cam.orthographicSize -= .05f;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator redButtonPressed()
    {
        Vector3 temp = redButton.transform.localPosition;
        temp.y = .4f;
        redButton.transform.localPosition = temp;
        yield return new WaitForSeconds(.9f);
        temp.y = .6f;
        redButton.transform.localPosition = temp;
        canPress = true;
    }
}
