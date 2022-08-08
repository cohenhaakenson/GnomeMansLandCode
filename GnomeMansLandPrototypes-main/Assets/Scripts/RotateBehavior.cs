using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehavior : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 90f / 2f; // 90-degrees in 2 seconds

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
