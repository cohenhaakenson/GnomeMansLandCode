using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEasier : MonoBehaviour
{
    [SerializeField] private GameObject ActivationMsg;
    [SerializeField] private GameObject Portal1;
    [SerializeField] private GameObject Portal2;
    [SerializeField] private SecondTrigger trigger2; //To increment count player will need to go from this trigger to trigger 2
    [SerializeField] private bool useSecondTrig = false; //If false, player will need to exit this trigger to increment count
    [SerializeField] public int timesFailBefore = 3; //number of times the player "fails" before portals are activated
    public int count;
    public bool activated;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        activated = false;
        Portal1.SetActive(false);
        Portal2.SetActive(false);
        ActivationMsg.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!activated)
            {
                if (useSecondTrig)
                {
                    trigger2.canTrigger = true;
                } else if (++count >= timesFailBefore)
                {
                    activatePortals();
                }
            }
        }
    }

    public void activatePortals()
    {
        Portal1.SetActive(true);
        Portal2.SetActive(true);
        StartCoroutine("PortalActivatedMessage");
    }

    IEnumerator PortalActivatedMessage()
    {
        ActivationMsg.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        ActivationMsg.SetActive(false);
    }
}
