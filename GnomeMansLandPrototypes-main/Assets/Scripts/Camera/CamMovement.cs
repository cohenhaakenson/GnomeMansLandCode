using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    private CamSupport myCam;
    //public GameObject respawnPos;
    //public GameObject CamStartPos;
    public float WorldBoundRegion = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main.GetComponent<CamSupport>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myCam != null)
        {
            //#region Player respawn if outside bounds
            //Bounds pBound = GetComponent<Renderer>().bounds; // player bounds
            ////player respawns if out of game bounds
            //if (pBound.max.x >= myCam.GameBound_Max.transform.localPosition.x ||
            //    pBound.min.x <= myCam.GameBound_Min.transform.localPosition.x ||
            //    pBound.max.y >= myCam.GameBound_Max.transform.localPosition.y ||
            //    pBound.min.y <= myCam.GameBound_Min.transform.localPosition.y)
            //{
            //    myCam.transform.localPosition = CamStartPos.transform.localPosition;
            //    transform.localPosition = respawnPos.transform.localPosition;
            //}
            //#endregion

            #region Camera Support: Push and Collision Bound
            //push if position collides with worldbound region
            myCam.PushCameraByPos(transform.position, WorldBoundRegion);
            CamSupport.WorldBoundStatus status = myCam.CollideWorldBound(GetComponent<BoxCollider2D>().bounds, WorldBoundRegion);
            #endregion
        }
    }
}
