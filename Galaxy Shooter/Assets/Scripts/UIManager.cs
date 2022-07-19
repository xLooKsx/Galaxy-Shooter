using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Sprite[] playerImagesLives;
    public Image playerLife;
    public TextMeshProUGUI playerScoreText;
    public int score = 0;
    public Image titleMenu;
    public bool gameStart = false;
    public TextMeshProUGUI gameOverText;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    public bool gameStarted()
    {
        return gameStart;
    }

    public void gameOver()
    {
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

    }
