using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;
    public bool isGameOver = true;
    public bool isGamePaused = false;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private Animator _pauseMenuAnimation;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _pauseMenuAnimation = GameObject.Find("PauseMenu").GetComponent<Animator>();
        _pauseMenuAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGameOver = false;
                _uiManager.gameStartUI();
                _spawnManager.initializeComponents();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                loadMenuScene();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isGamePaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }

        }
    }

    public void resumeGame()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = 1f;
        _uiManager.hidePauseMenu();
    }

    public void pauseGame()
    {
        isGamePaused = !isGamePaused;
        _pauseMenuAnimation.SetBool("pauseAnimation", true);
        Time.timeScale = 0f;
        _uiManager.showPauseMenu();
    }

    public void loadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void gameOver()
    {
        isGameOver = true;
        _uiManager.gameOver();
    }
}
