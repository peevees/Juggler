using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerButtons : MonoBehaviour {

    public int direction;
    public PlayerMovement playerMovement;
    
    private void Start()
    {

    }

    void OnMouseDown() {
        //Debug.Log("touch recognised" + currentActive);
        playerMovement.MoveArms(direction);
    }

}
