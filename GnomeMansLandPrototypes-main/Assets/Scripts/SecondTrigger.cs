using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTrigger : MonoBehaviour
{
    [SerializeField] private ActivateEasier trigger1;
    public bool canTrigger = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !(trigger1.activated) && canTrigger)
        {
            Debug.Log("SecondTrigger OnTriggerEnter2D count: " + trigger1.count);
            if (++(trigger1.count) >= trigger1.timesFailBefore)
            {
                trigger1.activatePortals();
            }
            canTrigger = false;
        }
    }

    //IEnumerator CanTrigger()
    //{
    //    canTrigger = true;
    //    for (int i = 0; i < 10; i++)
    //    {
    //        yield return null;
    //    }
    //    canTrigger = false;
    //}

}
