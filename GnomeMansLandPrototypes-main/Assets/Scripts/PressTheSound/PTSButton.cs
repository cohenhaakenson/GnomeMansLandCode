using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTSButton : MonoBehaviour
{
    public int buttonNumber;
    public GameObject sceneManager;
    public GameObject redButton;

    private bool canPress = true;


    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && canPress)
        {
            //Debug.Log("In here");
            if (Input.GetKey(KeyCode.F))
            {
                //Debug.Log("BUTTON PRESSED");
                sceneManager.GetComponent<PressTheSoundManager>().buttonPressed(buttonNumber);
                canPress = false;
                StartCoroutine("redButtonPressed");
            }
        }
    }
  

    IEnumerator redButtonPressed()
    {
        Vector2 temp = redButton.transform.localPosition;
        temp.y = .4f;
        redButton.transform.localPosition = temp;
        yield return new WaitForSeconds(.9f);
        temp.y = .6f;
        redButton.transform.localPosition = temp;
        canPress = true;
    }

}
