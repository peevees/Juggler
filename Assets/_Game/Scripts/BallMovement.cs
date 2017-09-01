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
    public AudioClip catchSound;
    public AudioClip[] moveSounds;
    public AudioClip crushSound;
    public GameObject leftCrash;
    public GameObject rightCrash;

    private int soundChange = 0;
    private AudioSource source;
    private GameObject aPosition;
    private float timer = 1f;
    private static bool inPlay = true;
    private bool firstStart = true;
    private float newTime = 1f;

    public bool InPlay(bool value)
    {
            return inPlay = value;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
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
            timer = newTime;            
        }
        //Debug.Log(direction);
	}
    public void playSound(int soundToPlay)
    {
        if (soundChange >=3)
        {
            soundChange = 0;
        }
        switch (soundToPlay)
        {
            case 1:
                source.PlayOneShot(catchSound, 1f);
                break;
            case 2:
                source.PlayOneShot(crushSound, 1f);
                break;
            default:
                source.PlayOneShot(moveSounds[soundChange], 1f);
                soundChange++;
                break;
        }
        
        //source.PlayOneShot(moveSound, 1f);
    }
    IEnumerator playAndInactivate()
    {
        playSound(2);
        yield return new WaitForSeconds(crushSound.length);
        gameObject.SetActive(false);
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
                playSound(1);
                gameManager.addPoint();
                newTime = gameManager.addSpeed();
                gameManager.setScoreText();
                return false;
            }else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                leftCrash.SetActive(true);
                StartCoroutine(playAndInactivate());
                
                InPlay(false);
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
                playSound(1);
                gameManager.addPoint();
                newTime = gameManager.addSpeed();
                gameManager.setScoreText();
                return false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                rightCrash.SetActive(true);
                StartCoroutine(playAndInactivate());
                InPlay(false);
                //the ball object is removed and the crashed ball sprite is to be shown
                //Debug.Log("Right Hand missed the ball");
                gameOver.SetActive(true);
                return true;
            }
        }
        else
        {
            playSound(0);
            //Debug.Log("ball is middle, meh!");
            return false;
        }
    }
}