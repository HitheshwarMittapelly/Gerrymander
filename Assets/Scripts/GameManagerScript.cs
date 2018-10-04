using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    //private float buttonDownMag = 0.05f;
    public float zoomSpeed = 0.5f;

	public static GameManagerScript instance = null;
    
    public GameObject SLF_P;

    public GameObject SLF_H;
    public GameObject SLF_E;
    public GameObject SLF_G;

    public GameObject GPP_P;
    public GameObject GPP_H;
    public GameObject GPP_G;

    

    private GameObject text;
    public GameObject minDistricts;
    public GameObject Map;
    public enum WinStates {WIN,LOSE,NONE,NEUTRAL};
	public List<GameObject> lineColliders = new List<GameObject> ();

    private List<Vector3> SLF_HPositions = new List<Vector3>();
  
    private List<Vector3> SLF_PPositions = new List<Vector3>();
    private List<Vector3> SLF_EPositions = new List<Vector3>();
    private List<Vector3> SLF_GPositions = new List<Vector3>();

    private List<Vector3> GPP_HPositions = new List<Vector3>();
   
    private List<Vector3> GPP_GPositions = new List<Vector3>();

    private List<WinStates> calculatedWinStates = new List<WinStates>();
    private bool gameWin = false;
    private int minNumOfDistricts = 4;

    public List<GameObject> activeLines = new List<GameObject>();
    public List<GameObject> activeLines2 = new List<GameObject>();

    public enum GameStates { OBJECTIVEONE,OBJECTIVETWO,NONE};
    public GameStates gameState = GameStates.NONE;

    private GenericPanelScript genericPanelScriptInstance;
    private bool autoDetection = false;
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
        gameState = GameStates.NONE;
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

        foreach (Vector3 pos in SLF_HPositions)
        {
            //Instantiate(pinkPlanet, pos, Quaternion.identity);
            Instantiate(SLF_H, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }
        foreach (Vector3 pos in SLF_PPositions)
        {
            //Instantiate(pinkPlanet, pos, Quaternion.identity);
            Instantiate(SLF_P, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }
        foreach (Vector3 pos in SLF_EPositions)
        {
            //Instantiate(pinkPlanet, pos, Quaternion.identity);
            Instantiate(SLF_E, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }
        foreach (Vector3 pos in SLF_GPositions)
        {
            //Instantiate(pinkPlanet, pos, Quaternion.identity);
            Instantiate(SLF_G, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }

        foreach (Vector3 pos in GPP_HPositions)
        {
            //Instantiate(orangePlanet, pos, Quaternion.identity);
            Instantiate(GPP_H, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }

        foreach (Vector3 pos in GPP_GPositions)
        {
            //Instantiate(greenPlanet, pos, Quaternion.identity);
            Instantiate(GPP_G, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }

        Instantiate(GPP_P, Camera.main.ViewportToWorldPoint(new Vector3(0.3f, 0.35f, 118f)),Quaternion.identity);
        //Instantiate(minDistricts, Camera.main.ViewportToWorldPoint(new Vector3(0.87f, 0.98f, 117f)),Quaternion.identity);
        ////minDistricts.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.85f, 0.98f, 117f));


        //minDistricts.GetComponent<TextMesh>().text = "Min Districts = "+ minNumOfDistricts;

        //foreach (Vector3 pos in pinkPlanetPositions)
        //{
        //     //Instantiate(pinkPlanet, pos, Quaternion.identity);
        //    Instantiate(pinkPlanet, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        //}
        //foreach (Vector3 pos in orangePlanetPositions)
        //{
        //    //Instantiate(orangePlanet, pos, Quaternion.identity);
        //    Instantiate(orangePlanet, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        //}

        //foreach (Vector3 pos in greenPlanetPositions)
        //{
        //    //Instantiate(greenPlanet, pos, Quaternion.identity);
        //    Instantiate(greenPlanet, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        //}


    }
	
	// Update is called once per frame
	void Update () 
	{
        if (gameState == GameStates.OBJECTIVEONE)
        {
            float gameWinTimes = 0f;
           
            if (activeLines.Count == 4 && !autoDetection)
            {

                //Debug.Log(activeLines.Count);
                //Debug.Log("Yayyyyy");
                autoDetection = true;
                foreach (WinStates state in calculatedWinStates)
                {
                    if (state == WinStates.WIN)
                    {
                        gameWinTimes++;
                       
                    }
                }

                if (gameWinTimes > 2)
                {
                    lineColliders.Clear();
                    activeLines.Clear();
                    genericPanelScriptInstance.bringUpObjective2();

                }
                else
                {
                    Debug.Log("gamewintimes " + gameWinTimes);
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
                    //Debug.Log("Is this what is happening");
                    genericPanelScriptInstance.bringUpRetryPanel();
                }
            }
        }
           

    }

     public void getActiveLines()
    {
        foreach (GameObject line in lineColliders)
        {
            if (!line.GetComponent<LineColliderScript>().isCalculated)
            {
                WinStates getState = line.GetComponent<LineColliderScript>().calculatePercentage();
                //Debug.Log(getState);
                if (getState != WinStates.NONE)
                {
                    calculatedWinStates.Add(getState);
                    activeLines.Add(line);
                   // Debug.Log("I am here");
                }
              
               
            }
        }
        
       
    }

  public void  getObjective2WinCond()
    {
        foreach (GameObject line in lineColliders)
        {
           // Debug.Log("count this");
            if (!line.GetComponent<LineColliderScript>().isObj2Calculated)
            {
                WinStates getState = line.GetComponent<LineColliderScript>().calculateSpeciesPer();
               // Debug.Log(getState);
                if (getState != WinStates.NONE)
                {
                    calculatedWinStates.Add(getState);
                    activeLines2.Add(line);
                   // Debug.Log("I am here");
                }


            }
        }

    }


    public void startObjectiveOne()
    {
        Instantiate(minDistricts, Camera.main.ViewportToWorldPoint(new Vector3(0.77f, 0.99f, 117f)), Quaternion.identity);
        //minDistricts.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.85f, 0.98f, 117f));
        lineColliders.Clear();
        activeLines.Clear();
        autoDetection = false;
        gameWin = false;

        minDistricts.GetComponent<TextMesh>().text = "Districts to draw = " + minNumOfDistricts;
        gameState = GameStates.OBJECTIVEONE;
        genericPanelScriptInstance = GenericPanelScript.Instance();

        
        Map.GetComponent<MapScript>().canDraw = true;
    }

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


//        if (Input.touchCount == 2)
//        {
//            Touch fingerOne = Input.GetTouch(0);
//            Touch fingerTwo = Input.GetTouch(1);
//
//            //calculate prev touch positions
//            Vector2 fingerOnePrevPos = fingerOne.position - fingerOne.deltaPosition; ;
//            Vector2 fingerTwoPrevPos = fingerTwo.position - fingerTwo.deltaPosition;
//
//            //calculate the distance bw prev touch positions and present touch positions
//            float deltaMagPrevTouch = (fingerOnePrevPos - fingerTwoPrevPos).magnitude;
//            float deltaMagTouch = (fingerOne.position - fingerTwo.position).magnitude;
//
//            //calculate the total magnitude difference
//            float deltaMagDiff = deltaMagPrevTouch - deltaMagTouch;
//            Camera.main.orthographicSize += deltaMagDiff * zoomSpeed;
//			Camera.main.orthographicSize = Mathf.Max (Camera.main.orthographicSize, 0.1f);
//			//Mathf.Clamp(Camera.main.orthographicSize,0.1f, 25f);
//			//if(Camera.main.orthographicSize > 25f)
//				
//
//        }
//
//        if (Input.GetKey(KeyCode.W))
//        {
//            buttonDownMag += 0.05f;
//            Camera.main.orthographicSize += buttonDownMag * zoomSpeed;
//            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize,0.1f, 25f);
//        }
//        if(Input.GetKey(KeyCode.S))
//        {
//            buttonDownMag += 0.05f;
//            Camera.main.orthographicSize -= buttonDownMag * zoomSpeed;
//            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
//
//        }
//        buttonDownMag = 0.05f;

//getActiveLines();

//if (activeLines.Count == 4)
//{
//    Debug.Log("Got here yayyyy");
//    //Debug.Log(calculatedWinStates.Count + " count");
//    //if (calculatedWinStates.Count > minNumOfDistricts)
//    //{
//    //    foreach (WinStates state in calculatedWinStates)
//    //    {
//    //        if (state == WinStates.WIN)
//    //        {
//    //            gameWin = true;
//    //        }
//    //    }

//    //    if (gameWin)
//    //    {
//    //        Debug.Log("You won the game");
//    //        text = new GameObject();
//    //        text.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 117f));
//    //        text.AddComponent<TextMesh>();
//    //        text.GetComponent<TextMesh>().color = Color.red;
//    //        text.GetComponent<TextMesh>().text = "You Won";

//    //        Destroy(text, 5f);
//    //    }
//    //    else
//    //        Debug.Log("You lose");
//    //}
//}
//if (Input.GetKeyDown(KeyCode.Space))
//{
//    Debug.Log("states" + calculatedWinStates.Count);
//    Debug.Log("lines " + calculatedLines.Count);

//    //foreach (GameObject line in lineColliders)
//    //{
//    //    Debug.Log(line.GetComponent<LineColliderScript>().totalCount);
//    //    //Debug.Log(activeLines.Count+" lines");
//    //    //Debug.Log(totalCount);
//    //    Debug.Log(calculatedWinStates.Count+" states");
//    //}
//}


