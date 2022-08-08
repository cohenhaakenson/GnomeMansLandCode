using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera Component to check world bounds
public partial class CamSupport : MonoBehaviour
{
    public Camera theCam;
    private Bounds worldBound;
    public GameObject GameBound_Min;
    public GameObject GameBound_Max;

    // Lerp support
    private TimedLerp positionLerp = new TimedLerp(2f, 4f);  // 2 second duration, rate of 4 per second
    private TimedLerp sizeLerp = new TimedLerp(2f, 4f);      // Similar values

    // Shake Support
    private ShakePosition shake = new ShakePosition(5, 0.5f); // Oscillate for 5 cycles, in 0.5 seconds

    public enum WorldBoundStatus
    {
        Outside = 0,
        LeftCollide = 1,
        RightCollide = 2,
        TopCollide = 4,
        BottomCollide = 8,
        Inside = 16
    }

    // Start is called before the first frame update
    void Awake()
    {
        theCam = gameObject.GetComponent<Camera>(); // initialized cam
        Debug.Assert(theCam != null); // checks to make sure script is on camera object

        worldBound = new Bounds();
        UpdateWorldBound();
    }

    // Update is called once per frame
    void Update()
    {
        #region Check for Lerp
        bool once = false;
        if (positionLerp.LerpIsActive())
        {
            Vector2 p = positionLerp.UpdateLerp();
            transform.position = new Vector3(p.x, p.y, transform.position.z);
            once = true;
        }

        if (sizeLerp.LerpIsActive())
        {
            Vector2 p = sizeLerp.UpdateLerp();
            theCam.orthographicSize = p.x;
            once = true;
        }
        if (once)
            UpdateWorldBound();
        #endregion

        #region Check for Shake
        if (!shake.ShakeDone())
        {
            transform.position = shake.UpdateShake();
        }
        #endregion 
        //UpdateWorldBound();
    }

    public Bounds GetWorldBounds()
    {
        return worldBound;
    }

    //true if the camera world bound inside of the game bounds. 
    public bool inGameBounds()
    {
        //Debug.Log("CamSupport inGameBounds\n\tworldBound.max.x: " + worldBound.max.x +
        //    "\n\tGameBound_Max.transform.position.x: " + GameBound_Max.transform.position.x + "\n\tworldBound.min.x: " + worldBound.min.x + 
        //    "\n\tGameBound_Min.transform.position.x: " + GameBound_Min.transform.position.x + "\n\tworldBound.max.y: " + 
        //    worldBound.max.y + "\n\tGameBound_Max.transform.position.y: " + GameBound_Max.transform.position.y + 
        //    "\n\tworldBound.min.y: " + worldBound.min.y + "\n\tGameBound_Min.transform.position.y" + GameBound_Min.transform.position.y);
        UpdateWorldBound();
        if (worldBound.max.x > GameBound_Max.transform.position.x)
            return false;
        if (worldBound.min.x < GameBound_Min.transform.position.x)
            return false;
        if (worldBound.max.y > GameBound_Max.transform.position.y)
            return false;
        if (worldBound.min.y < GameBound_Min.transform.position.y)
            return false;
        return true;
    }

    #region bound support
    private void UpdateWorldBound()
    {
        // updates the bound based on Camera position
        if (theCam != null)
        {
            float maxY = theCam.orthographicSize;
            float maxX = theCam.orthographicSize * theCam.aspect;
            float sizeY = 2 * maxY;
            float sizeX = 2 * maxX;

            // z-component must be 0
            Vector3 camCenter = theCam.transform.position;
            camCenter.z = 0.0f;
            worldBound.center = camCenter;
            worldBound.size = new Vector3(sizeX, sizeY, 1f); // z doesnt matter
        }
    }

    // Recreates Bounds intersect() and contains() to exclude the z value
    private bool IntersectXY(Bounds b1, Bounds b2)
    {
        return ((b1.min.x < b2.max.x) && (b1.max.x > b2.min.x)
             && (b1.min.y < b2.max.y) && (b1.max.y > b2.min.y));
    }

    private bool ContainsXY(Bounds b, Vector3 pt)
    {
        return ((b.min.x < pt.x) && (b.max.x > pt.x)
             && (b.min.y < pt.y) && (b.max.y > pt.y));
    }

    public WorldBoundStatus CollideWorldBound(Bounds objBounds, float region = 1f)
    {

        WorldBoundStatus status = WorldBoundStatus.Outside;
        Bounds b = new Bounds(transform.position, region * worldBound.size);
        
        // if objBounds is touching world bounds
        if (IntersectXY(b, objBounds))
        {
            // if objects x is bigger than world, colliding on right
            if (objBounds.max.x > b.max.x)
            {
                status |= WorldBoundStatus.RightCollide;
            }
            // if objects x is smaller than workd, colliding on left
            if (objBounds.min.x < b.min.x)
            {
                status |= WorldBoundStatus.LeftCollide;
            }
            // if objects y is bigger, colliding on top
            if (objBounds.max.y > b.max.y)
            {
                status |= WorldBoundStatus.TopCollide;
            }
            // if object y is smaller, collide on bottom
            if (objBounds.min.y < b.min.y)
            {
                status |= WorldBoundStatus.BottomCollide;
            }

            // intersects but is not touching bounds
            if(status == WorldBoundStatus.Outside)
            {
                status = WorldBoundStatus.Inside;
            }

        }
        return status;
    }

    public WorldBoundStatus ClampToWorldBound(Transform t, float region = 1f)
    {
        Vector3 p = t.position;
        WorldBoundStatus status = WorldBoundStatus.Outside;
        Bounds b = new Bounds(transform.position, region * worldBound.size);
                
        if (p.x > b.max.x) 
        {
            status |= WorldBoundStatus.RightCollide;
            p.x = b.max.x;
        }
        if (p.x < b.min.x)
        {
            status |= WorldBoundStatus.LeftCollide;
            p.x = b.min.x;
        }
        if (p.y > b.max.y)
        {
            status |= WorldBoundStatus.TopCollide;
            p.y = b.max.y;
        }
        if (p.y < b.min.y)
        {
            status |= WorldBoundStatus.BottomCollide;
            p.y = b.min.y;
        }
        
        t.position = p;
        return status;
    }
    #endregion

    #region Viewport support
    public void SetViewportMinPos(float x, float y)
    {
        Rect r = theCam.rect;
        theCam.rect = new Rect(x, y, r.width, r.height);
    }

    public void SetViewportSize(float w, float h)
    {
        Rect r = theCam.rect;
        theCam.rect = new Rect(r.x, r.y, w, h);
    }
    #endregion


}
