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
    public int score=0;
    public Image titleMenu;
    public bool gameStart = false;
    public TextMeshProUGUI gameOverText;

    private SpawnManager spawnManager;

    private void Start()
    {
        gameOverText.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStart)
        {
            gameStart = true;
            titleMenu.enabled = false;
            titleMenu.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            spawnManager.initializeComponents();
        }
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
        gameOverText.enabled = true;
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
        gameOverText.enabled = false;        
        titleMenu.enabled = true;
        titleMenu.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        gameStart = false;
    }

    }
