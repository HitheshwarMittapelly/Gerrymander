using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the GameManager object
public class GameManagerScript : MonoBehaviour {


	public static GameManagerScript instance = null;
    
    //GPP Faction planets
    public GameObject SLF_P;
    public GameObject SLF_H;
    public GameObject SLF_E;
    public GameObject SLF_G;

    //SLF Faction planets
    public GameObject GPP_P;
    public GameObject GPP_H;
    public GameObject GPP_G;

    
    public GameObject minDistricts;                      //Gameobject used to display the number of districts text
    public GameObject Map;                              //reference to Map (Quad background)
    public enum WinStates {WIN,LOSE,NONE,NEUTRAL};
	public List<GameObject> lineColliders = new List<GameObject> ();


    //Planet positions 
    private List<Vector3> SLF_HPositions = new List<Vector3>();
    private List<Vector3> SLF_PPositions = new List<Vector3>();
    private List<Vector3> SLF_EPositions = new List<Vector3>();
    private List<Vector3> SLF_GPositions = new List<Vector3>();

    private List<Vector3> GPP_HPositions = new List<Vector3>();
   
    private List<Vector3> GPP_GPositions = new List<Vector3>();

    private List<WinStates> calculatedWinStates = new List<WinStates>();            //winstates calculated from each district
    private bool gameWin = false;
    private int minNumOfDistricts = 4;

    public List<GameObject> activeLines = new List<GameObject>();                   //Currently active lines in the scene during the objective 1
    public List<GameObject> activeLines2 = new List<GameObject>();                  //Currently active lines in the scene during the Objective 2

    public enum GameStates { OBJECTIVEONE,OBJECTIVETWO,NONE};
    public GameStates gameState = GameStates.NONE;

    private GenericPanelScript genericPanelScriptInstance;
    private bool autoDetection = false;                                             //calculating the num of districts in every frame to check if it reached 4
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
        gameState = GameStates.NONE;

        //preload planet positions
        SLF_HPositions.Add(new Vector3(0.1f, 0.7f, 118f));
        SLF_HPositions.Add(new Vector3(0.7f, 0.1f, 118f));
        SLF_HPositions.Add(new Vector3(0.3f, 0.1f, 118f));
        SLF_HPositions.Add(new Vector3(0.3f, 0.5f, 118f));

        GPP_HPositions.Add(new Vector3(0.45f, 0.85f, 118f));
        GPP_HPositions.Add(new Vector3(0.2f, 0.6f, 118f));
        GPP_HPositions.Add(new Vector3(0.4f, 0.7f, 118f));

        SLF_PPositions.Add(new Vector3(0.3f, 0.8f, 118f));
        SLF_PPositions.Add(new Vector3(0.7f, 0.9f, 118f));
        SLF_PPositions.Add(new Vector3(0.9f, 0.7f, 118f));
        SLF_PPositions.Add(new Vector3(0.9f, 0.4f, 118f));
        SLF_PPositions.Add(new Vector3(0.1f, 0.4f, 118f));

        SLF_EPositions.Add(new Vector3(0.1f, 0.1f, 118f));
        SLF_EPositions.Add(new Vector3(0.9f, 0.1f, 118f));
        SLF_EPositions.Add(new Vector3(0.9f, 0.9f, 118f));


        SLF_GPositions.Add(new Vector3(0.5f, 0.3f, 118f));
        SLF_GPositions.Add(new Vector3(0.5f, 0.7f, 118f));

