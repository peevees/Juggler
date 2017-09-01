using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] Arms = new GameObject[3];

    private bool aiJuggler = true;

    private static int currentActive = 1; //active state for arm and leg placement is individual because of static
    public int HandPosition()
    {
        //Debug.Log(currentActive);
        return currentActive;
    }

    public void SetIsAi(bool condition)
    {
        //Debug.Log(condition);
        Arms[currentActive].SetActive(false);
        currentActive = 1;
        Arms[currentActive].SetActive(true);
        aiJuggler = condition;
    }
    public bool GetIsAi()
    {
        //Debug.Log(aiJuggler);
        return aiJuggler;
    }

    public void MoveArms(int direction)
    {
            Arms[currentActive].SetActive(false);
            //Debug.Log(currentActive);
            currentActive += direction;
            //Debug.Log(currentActive);
            switch (currentActive)
            {
                case 0:
                    Arms[currentActive].SetActive(true);
                    break;
                case 1:
                    Arms[currentActive].SetActive(true);
                    break;
                case 2:
                    Arms[currentActive].SetActive(true);
                    break;
                default:
                    //Debug.Log("invalid Move");
                    currentActive -= direction;
                    Arms[currentActive].SetActive(true);
                    break;
            }
    }
}