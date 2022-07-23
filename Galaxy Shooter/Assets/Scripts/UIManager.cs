using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public Sprite[] playerImagesLives;
    public Image playerLife;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI playerBestScoreText;
    public int score = 0;
    public int bestScore = 0;
    public Image titleMenu;
    public bool gameStart = false;
    public TextMeshProUGUI gameOverText;

    [SerializeField]
    private GameObject _pauseMenuPanel;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bestScore = PlayerPrefs.GetInt("Best", 0);
        updateBestScore();
    }
    public void gameStartUI()
    {
        score = 0;
        titleMenu.enabled = false;
        titleMenu.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    public void updateLife(int life)
    {
        playerLife.sprite = playerImagesLives[life];
    }

    public void updateScore()
    {
        score += 10;
        playerScoreText.text = "Score: " + score;
    }

    public void updateBestScore()
    {
        playerBestScoreText.text = "Best: " + bestScore;
    }

    public void checkBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            updateBestScore();
            PlayerPrefs.SetInt("Best", bestScore);
        }
    }

    public bool gameStarted()
    {
        return gameStart;
    }

    public void gameOver()
    {
        checkBestScore();
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(showTitleMenu());
    }
    public void resetScore()
    {
        playerScoreText.text = "Score: " + 0;
    }

    private IEnumerator showTitleMenu()
    {
        yield return new WaitForSeconds(5f);
        playerScoreText.text = "Score: " + 0;
        gameOverText.gameObject.SetActive(false);
        titleMenu.enabled = true;
        titleMenu.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        gameStart = false;
    }

    public void showPauseMenu() {
        _pauseMenuPanel.SetActive(true);
    }

    public void hidePauseMenu() {
        _pauseMenuPanel.SetActive(false);
    }

    public void resumeGame()
    {
        _gameManager.resumeGame();
    }

    public void goToMenu()
    {
        _gameManager.resumeGame();
        _gameManager.loadMenuScene();
    }
}
