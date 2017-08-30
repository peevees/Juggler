using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public GameManager gameManager;
    public GameObject[] ballPositions;
    public int currentPos;
    public int track;
    public int direction;
    public GameObject gameOver;

    private GameObject aPosition;
    private float timer = 1f;
    private static bool inPlay = true;
    private bool firstStart = true;

    public bool InPlay(bool value)
    {
            return inPlay = value;
    }

    void Start()
    {

        //direction = randomDirection();
        //Debug.Log(direction);
        //Debug.Log(this.gameObject.transform.position);
    } 
	void Update () {

        timer = timer - Time.deltaTime;
        if (inPlay == true && timer < 0) {
            changePosition();
            timer = 1f;
        }
        //Debug.Log(direction);
	}

    void changePosition(){
        Debug.Log(timer);
        if (firstStart == false)
        {
            checkCollision();
            if (currentPos >= ballPositions.Length - 1)
            {
                direction = -1;
            }
            else if (currentPos <= 0)
            {
                direction = 1;
            }
            getPosition();
            this.gameObject.transform.position = aPosition.transform.position;
            currentPos = currentPos + direction;
            //Debug.Log(currentPos);
        }else
        {
            if (currentPos >= ballPositions.Length - 1)
            {
                direction = -1;
            }
            else if (currentPos <= 0)
            {
                direction = 1;
            }
            getPosition();
            gameObject.transform.position = aPosition.transform.position;
            currentPos = currentPos + direction;
            //Debug.Log(currentPos);
            firstStart = false;
        }


    }
    public int BallPos()
    {
        return currentPos;
    }
    void getPosition() {
        aPosition = ballPositions[currentPos];
        //Debug.Log(aPosition.transform.position);
    }
    private int randomDirection(){
        int direct = Random.Range(-1, 1);
        if(direct == 0){
            return direct = 1;
        }
        return direct;
    }

    public bool checkCollision()
    {
        if(currentPos == 0)// check if ball is in its 0 position
        {
            //Debug.Log("ball is in left where is hand?");
            if (gameManager.GetPlayerPos() == track)
            {
                //Debug.Log("Left Hand caught the ball");
                return false;
            }else
            {
                InPlay(false);
                gameObject.SetActive(false);
                //the ball object is removed and the crashed ball sprite is to be shown
                //Debug.Log("Left Hand missed the ball");
                gameOver.SetActive(true);
                return true;
            }
        }else if (currentPos == ballPositions.Length -1)// check if ball is in its last position
        {
            //Debug.Log("ball is in right where is hand?");
            if (gameManager.GetPlayerPos() == (2-track))
            {
                //Debug.Log("Right Hand caught the ball");
                return false;
            }
            else
            {
                InPlay(false);
                gameObject.SetActive(false);
                //the ball object is removed and the crashed ball sprite is to be shown
                //Debug.Log("Right Hand missed the ball");
                gameOver.SetActive(true);
                return true;
            }
        }
        else
        {
            //Debug.Log("ball is middle, meh!");
            return false;
        }
    }
}