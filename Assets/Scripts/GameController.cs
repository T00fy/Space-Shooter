using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject[] easyEnemies;
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
    public float difficulty;

    private bool gameOver;
    private bool restart;
    private int score;
    private bool waitForWave;
    private float waveElapsed;
    private int waveCount;
    //private GameObject[] easyEnemies;

    void Start() {
        Screen.SetResolution(600, 900, false);
        waveCount = 0;
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
                if (waveCount < 8)
                {
                    SpawnEasyEnemy(spawnPosition, spawnRotation);
                }
                else {
                    float rand = Random.Range(0, 10);
                    float modifier = waveCount / 10;
                    if (modifier > 2.5) {
                        modifier = 2.5f;
                    }
                    rand = rand + modifier;

                    if (rand < 5)
                    {
                        SpawnEasyEnemy(spawnPosition, spawnRotation);
                    }
                    else {
                        Instantiate(hazards[(Random.Range(0, hazards.Length))], spawnPosition, spawnRotation);
                    }
                }

                // Instantiate(hazards[(Random.Range(0,hazards.Length))], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            waitForWave = true;
            if (waveCount < 7)
            {
                yield return new WaitForSeconds(0);
            }
            else {
                yield return new WaitForSeconds(waveWait);
            }
            
            if (waveWait < 6) {
                waveWait = waveWait + 0.2f;
            }
            waveCount++;
            ResetWaveText();
            UpdateDifficulty();
            if (gameOver) {
                restartText.text = "Press R to restart.";
                restart = true;
                break;
            }
        }
    }

    private void UpdateDifficulty()
    {
        if (hazardCount > 15)
        {
            spawnWait = Random.Range(0.45f, 0.6f);
            hazardCount += Random.Range(0, 2);
        }
        else {
            spawnWait = spawnWait - difficulty * Random.Range(1.2f, 1.8f);
            hazardCount = hazardCount + Random.Range(1, 3);
        }

    }

    private void SpawnEasyEnemy(Vector3 spawnPosition, Quaternion spawnRotation)
    {
          Instantiate(easyEnemies[(Random.Range(0, easyEnemies.Length))], spawnPosition, spawnRotation);
    }

    private GameObject[] GetEnemies(string tag)
    {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);
        GameObject[] parentObjects = new GameObject[foundObjects.Length+1];
        for(int i = 0; i < foundObjects.Length; i++) {
            parentObjects[i] = foundObjects[i].transform.parent.gameObject;
        }
        
        return parentObjects;
            

    }

    private void ResetWaveText()
    {
        waitForWave = false;
        waveElapsed = waveWait;
        waveText.text = ""; 
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
