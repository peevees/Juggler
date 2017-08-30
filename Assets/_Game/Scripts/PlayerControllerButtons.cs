using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerButtons : MonoBehaviour {

    public int direction;
    public PlayerMovement playerMovement;

    void OnMouseDown() {
        //Debug.Log("touch recognised" + currentActive);
        playerMovement.MoveArms(direction);
    }

}
