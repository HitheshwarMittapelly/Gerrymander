using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    //private float buttonDownMag = 0.05f;
    public float zoomSpeed = 0.5f;

	public static GameManagerScript instance = null;
    public GameObject pinkPlanet;
    public GameObject orangePlanet;
    public GameObject greenPlanet;
    private GameObject text;
    public GameObject minDistricts;
    public GameObject Map;
    public enum WinStates {WIN,LOSE,NONE,NEUTRAL};
	public List<GameObject> lineColliders = new List<GameObject> ();

    private List<Vector3> pinkPlanetPositions = new List<Vector3>();
    private List<Vector3> orangePlanetPositions = new List<Vector3>();
    private List<Vector3> greenPlanetPositions = new List<Vector3>();
    private List<WinStates> calculatedWinStates = new List<WinStates>();
    private bool gameWin = false;
    private int minNumOfDistricts = 2;

    private List<GameObject> activeLines = new List<GameObject>();
    private List<GameObject> calculatedLines=new List<GameObject>();
    public enum GameStates { OBJECTIVEONE,OBJECTIVETWO,NONE};
    public GameStates gameState = GameStates.NONE;


    private float totalCount = 0f;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
        gameState = GameStates.NONE;
        pinkPlanetPositions.Add(new Vector3(0.1f, 0.9f, 118f));
        pinkPlanetPositions.Add(new Vector3(0.9f, 0.1f, 118f));
        pinkPlanetPositions.Add(new Vector3(0.6f, 0.75f, 118f));
        orangePlanetPositions.Add(new Vector3(0.25f, 0.55f, 118f));
        orangePlanetPositions.Add(new Vector3(0.7f, 0.4f, 118f));
        orangePlanetPositions.Add(new Vector3(0.45f, 0.85f, 118f));
        greenPlanetPositions.Add(new Vector3(0.4f, 0.6f, 118f));
        greenPlanetPositions.Add(new Vector3(0.3f, 0.2f, 118f));
        greenPlanetPositions.Add(new Vector3(0.8f, 0.8f, 118f));
    }
	// Use this for initialization
	void Start () 
	{
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

            getActiveLines();
            //Debug.Log(activeLines.Count);
            if (activeLines.Count == 4)
            {
                Debug.Log(activeLines.Count);
                Debug.Log("Yayyyyy");
            }
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("states" + calculatedWinStates.Count);
                Debug.Log("lines " + calculatedLines.Count);
               
                //foreach (GameObject line in lineColliders)
                //{
                //    Debug.Log(line.GetComponent<LineColliderScript>().totalCount);
                //    //Debug.Log(activeLines.Count+" lines");
                //    //Debug.Log(totalCount);
                //    Debug.Log(calculatedWinStates.Count+" states");
                //}
            }
        }
        

    }

    void getActiveLines()
    {
        foreach (GameObject line in lineColliders)
        {
            if (!line.GetComponent<LineColliderScript>().isCalculated)
            {
                WinStates getState = line.GetComponent<LineColliderScript>().calculatePercentage();
                //if (getState != WinStates.NONE)
                    calculatedWinStates.Add(getState);
                    calculatedLines.Add(line);
              
                //calculatedWinStates.Add(getState);
                // totalCount = line.GetComponent<LineColliderScript>().totalCount;
                //if (line.GetComponent<LineColliderScript>().totalCount >= 2f)
                //{
                //    Debug.Log("Am i ever here");
                //    calculatedWinStates.Add(getState);
                //    activeLines.Add(line);
                //}
            }
        }
        if(calculatedLines.Count>=2)
            getFromStates();
       
    }

    void getFromStates()
    {
        foreach(GameObject line in calculatedLines)
        {
            if(line.GetComponent<LineColliderScript>().totalCount>=2f)
                activeLines.Add(line);
        }
       
    }
    public void startObjectiveOne()
    {
        Instantiate(minDistricts, Camera.main.ViewportToWorldPoint(new Vector3(0.85f, 0.98f, 117f)), Quaternion.identity);
        //minDistricts.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.85f, 0.98f, 117f));


        minDistricts.GetComponent<TextMesh>().text = "Min Districts = " + minNumOfDistricts;
        gameState = GameStates.OBJECTIVEONE;
      

        foreach (Vector3 pos in pinkPlanetPositions)
        {
            //Instantiate(pinkPlanet, pos, Quaternion.identity);
            Instantiate(pinkPlanet, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }
        foreach (Vector3 pos in orangePlanetPositions)
        {
            //Instantiate(orangePlanet, pos, Quaternion.identity);
            Instantiate(orangePlanet, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }

        foreach (Vector3 pos in greenPlanetPositions)
        {
            //Instantiate(greenPlanet, pos, Quaternion.identity);
            Instantiate(greenPlanet, Camera.main.ViewportToWorldPoint(pos), Quaternion.identity);
        }
        Map.GetComponent<MapScript>().canDraw = true;
    }

   public  void startObjectiveTwo()
    {
        gameState = GameStates.OBJECTIVETWO;
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
