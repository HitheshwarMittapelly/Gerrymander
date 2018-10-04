using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is attached to the Collider object that is instantiated as a child to the line
public class LineColliderScript : MonoBehaviour {

	private float GPPCount = 0f;                //GPP and SLF are the two factions in the game
    private float SLFCount = 0f;

    private float hCount = 0f;
    private float eCount = 0f;                  //h,e,g,p are the first letters of different species in the game (Hiteshan, Emiloops, Galenius, Plizdorp)
    private float gCount = 0f;
    private float pCount = 0f;
  
    public bool isDestroyed = false;             //A line drawn within other is destroyed
	
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
            //Destroy the first line object that is collided (A line drawn within the other gets destroyed)
            if (!isDestroyed)
            {
               
                collider.gameObject.GetComponent<LineColliderScript>().isDestroyed = true;
                Debug.Log("Destroying object : " + this.gameObject);
                GameObject removeLine = null;
                foreach (GameObject line in GameManagerScript.instance.lineColliders)           //remove the object destroyed from the list
                {
                    if (this.gameObject == line)
                       removeLine = line;
                }
                GameManagerScript.instance.lineColliders.Remove(removeLine);
                Destroy(this.gameObject.transform.parent.gameObject);
                
            }
        }

	}


    //This method is called from the gamemanager script when the current district number is 4
    //This is initially used to calculate the percentage of each faction but later changed to calculate the number of 
    //each different faction planets that are collided with this object
    public GameManagerScript.WinStates calculatePercentage()
	{
        
        isCalculated = true;
        winState = GameManagerScript.WinStates.NONE;

        if (totalCount >= 2)
        {
            //winstate only means the state in current district
            if (GPPCount > SLFCount)
            {
                winState = GameManagerScript.WinStates.WIN;
                
            }
            
            else if (GPPCount == SLFCount )
            {
                winState = GameManagerScript.WinStates.NEUTRAL;
               
            }

            else
            {

                winState = GameManagerScript.WinStates.LOSE;
               
            }
        }
        	
		return winState;
    }


    //This is for objective 2. Calculates the number of planets of each different species
    //This method is called from the gamemanager script when the current district number is 4
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
               
            }

            else if (eCount < gCount || eCount < hCount || eCount < pCount)
            {
                winState = GameManagerScript.WinStates.LOSE;
                
            }

            else if (eCount == gCount && eCount == hCount && eCount == pCount)
            {

                winState = GameManagerScript.WinStates.NEUTRAL;
               
            }
            else
            {
               winState = GameManagerScript.WinStates.NONE;
            }
        }

        
        return winState;
    }
    






}
