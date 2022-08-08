using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearingBlock : MonoBehaviour
{
    public GameObject player;
    private Vector3 curPos;
    private Vector3 newPos;
    private bool alreadyStarted = false;
    private bool isShaking = false;

    private bool canSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        curPos = this.gameObject.transform.position;
        newPos = curPos;
        newPos.x = 10000000;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(curPos, player.transform.position) > 2f)
        {
            canSpawn = true;
        }else
        {
            canSpawn = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyStarted)
        {
            Vector2 direction = collision.GetContact(0).normal;
            if(direction.y == -1)
            {
                alreadyStarted = true;
                StartCoroutine("disappear");
            }
        }
    }

    IEnumerator disappear()
    {
        yield return new WaitForSeconds(.10f);
        Vector3 sub = curPos;
        Debug.Log("Started disappear");
        StartCoroutine("startTimer");
        while (isShaking)
        {
            Debug.Log("shaking object");
            
            sub.x = curPos.x+ Mathf.Sin(Time.time * 70) * .1f;
            transform.position = sub;
            yield return new WaitForEndOfFrame();
        }
        transform.position = newPos;
        yield return new WaitForSeconds(1.5f);

        yield return new WaitUntil(() => canSpawn);
        transform.position = curPos;
        alreadyStarted = false;
    }

    IEnumerator startTimer()
    {
        isShaking = true;
        yield return new WaitForSeconds(.5f);
        isShaking = false;
    }
}
