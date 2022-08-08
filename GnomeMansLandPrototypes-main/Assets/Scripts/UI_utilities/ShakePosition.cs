using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakePosition 
{
    private Vector2 shakeDelta;
    private float duration;
    private float omega;

    // runtime
    private float secLeft = 0;
    private Vector3 orgPos;

    public ShakePosition(float frequency, float durationInSec)
    {
        SetShakeParameters(frequency, durationInSec);
    }

    public void SetShakeParameters(float frequency, float durationInSections)
    {
        duration = durationInSections;
        omega = frequency * 2 * Mathf.PI;
    }
    public void SetShakeMagnitude(Vector2 magnitude, Vector3 OrgPos) {
        orgPos = OrgPos;
        shakeDelta = magnitude;
        secLeft = duration;
    }

    public Vector3 UpdateShake()
    {
        secLeft -= Time.smoothDeltaTime;
        Vector3 c = orgPos;
        if (!ShakeDone())
        {
            float v = NextDampedHarmonic();
            float fx = Random.Range(0f, 1f) > 0.5f ? -v : v;
            float fy = Random.Range(0f, 1f) > 0.5f ? -v : v;
            c.x += shakeDelta.x * fx;
            c.y += shakeDelta.y * fy;
        }
        return c;
    }

    public bool ShakeDone() { return secLeft <= 0f; }

    private float NextDampedHarmonic()
    {
        // computes (Cycles) * cos(  Omega * t )
        var frac = secLeft / duration;
        return frac * frac * Mathf.Cos((1 - frac) * this.omega);
    }
    
}
