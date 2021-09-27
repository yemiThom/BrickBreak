using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int bricks = 20;
    public int score = 0;
    public int highScore, currentLvl, maxNumLvls;
    public float resetDelay = 1f;
    public Text livesTxt, scoreTxt, highScoreTxt;
    public GameObject gameOver, youWon, bricksPrefab, bricksPrefab2, bricksPrefab3, p_Paddle, deathParticles;


    private static GameManager instance;
    public static GameManager Instance{
        get{
            if(instance == null){
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    private GameObject clonePaddle;

    void Awake(){
        Setup();
    }

    public void Setup()
    {
        currentLvl = 1;
        maxNumLvls = 3;

        //clonePaddle = Instantiate(p_Paddle, transform.position, Quaternion.identity) as GameObject;
        SetupPaddle();
        Instantiate(bricksPrefab, transform.position, Quaternion.identity);

        if(PlayerPrefs.HasKey("HighScore_BB")){
            highScore = PlayerPrefs.GetInt("HighScore_BB");
        }

        UpdateScores();
    }

    public void LoseLife()
    {
        lives--;
        livesTxt.text = "Lives: " + lives;
        Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
        Destroy(clonePaddle);
        Invoke("SetupPaddle", resetDelay);
        CheckGameOver();
    }

    public void DestroyBrick(int brickScore){
        bricks--;
        AddScore(brickScore);
        CheckGameOver();
    }

    void AddScore(int bScore){
        score += bScore;

        if(score > highScore){
            highScore = score;

            PlayerPrefs.SetInt("HighScore_BB", highScore);
        }

        UpdateScores();
    }

    void UpdateScores(){
        scoreTxt.text = "Score: " + score;
        highScoreTxt.text = "High Score: " + highScore;
    }

    void CheckGameOver()
    {
        if (bricks < 1)
        {
            if(currentLvl > maxNumLvls){
                youWon.SetActive(true);
                Time.timeScale = 0.25f;
                Invoke("Reset", resetDelay);
            }
            
            currentLvl++;
            SetupNextLEvel();
            
        }

        if (lives < 1)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0.25f;
            Invoke("Reset", resetDelay);
        }
    }

    void SetupNextLEvel(){
        clonePaddle = null;
        Destroy(GameObject.Find("PlayerBall"));
        Destroy(GameObject.Find("PlayerPaddle(Clone)"));
        SetupPaddle();
        bricks = 20;
        lives += 3;

        switch(currentLvl){
            case 2:
                Debug.Log("Current Level is: " + currentLvl);
                Destroy(GameObject.Find("Bricks(Clone)"));
                Instantiate(bricksPrefab2, transform.position, Quaternion.identity);
                break;
            case 3:
                Debug.Log("Current Level is: " + currentLvl);
                Destroy(GameObject.Find("Bricks2(Clone)"));
                Instantiate(bricksPrefab3, transform.position, Quaternion.identity);
                break;
        }
    }

    void SetupPaddle()
    {
        clonePaddle = Instantiate(p_Paddle, transform.position, Quaternion.identity) as GameObject;
    }

    void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
