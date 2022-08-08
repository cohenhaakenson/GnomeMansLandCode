using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLerp
{
    private float lerpTime;
    private float lerpRate;
    private Vector2 end;
    private Vector2 current;

    // the timed parameters
    private float startTime; //
    private bool lerpEnded;

    public TimedLerp(float timeInSeconds, float rate)
    {
        SetLerpParms(timeInSeconds, rate);
        lerpEnded = true;
    }
    public void SetLerpParms(float timeInSeconds, float rate)
    {
        lerpTime = timeInSeconds;
        lerpRate = rate;
    }
    public void BeginLerp(Vector2 start, Vector2 theEnd)
    {
        current = start;
        end = theEnd;
        startTime = Time.realtimeSinceStartup;
        lerpEnded = false;
    }
    public Vector2 UpdateLerp()
    {
        lerpEnded = ((Time.realtimeSinceStartup - startTime) > lerpTime);
        if (lerpEnded)
            current = end;
        else 
            current += (end - current) * (lerpRate * Time.smoothDeltaTime);
        return current;
    }
    public bool LerpIsActive() { return !lerpEnded; }
    public void EndLerp()
    {
        lerpEnded = true;
    }
}