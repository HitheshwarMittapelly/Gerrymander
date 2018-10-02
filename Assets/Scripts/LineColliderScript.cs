using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineColliderScript : MonoBehaviour {

	private float pinkMediumCount = 0f;
	private float greenMediumCount = 0f;
    private float orangeMediumCount = 0f;
    public bool isDestroyed = false;
	private GameObject text ;
	private GameManagerScript.WinStates winState;

	void OnTriggerEnter2D(Collider2D collider)
	{
		
		if (collider.gameObject.CompareTag ("PinkMedium")) 
		{
			pinkMediumCount++;
		}
		if (collider.gameObject.CompareTag ("GreenMedium"))
		{
			greenMediumCount++;
		}
        if (collider.gameObject.CompareTag("OrangeMedium"))
        {
            greenMediumCount++;
        }
        if (collider.gameObject.GetComponent<LineColliderScript>())
        {
            //foreach(GameObject line in GameManagerScript.instance.lineColliders)
            //{
            //    if (this.gameObject == line)
            //        GameManagerScript.instance.lineColliders.Remove(line);
            //}
            if (!isDestroyed)
            {
               
                collider.gameObject.GetComponent<LineColliderScript>().isDestroyed = true;
                Debug.Log("Destroying object : " + this.gameObject);
                GameObject removeLine = null;
                foreach (GameObject line in GameManagerScript.instance.lineColliders)
                {
                    if (this.gameObject == line)
                       removeLine = line;
                }
                GameManagerScript.instance.lineColliders.Remove(removeLine);
                Destroy(this.gameObject.transform.parent.gameObject);
                
            }
        }

	}

	void Update()
	{
		

	}

	public GameManagerScript.WinStates calculatePercentage()
	{
        float equality =  1f/(pinkMediumCount + greenMediumCount + orangeMediumCount);

        float percentage = pinkMediumCount / (pinkMediumCount + greenMediumCount + orangeMediumCount);
           

			//Debug.Log ("blue percentage = " + percentage);
			if (percentage > equality) 
			{
				winState = GameManagerScript.WinStates.WIN;
				Debug.Log ("You win");
			}
			else if (percentage == equality)
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
			
				winState = GameManagerScript.WinStates.LOSE;
				Debug.Log ("you lose");
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
