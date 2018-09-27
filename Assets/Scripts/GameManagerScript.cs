using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    private float buttonDownMag = 0.05f;
    public float zoomSpeed = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.touchCount == 2)
        //{
        //    Touch fingerOne = Input.GetTouch(0);
        //    Touch fingerTwo = Input.GetTouch(1);

        //    //calculate prev touch positions
        //    Vector2 fingerOnePrevPos = fingerOne.position - fingerOne.deltaPosition; ;
        //    Vector2 fingerTwoPrevPos = fingerTwo.position - fingerTwo.deltaPosition;

        //    //calculate the distance bw prev touch positions and present touch positions
        //    float deltaMagPrevTouch = (fingerOnePrevPos - fingerTwoPrevPos).magnitude;
        //    float deltaMagTouch = (fingerOne.position - fingerTwo.position).magnitude;

        //    //calculate the total magnitude difference
        //    float deltaMagDiff = deltaMagPrevTouch - deltaMagTouch;
        //    Camera.main.orthographicSize += deltaMagDiff * zoomSpeed;
        //     Mathf.Clamp(Camera.main.orthographicSize,0.1f, 1f);

        //}

        if (Input.GetKey(KeyCode.W))
        {
            buttonDownMag += 0.05f;
            Camera.main.orthographicSize += buttonDownMag * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize,0.1f, 25f);
        }
        if(Input.GetKey(KeyCode.S))
        {
            buttonDownMag += 0.05f;
            Camera.main.orthographicSize -= buttonDownMag * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);

        }
        buttonDownMag = 0.05f;

    }
}
