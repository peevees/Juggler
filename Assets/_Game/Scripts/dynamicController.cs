using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Debug.Log("" + i);
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                Debug.Log(ray);
            }
        }

        //Debug.Log (Input.GetTouch().position.ToString);
        //Vector2 movement = new Vector2();
    }
}