using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int carrotsToLose = 10;
    public int carrotsToWin = 5;
    public float timeToRestart = 3;

    [Header("Read Only")]
    public int currentDestroyedCarrots = 0;
    public int currentPickedCarrots = 0;
    public bool isGameOver = false;

    GameUI gameUI = null;

    void Start()
    {
        isGameOver = false;
        currentDestroyedCarrots = 0;
        currentPickedCarrots = 0;
        gameUI = FindObjectOfType<GameUI>();
    }

    public void OnCarrotDestroyed()
    {
        if(isGameOver)
            return;
        
        currentDestroyedCarrots = currentDestroyedCarrots + 1;
        if(currentDestroyedCarrots >= carrotsToLose)
        {
            isGameOver = true;
            gameUI.ActivateLoseScreen();
            Invoke("RestartLevel",timeToRestart);
        }
    }
    public void OnCarrotPicked()
    {
        if(isGameOver)
            return;
        
        currentPickedCarrots = currentPickedCarrots + 1;
        if(currentPickedCarrots>=carrotsToWin)
        {
            isGameOver = true;
            gameUI.ActivateWinScreen();
            Invoke("GoToNextLevel",timeToRestart);
        }
    }

    void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    void GoToNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextLevel>=SceneManager.sceneCountInBuildSettings)
            nextLevel = 0;
        
        SceneManager.LoadScene(nextLevel);
    }
}