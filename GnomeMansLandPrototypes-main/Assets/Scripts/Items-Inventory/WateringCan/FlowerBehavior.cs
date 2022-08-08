using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FlowerBehavior : MonoBehaviour
{
    bool growing = false;

    [SerializeField]
    private GameObject flower;
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = flower.GetComponent<Renderer>();
        Color c = renderer.material.color;
        c.a = 0;
        renderer.material.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        //if (growing)
        //{
            updateFSM();
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("waterdrop") && state == FlowerStates.start)
        {
            Debug.Log("COLLIDED");
            state = FlowerStates.grow;

            //growing = true;
        }
    }
}
