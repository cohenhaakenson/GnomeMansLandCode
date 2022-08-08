using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag($"MovableObject")) return;
        if (Vector3.Distance(transform.position,other.transform.position ) > 0.1f) return;
        other.attachedRigidbody.isKinematic = true;
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.blue;
        Destroy(this);
    }
}
