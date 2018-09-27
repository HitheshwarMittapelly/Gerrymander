using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColliderScript : MonoBehaviour {

	private float blueCount =0f;
	private float redCount = 0f;

	void OnTriggerEnter2D(Collider2D collider)
	{
		
		if (collider.gameObject.CompareTag ("Blue")) 
		{
			blueCount++;
		}
		if (collider.gameObject.CompareTag ("Red"))
		{
			redCount++;
		}

	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			float percentage = blueCount / (blueCount + redCount);

			Debug.Log ("blue percentage = " + percentage);
            if (percentage > 0.5f)
                Debug.Log("Blue wins");
            else if (percentage == 0.5f)
                Debug.Log("It's a tie");
            else
                Debug.Log("Red wins");
		}

	}


}
