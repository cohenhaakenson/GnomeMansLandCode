using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressTheSoundManager : MonoBehaviour
{
    private int shape1;
    private int shape2;
    private int shape3;
    private int sound1;
    private int sound2;
    private int sound3;

    private AudioSource source;
    public Sprite[] spriteList;
    public AudioClip[] audioList;
    public Camera cam;

    public GameObject text;
    public GameObject canvasShape1;
    public GameObject canvasShape2;
    public GameObject canvasShape3;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button1ItemImage;
    public GameObject button2ItemImage;
    public GameObject button3ItemImage;


    List<int> gamePattern;
    List<int> playerPattern;
    private List<AudioClip> patternSounds;
    private List<Sprite> patternShapes;

    private bool raiseButtons = false;
    private bool lowerButtons = false;

    private int sizeOfPuzzle = 4;

    private bool allButtonsPressed = false;

    private bool isCorrect = true;

    private bool showItem = false;

    [SerializeField] private LockAnimated door;



    // Start is called before the first frame update
    void Start()
    {
        gamePattern = new List<int>();
        playerPattern = new List<int>();
        patternSounds = new List<AudioClip>();
        patternShapes = new List<Sprite>();
        //GetPattern();
        source = this.GetComponent<AudioSource>();
        StartCoroutine("StartGame", false);


    }

    // Update is called once per frame
    void Update()
    {
        if (raiseButtons && button1.transform.position.y < 0)
        {
            
            button1.transform.Translate(0f, 2f * Time.deltaTime, 0f);
        }
        if (raiseButtons && button2.transform.position.y < 0)
        {

            button2.transform.Translate(0f, 2f * Time.deltaTime, 0f);
        }
        if (raiseButtons && button3.transform.position.y < 0)
        {

            button3.transform.Translate(0f, 2f * Time.deltaTime, 0f);
        }



        if (lowerButtons && button1.transform.position.y > -2)
        {

            button1.transform.Translate(0f, -2f * Time.deltaTime, 0f);
        }
        if (lowerButtons && button2.transform.position.y > -2)
        {

            button2.transform.Translate(0f, -2f * Time.deltaTime, 0f);
        }
        if (lowerButtons && button3.transform.position.y > -2)
        {

            button3.transform.Translate(0f, -2f * Time.deltaTime, 0f);
        }





        if (button1.transform.position.y >= 0 && button2.transform.position.y >= 0 && button3.transform.position.y >= 0)
        {
            showItem = true;
        }else
        {
            showItem = false;
        }

        if(showItem)
        {
            button1ItemImage.SetActive(true);
            button2ItemImage.SetActive(true);
            button3ItemImage.SetActive(true);
        }else
        {
            button1ItemImage.SetActive(false);
            button2ItemImage.SetActive(false);
            button3ItemImage.SetActive(false);
        }
    }

    

    IEnumerator StartGame(bool failedPreviously)
    {
        

        cam.orthographicSize = 8;//changes camera size
        gamePattern.Clear();
        playerPattern.Clear();
        patternShapes.Clear();
        patternSounds.Clear();
        if(failedPreviously)
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "You Failed!";
            yield return new WaitForSeconds(1f);
            text.GetComponent<UnityEngine.UI.Text>().text = "Try Again...";
            yield return new WaitForSeconds(1f);
        }
        //Debug.Log("START GAME");
        for (int i = 0; i < sizeOfPuzzle; i++)//gets puzzle patterns
        {
            gamePattern.Add(Random.Range(0, 3));
        }
        RandomizeAssets();//randomizes shapes and sounds
        lowerButtons = false;
        text.GetComponent<UnityEngine.UI.Text>().text = "Listen Up!";
        yield return new WaitForSeconds(1.5f);

        //shows shape 1 and sound
        canvasShape1.GetComponent<SpriteRenderer>().sprite = spriteList[shape1];
        source.clip = audioList[sound1];
        source.Play();
        yield return new WaitForSeconds(2f);

        //shows shape 1 and sound
        canvasShape2.GetComponent<SpriteRenderer>().sprite = spriteList[shape2];
        source.clip = audioList[sound2];
        source.Play();
        yield return new WaitForSeconds(2f);

        //shows shape 1 and sound
        canvasShape3.GetComponent<SpriteRenderer>().sprite = spriteList[shape3];
        source.clip = audioList[sound3];
        source.Play();
        yield return new WaitForSeconds(2f);
        text.GetComponent<UnityEngine.UI.Text>().text = "Ready?";
        //Hides the corresponding shapes
        canvasShape1.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        canvasShape2.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        canvasShape3.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        yield return new WaitForSeconds(1f);
        //pattern goes here
        text.GetComponent<UnityEngine.UI.Text>().text = "Heres the Pattern";
        for (int i = 0; i < sizeOfPuzzle; i++)
        {
            source.clip = patternSounds[gamePattern[i]];
            source.Play();
            canvasShape2.GetComponent<SpriteRenderer>().sprite = patternShapes[gamePattern[i]];
            yield return new WaitForSeconds(1f);
        }
        canvasShape2.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        yield return new WaitForSeconds(1f);

        text.GetComponent<UnityEngine.UI.Text>().text = "Start!";
        yield return new WaitForSeconds(.5f);

        cam.orthographicSize = 5;//changes camera size
        raiseButtons = true;
        yield return new WaitForSeconds(0.8f);
        yield return new WaitUntil(() => allButtonsPressed == true);
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("CHECK IF PLAYER WAS CORRECT");

        isCorrect = true;
        for (int i = 0; i < sizeOfPuzzle; i++)
        {
            Debug.Log("player is: " + playerPattern[i] + "\tgame is: " + gamePattern[i]);
            if (playerPattern[i] != gamePattern[i])
            {
                
                isCorrect = false;
                break;
            }
        }
        restartGame(isCorrect);
        if (isCorrect)
        {
            cam.orthographicSize = 12;//changes camera size
            text.GetComponent<UnityEngine.UI.Text>().text = "You Passed!";
            yield return new WaitForSeconds(1.5f);
            cam.orthographicSize = 5;//changes camera size
        }else
        {
            cam.orthographicSize = 8;//changes camera size
            text.GetComponent<UnityEngine.UI.Text>().text = "You Failed!";
            yield return new WaitForSeconds(2f);
            
        }
        


    }

    void RandomizeAssets()
    {
        shape1 = Random.Range(1,9);
        shape2 = Random.Range(1, 9);
        shape3 = Random.Range(1, 9);

        button1ItemImage.GetComponent<SpriteRenderer>().sprite = spriteList[shape1];
        patternShapes.Add(spriteList[shape1]);
        while (shape1 == shape2) {
            shape2 = Random.Range(1, 9);
        }
        patternShapes.Add(spriteList[shape2]);
        button2ItemImage.GetComponent<SpriteRenderer>().sprite = spriteList[shape2];

        while (shape1 == shape3 || shape2 == shape3)
        {
            shape3 = Random.Range(1, 9);
        }
        patternShapes.Add(spriteList[shape3]);
        button3ItemImage.GetComponent<SpriteRenderer>().sprite = spriteList[shape3];
        //Debug.Log("shape \ts1: " + shape1 + "\ts2: " + shape2 + "\ts3: " + shape3);




        sound1 = Random.Range(0, 8) ;
        sound2 = Random.Range(0, 8);
        sound3 = Random.Range(0, 8);
        patternSounds.Add(audioList[sound1]);
        while (sound1 == sound2)
        {
            sound2 = Random.Range(0, 8);
        }
        patternSounds.Add(audioList[sound2]);
        while (sound1 == sound3 || sound2 == sound3)
        {
            sound3 = Random.Range(0, 8);
        }
        patternSounds.Add(audioList[sound3]);
        //Debug.Log("sound \ts1: " + sound1 + "\ts2: " + sound2 + "\ts3: " + sound3);
    }

    public void buttonPressed(int button)
    {
        source.clip = patternSounds[button-1];
        source.Play();


        playerPattern.Add(button -1);
        //foreach(var x in gamePattern)
        //{
        //    Debug.Log("GamePattern: " +x.ToString());
        //}

        //foreach (var x in playerPattern)
        //{
        //    Debug.Log("PlayerPattern: " + x.ToString());
        //}

        if (playerPattern.Count == sizeOfPuzzle)
        {
            allButtonsPressed = true;
        }

    }

    private void restartGame(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("PASSED THE GAME");
            lowerButtons = true;
            door.OpenLock();
        }else
        {
            Debug.Log("FAILED THE GAME");
            allButtonsPressed = false;
            isCorrect = true;
            raiseButtons = false;
            
            
            StartCoroutine("StartGame", true) ;

            


        }
        lowerButtons = true;
    }
}
