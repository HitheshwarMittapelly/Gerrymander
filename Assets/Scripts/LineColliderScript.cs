using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineColliderScript : MonoBehaviour {

	private float GPPCount = 0f;
    private float hCount = 0f;
    private float eCount = 0f;
    private float gCount = 0f;
    private float pCount = 0f;
    private float SLFCount = 0f;
    public bool isDestroyed = false;
	private GameObject text ;
	private GameManagerScript.WinStates winState;
    public bool isCalculated = false;
    public int totalCount = 0;
    
    public bool isObj2Calculated = false;


    void OnTriggerEnter2D(Collider2D collider)
	{
		
		if (collider.gameObject.CompareTag ("GPP")) 
		{
			GPPCount++;
            totalCount++;
            if (collider.gameObject.GetComponent<PlanetScript>().species == "Hiteshan")
                hCount++;
            else if (collider.gameObject.GetComponent<PlanetScript>().species == "Galenius")
                gCount++;
            else if (collider.gameObject.GetComponent<PlanetScript>().species == "Plizdorp")
                pCount++;
            
        }
		if (collider.gameObject.CompareTag ("SLF"))
		{
			SLFCount++;
            totalCount++;
            if (collider.gameObject.GetComponent<PlanetScript>().species == "Hiteshan")
                hCount++;
            else if (collider.gameObject.GetComponent<PlanetScript>().species == "Galenius")
                gCount++;
            else if (collider.gameObject.GetComponent<PlanetScript>().species == "Plizdorp")
                pCount++;
            else if (collider.gameObject.GetComponent<PlanetScript>().species == "Emiloops")
                eCount++;
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
        //Debug.Log(totalCount);
        isCalculated = true;
        winState = GameManagerScript.WinStates.NONE;

        if (totalCount >= 2)
        {
           
            if (GPPCount > SLFCount)
            {
                winState = GameManagerScript.WinStates.WIN;
                //Debug.Log ("You win");
            }
            
            else if (GPPCount == SLFCount )
            {
                winState = GameManagerScript.WinStates.NEUTRAL;
                //Debug.Log ("It's a tie");
            }

            else
            {

                winState = GameManagerScript.WinStates.LOSE;
                //Debug.Log ("you lose");
            }
        }

		//text = new GameObject();
		//text.transform.position = this.transform.parent.GetComponent<LineRendererScript>().textPosition;
		//text.AddComponent<TextMesh> ();
		//text.GetComponent<TextMesh> ().color = Color.red;
		//text.GetComponent<TextMesh>().text = winState.ToString();

		//Destroy (text, 5f);
		return winState;
    }

    public GameManagerScript.WinStates calculateSpeciesPer()
    {
        totalCount = (int)(hCount + gCount + pCount + eCount);
        winState = GameManagerScript.WinStates.NONE;
        isObj2Calculated = true;
        if (totalCount >= 2)
        {

            if (eCount > gCount && eCount > hCount && eCount > pCount)
            {
                winState = GameManagerScript.WinStates.WIN;
                //Debug.Log("You win");
            }

            else if (eCount < gCount || eCount < hCount || eCount < pCount)
            {
                winState = GameManagerScript.WinStates.LOSE;
                //Debug.Log("It's a lose");
            }

            else if (eCount == gCount && eCount == hCount && eCount == pCount)
            {

                winState = GameManagerScript.WinStates.NEUTRAL;
               // Debug.Log("you tie");
            }
            else
            {
                Debug.Log("haha");
                Debug.Log(eCount);
                Debug.Log(gCount);
                Debug.Log(pCount);
                Debug.Log(hCount);
                winState = GameManagerScript.WinStates.NONE;
            }
        }

        //text = new GameObject();
        //text.transform.position = this.transform.parent.GetComponent<LineRendererScript>().textPosition;
        //text.AddComponent<TextMesh> ();
        //text.GetComponent<TextMesh> ().color = Color.red;
        //text.GetComponent<TextMesh>().text = winState.ToString();

        //Destroy (text, 5f);
        return winState;
    }
    //pinkPlanetPositions.Add(new Vector3(0.1f, 0.9f, 0));
    //    pinkPlanetPositions.Add(new Vector3(0.9f, 0.1f, 0));
    //    pinkPlanetPositions.Add(new Vector3(0.6f, 0.75f, 0));
    //    orangePlanetPositions.Add(new Vector3(0.25f, 0.55f, 0));
    //    orangePlanetPositions.Add(new Vector3(0.7f, 0.4f, 0));
    //    orangePlanetPositions.Add(new Vector3(0.75f,0.3f, 0));
    //    greenPlanetPositions.Add(new Vector3(0.4f, 0.6f, 0));
    //    greenPlanetPositions.Add(new Vector3(0.3f, 0.2f, 0));
    //    greenPlanetPositions.Add(new Vector3(0.8f, 0.8f, 0));
    //else if (pinkMediumCount == 0f && orangeMediumCount == 0 && greenMediumCount == 0)
    //        {
    //            winState = GameManagerScript.WinStates.NONE;
    //            //Debug.Log ("Try again");
    //        }






}
