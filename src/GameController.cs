using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues; // We need this to specify the coordinates where our game objects can be spawned
    public int hazardCount; // Max number of hazards to be created 
    public float spawnWait; // Spawn Interval 
    public float startWait; // Time before waves start 
    public float waveWait; // Time before next wave

    // This is for UI score keeping and game texts 
    public GameObject scoreText; // ScoreText UI object 
    public GameObject gameOverText; // Game Over UI object 
    public GameObject restartText; // Restart UI object 

    private int score; // Current Score 
    private bool gameOver; // True if the game is over 
    private bool restart; // True if can restart

    void Start()
    {
        // Value Init 
        score = 0;
        gameOver = false;
        restart = false;
        restartText.GetComponent<Text>().text = "";
        gameOverText.GetComponent<Text>().text = "";
        UpdateScore(); // Set Score on init 
                       // Corroutines allow us to call the same functions at intervals 
        StartCoroutine(SpawnWaves());
    }

    // Check if Restart button is pressed 
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Reloads the scene 
                SceneManager.LoadScene("Main");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait); // Waits for startWait seconds before continuing the function 
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                // We want to spawn the asteroids at a random location along the x-axis 
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

                // Creating a hazard with 0 rotation 
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            // Pause the loop for waveWait seconds then begin next wave

            // Showing Restart Text after waves end 
            if (gameOver)
            {
                restartText.GetComponent<Text>().text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    // This is for UI updating 
    public void AddScore(int newScoreValue)
    {
        // Only update score if the game is not over 
        if (!gameOver)
        {
            score += newScoreValue;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        scoreText.GetComponent<Text>().text = "Score: " + score;
    }

    // This handles UI on showing Game over 
    public void GameOver()
    {
        gameOverText.GetComponent<Text>().text = "Game Over!";
        gameOver = true;
    }

}