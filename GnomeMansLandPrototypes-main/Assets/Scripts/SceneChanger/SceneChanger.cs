using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        GameManager.instance.GameStateManager.ChangeState(GameManager.instance.playingState);
        SceneManager.LoadScene(sceneName);
    }

    public void MenuScene()
    {
        GameManager.instance.GameStateManager.ChangeState(GameManager.instance.mainMenuState);
        SceneManager.LoadScene("Menu");
    }
}
