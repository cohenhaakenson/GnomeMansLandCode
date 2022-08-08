using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSize : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float newCamSize;
    [SerializeField] private float changeSpeed;
    private float currentSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine("changeCam");
            
        }
    }

    IEnumerator changeCam()
    {
        while (cam.orthographicSize < newCamSize)
        {
            cam.orthographicSize += changeSpeed;
            yield return new WaitForEndOfFrame();
        }

        while (cam.orthographicSize > newCamSize)
        {
            cam.orthographicSize -= changeSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

    

}
