using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBridge : MonoBehaviour
{
    Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        //StartCoroutine("destroyBridge");

    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.name);
        if (collider.gameObject.layer == 9)
        {
            StartCoroutine("destroyBridge");

        }
    }

    IEnumerator destroyBridge()
    {
        //Debug.Log("Bridge piece destroyed");
        
        this.transform.position = new Vector3(0, 0, 1000);
        yield return new WaitForSeconds(.5f);
        this.transform.position = currentPosition;
    }

}
