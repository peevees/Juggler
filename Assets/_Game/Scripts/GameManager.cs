﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public BallMovement ballMovement;
    public PlayerMovement playerMovement;
    public GameObject ballFurthest;
    public GameObject ballMiddle;
    public GameObject ballNearest;
    public Text scoreText;
    public Text highScoreText;
    public Text highScoreText2;
    public Button game1;
    public Button game2;
    public static GameManager instance = null;
    public AnimationCurve speedCurve;
    
    private Button game1Button;
    private Button game2Button;
    private System.DateTime timeunformat;
    private string time;
    private float timer = 1f;
    private bool clock;
    private Scene scene;
    private int score = 0;
    private int highScore;
    private int highScore2;
    private float speed = 1f;

    // Use this for initialization
    void Start(){
        //PlayerPrefs.DeleteAll();//erases all prefs
        highScore = PlayerPrefs.GetInt("High Score");
        highScore2 = PlayerPrefs.GetInt("High Score2");
        //TODO under construction save+ score to preferences 
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Main_menu")
        {
            setHighScoreText();
            setHighScoreText2();
            playerMovement.SetIsAi(true);
            timeunformat = new System.DateTime();
            getTime();
            setScoreText();
            
        }
        else
        {
            Debug.Log("scene is: " + scene.name);
            playerMovement.SetIsAi(false);
            setScoreText();
        }
        

    }
    void Awake()
    {
        
        //Check if instance already exists
        if (instance == null) { 

            //if not, set instance to this
            instance = this;

            //If instance already exists and it's not this:
        }
        else if (instance != this) { 

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {

        if (playerMovement.GetIsAi() == true)
        {

            int ball = BallLocation();

            if (ball <= 2)
            {
                playerMovement.MoveArms(-1);
            }
            else if (ball >= 10)
            {
                playerMovement.MoveArms(1);
            }
            //updates the score text to show current system time
            timer = timer - Time.deltaTime;
            if (timer < 0)
            {
                getTime();
                setScoreText();
                timer = 10f;
            }
        }
        else
        {

        }

    }
    public void addPoint()
    {
        if (scene.name == "GameScene")
        {
            score++;
        }else if (scene.name == "GameScene2")
        {
            score += 10;
        }
        
    }
    public void LoadScene()
    {
        SceneManager.UnloadSceneAsync("Main_Menu");
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
        scoreText.text = "";
    }

    //HACK
    public void LoadScene2()
    {
        SceneManager.UnloadSceneAsync("Main_Menu");
        SceneManager.LoadSceneAsync("GameScene2", LoadSceneMode.Single);
        scoreText.text = "";
    }
    
    public void ReturnScene()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            setHighScore();
            SceneManager.UnloadSceneAsync("GameScene");
        }else if (SceneManager.GetActiveScene().name == "GameScene2")
        {
            setHighScore2();
            SceneManager.UnloadSceneAsync("GameScene2");
        }

        SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
        ballMovement.InPlay(true);
        playerMovement.SetIsAi(true);
        scoreText.text = "";
    }

    //HACK
    public void setHighScore()
    {
        if(score > highScore)
        {
            Debug.Log("score is: " + score + "HighScore is: " + highScore);
            highScore = score;
            Debug.Log("highscore is now: " + highScore);
            PlayerPrefs.SetInt("High Score", highScore);
        }    
    }
    public void setHighScore2()
    {
        if (score > highScore2)
        {
            Debug.Log("score is: " + score + "HighScore is: " + highScore2);
            highScore2 = score;
            Debug.Log("highscore is now: " + highScore2);
            PlayerPrefs.SetInt("High Score2", highScore2);
        }
    }
    public void setHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }
    public void setHighScoreText2()
    {
        highScoreText2.text = "High Score: " + highScore2.ToString();
    }
    public void setScoreText()
    {
        if (scene.name == "Main_menu")
        {
            scoreText.text = time;
        }else
        {
            scoreText.text = score.ToString();
        }
        
    }
    public void getTime()
    {
        timeunformat = System.DateTime.Now;
        time = timeunformat.ToString("HH:mm");
        //Debug.Log(time);
    }
    public int GetPlayerPos()
    {
        return playerMovement.HandPosition();
    }
    public int BallLocation(){
        return ballMovement.currentPos;
    }
    public float addSpeed()
    {
        if (scene.name == "GameScene")
        {
            if (score % 5 == 0)
            {
                if (speed >= 0.3)
                {
                    speed = speed - speedCurve.Evaluate(score / 100f);
                    Debug.Log("speed is: " + speed);
                }
                else
                {
                    speed = 1f;
                }
            }
        }
        else if (scene.name == "GameScene2")
        {
            if (score % 50 == 0)
            {
                if (speed >= 0.3)
                {
                    speed = speed - speedCurve.Evaluate(score / 100f);
                    Debug.Log("speed is: " + speed);
                }
                else
                {
                    speed = 1f;
                }
            }
        }
        else
        {
            speed = 1f;
        }
            return speed;
    }
}
