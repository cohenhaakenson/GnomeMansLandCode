using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAnvil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 10)
        {
            Destroy(this.gameObject);

        }
    }
}
