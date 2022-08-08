using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BD;

public class GameManager : BD.GameManagerBase
{
    public static GameManager instance;
    
    [SerializeField]
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public StateTag mainMenuState;
    public StateTag playingState;
    public int score;

    public override void Awake()
    {
        base.Awake();
        SetInstance();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("Menu");
        //}
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public override void SetInstance()
    {
        if (!instance)
            instance = this;
        // else
        //     Debug.LogError($"GameManager: Trying to set instance but there is already one! {instance}");
    }

    public void AwardScore(int award)
    {
        score += award;
        // find UI, tell it to reward
    }
}
