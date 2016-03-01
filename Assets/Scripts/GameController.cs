using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GUIText scoreText;
    public GUIText waveText;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;
    private bool waitForWave;
    private float waveElapsed;

    void Start() {
        gameOver = false;
        restart = false;
        waitForWave = false;
        waveElapsed = waveWait;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());  //A coroutine is a function that can suspend its execution (yield) until the given YieldInstruction finishes.
        
    }

    void Update() {
        if (restart) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene("Main");

            }

        }


        if (waitForWave){
            UpdateWaveTime();

        }


    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity; //no rotation at all
                Instantiate(hazards[(Random.Range(0,hazards.Length))], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            waitForWave = true;
            yield return new WaitForSeconds(waveWait);
            ResetWaveText();
            if (gameOver) {
                restartText.text = "Press R to restart.";
                restart = true;
                break;
            }
        }
    }

    private void ResetWaveText()
    {
        waitForWave = false;
        waveElapsed = waveWait;
        waveText.text = "Next Wave: "; 
    }

    private void UpdateWaveTime()
    {
        waveElapsed = waveElapsed - Time.deltaTime;
        if (waveElapsed > 0)
        {
            waveText.text = "Next Wave: " + waveElapsed.ToString("0.00");
        }
        
        
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }      

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }
    public void GameOver() {
        gameOverText.text = "Game Over.";
        gameOver = true;

    }
}
