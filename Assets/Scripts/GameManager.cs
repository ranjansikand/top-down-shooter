using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int playerScore, highScore;

    private Text currentScoreText;
    private Text highScoreText;
    private Text gameOverText;
    private GameObject restartButton;

    [SerializeField] GameObject enemy;

    void Awake()
    {
        PlayerController.playerDeath += EndGame;
        EnemyController.addPoint += UpdatePlayerScore;

        // initialize score fields
        playerScore = 0;
        highScore = PlayerPrefs.GetInt("High Score", 0);

        currentScoreText 
            = GameObject.FindGameObjectWithTag("CurrentScore").GetComponent<Text>();
        highScoreText
            = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text>();
        gameOverText
            = GameObject.FindGameObjectWithTag("GameOver").GetComponent<Text>();

        restartButton = GameObject.FindGameObjectWithTag("Restart");

        // update UI
        currentScoreText.text = playerScore.ToString();
        highScoreText.text = highScore.ToString();
        gameOverText.text = "";  // should not display a message yet
        restartButton.SetActive(false);  // should not display yet

        // start spawning
        StartCoroutine("EnemySpawner");
    }

    void EndGame() 
    {
        // remove... subscriptions?
        PlayerController.playerDeath -= EndGame;
        EnemyController.addPoint -= UpdatePlayerScore;

        // stop enemy spawning
        StopCoroutine("EnemySpawner");

        // lose message
        Debug.Log("Player died!");

        // check if new high score
        if (playerScore > highScore) {
            highScore = playerScore;
            PlayerPrefs.SetInt("High Score", highScore);

            gameOverText.text = "New High Score: " + highScore.ToString();
        }
        else {
            gameOverText.text = "Game Over!";
        }

        restartButton.SetActive(true);
    }

    void UpdatePlayerScore()
    {
        playerScore++;
        currentScoreText.text = playerScore.ToString();
    }

    void SpawnEnemy() 
    {
        // left side x = -9, z = (9, -9)
        // right side x = 9, z = (9, -9)
        // top x = (-9, 9), z = 9
        // bottom x = (-9, 9), z = 9

        int spawnSide = Random.Range(1, 5);
        Vector3 spawnPosition;

        switch (spawnSide)
        {
            case 1:
                // left side
                spawnPosition = new Vector3(-9, 1.5f, Random.Range(-9f, 9f));
                break;
            
            case 2:
                // right side
                spawnPosition = new Vector3(9, 1.5f, Random.Range(-9f, 9f));
                break;
            
            case 3:
                // top
                spawnPosition = new Vector3(Random.Range(-9f, 9f), 1.5f, 9);
                break;
            
            case 4:
                // bottom
                spawnPosition = new Vector3(Random.Range(-9f, 9f), 1.5f, -9);
                break;
            
            default:
                spawnPosition = new Vector3(Random.Range(-9f, 9f), 1.5f, -9);
                break;
        }

        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(Random.Range(0.25f, 1.5f));
        }
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
