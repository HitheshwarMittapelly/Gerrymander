﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineColliderScript : MonoBehaviour {

	private float blueCount = 0f;
	private float redCount = 0f;
	private GameObject text ;
	private GameManagerScript.WinStates winState;

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
		

	}

	public GameManagerScript.WinStates calculatePercentage()
	{
		
			float percentage = blueCount / (blueCount + redCount);

			Debug.Log ("blue percentage = " + percentage);
			if (percentage > 0.5f) 
			{
				winState = GameManagerScript.WinStates.BLUE;
				Debug.Log ("Blue wins");
			}
			else if (percentage == 0.5f)
			{
				winState = GameManagerScript.WinStates.NEUTRAL;
				Debug.Log ("It's a tie");
			}
		else if (float.IsNaN(percentage)) 
			{
				winState = GameManagerScript.WinStates.NONE;
				Debug.Log ("Try again");
			} 
			else 
			{
			
				winState = GameManagerScript.WinStates.RED;
				Debug.Log ("Red wins");
			}

		text = new GameObject();
		text.transform.position = this.transform.parent.GetComponent<LineRendererScript>().textPosition;
		text.AddComponent<TextMesh> ();
		text.GetComponent<TextMesh> ().color = Color.red;
		text.GetComponent<TextMesh>().text = winState.ToString();

		Destroy (text, 5f);
		return winState;
	}






}
