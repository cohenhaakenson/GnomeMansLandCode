using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropBehavior : MonoBehaviour
{
    private float lifetime = 60f; // 1 sec/ 60 frames
    private float lifetracker = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetracker >= lifetime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifetracker++;
        }
    }
}
