using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{

    [SerializeField] GameObject anvil;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine("startFalling");

        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StopCoroutine("startFalling");

        }
    }

    IEnumerator startFalling()
    {
        //Debug.Log("Start Falling!");
        float randomX;
        float y = 5;
        while (true)
        {
            randomX = Random.Range(-8.2f, 8.2f);
            Vector3 newPosition = transform.position;
            newPosition.y += y;
            newPosition.x += randomX;
            GameObject e = Instantiate(anvil as GameObject, newPosition,transform.rotation);
            yield return new WaitForSeconds(.2f);
        }
        

    }
}