        GPP_GPositions.Add(new Vector3(0.7f, 0.6f, 118f));
        GPP_GPositions.Add(new Vector3(0.5f, 0.5f, 118f));




    }
    // Use this for initialization
    void Start () 
	{

        //Instantiate planets at the start
        foreach (Vector3 pos in SLF_HPositions)
        {
           
            Instantiate(SLF_H, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }
        foreach (Vector3 pos in SLF_PPositions)
        {
           
            Instantiate(SLF_P, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }
        foreach (Vector3 pos in SLF_EPositions)
        {
           
            Instantiate(SLF_E, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }
        foreach (Vector3 pos in SLF_GPositions)
        {
            
            Instantiate(SLF_G, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }

        foreach (Vector3 pos in GPP_HPositions)
        {
            
            Instantiate(GPP_H, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }

        foreach (Vector3 pos in GPP_GPositions)
        {
            
            Instantiate(GPP_G, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }

        Instantiate(GPP_P, Camera.main.ViewportToWorldPoint(new Vector3(0.3f, 0.35f, 118f)),Quaternion.identity);
        


    }
	
	// Update is called once per frame
	void Update () 
	{
        if (gameState == GameStates.OBJECTIVEONE)
        {
            float gameWinTimes = 0f;
           // check if current number of districts drawn is equal to 4
            if (activeLines.Count == 4 && !autoDetection)
            {

                
                autoDetection = true;
                foreach (WinStates state in calculatedWinStates)
                {
                    if (state == WinStates.WIN)
                    {
                        gameWinTimes++;
                       
                    }
                }
                //if holds majority in atleast 3 districts bring up the second objective
                if (gameWinTimes > 2)
                {
                    lineColliders.Clear();
                    activeLines.Clear();
                    genericPanelScriptInstance.bringUpObjective2();

                }
                //retry level
                else
                {
                    
                    genericPanelScriptInstance.bringUpRetryPanel();
                }
            }
        }
        if(gameState == GameStates.OBJECTIVETWO)
        {
            if (activeLines2.Count == 4 && !autoDetection)
            {
                foreach (WinStates state in calculatedWinStates)
                {
                    if (state == WinStates.WIN)
                    {
                        gameWin = true;
                    }
                }

                if (gameWin)
                {

                    genericPanelScriptInstance.bringUpVictory();

                }
                else
                {
                   
                    genericPanelScriptInstance.bringUpRetryPanel();
                }
            }
        }
           

    }

    //get the 4 active lines(districts) in the scene
     public void getActiveLines()
    {
        foreach (GameObject line in lineColliders)
        {
            if (!line.GetComponent<LineColliderScript>().isCalculated)
            {
                WinStates getState = line.GetComponent<LineColliderScript>().calculatePercentage();
               
                if (getState != WinStates.NONE)
                {
                    calculatedWinStates.Add(getState);
                    activeLines.Add(line);
                  
                }
              
               
            }
        }
        
       
    }

    //Get current active lines for the second objective and add the returned winstates to calculatedwinstates
  public void  getObjective2WinCond()
    {
        foreach (GameObject line in lineColliders)
        {
       
            if (!line.GetComponent<LineColliderScript>().isObj2Calculated)
            {
                WinStates getState = line.GetComponent<LineColliderScript>().calculateSpeciesPer();
             
                if (getState != WinStates.NONE)
                {
                    calculatedWinStates.Add(getState);
                    activeLines2.Add(line);
                   
                }


            }
        }

    }


    //stuff to do while initialising objective one
    public void startObjectiveOne()
    {
        Instantiate(minDistricts, Camera.main.ViewportToWorldPoint(new Vector3(0.77f, 0.99f, 117f)), Quaternion.identity);
        lineColliders.Clear();
        activeLines.Clear();
        autoDetection = false;
        gameWin = false;

        minDistricts.GetComponent<TextMesh>().text = "Districts to draw = " + minNumOfDistricts;
        gameState = GameStates.OBJECTIVEONE;
        genericPanelScriptInstance = GenericPanelScript.Instance();

        
        Map.GetComponent<MapScript>().canDraw = true;
    }

    //Delete all the lines in the scene
    public void deleteAllLines()
    {
        lineColliders.Clear();
        activeLines.Clear();
        autoDetection = false;
        foreach (Transform child in Map.transform)
        {
            Destroy(child.gameObject);
           
        }
    }


    //stuff to do with objective two
   public  void startObjectiveTwo()
    {
        gameState = GameStates.OBJECTIVETWO;
        lineColliders.Clear();
        activeLines.Clear();
        foreach (Transform child in Map.transform)
        {
            Destroy(child.gameObject);

        }
        Debug.Log(lineColliders.Count);
        calculatedWinStates.Clear();
        gameWin = false;
        autoDetection = false;

    }
}
