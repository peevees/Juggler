using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returntoscene : MonoBehaviour {

    public void ReturnToScene()
    {
        GameManager.instance.ReturnScene();
    }
}
