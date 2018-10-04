using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is attached to the background quad which is a galaxy map.
//This script uses raycast to determine the points to draw the lines.
//The touch points are updated everyframe to the line renderer script

public class MapScript : MonoBehaviour {

	public GameObject line;

    private LayerMask mask = -1;
	private Vector3 initPos;

	private GameObject lineInstance;

    public bool canDraw = false;

    private float minX, maxX, minY, maxY;
    // Use this for initialization

    void Start () 
	{
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;


    }

    // Update is called once per frame
    void Update()
    {
        if (canDraw)
        {
            //if (Input.touchCount == 1) {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 200f, mask.value))
                {
                    if (hitInfo.collider.gameObject == this.gameObject)
                    {
                        lineInstance = Instantiate(line, hitInfo.point + Vector3.up * 0.1f, Quaternion.identity, transform);

                        initPos = hitInfo.point + Vector3.up * 0.1f;

                        // Horizontal contraint (clamping out of bounds line drawing)
                        if (initPos.x < minX) initPos.x = minX;
                        if (initPos.x > maxX) initPos.x = maxX;

                        // vertical contraint
                        if (initPos.y < minY) initPos.y = minY;
                        if (initPos.y > maxY) initPos.y = maxY;

                        lineInstance.GetComponent<LineRendererScript>().addToList(initPos);
                    }

                }


            }
            //stack up the points as long as the finger is down and dragging
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.gameObject == this.gameObject)
                    {



                        Vector3 hitPosition = hitInfo.point + Vector3.up * 0.1f;
                        // Horizontal contraint
                        if (hitPosition.x < minX) hitPosition.x = minX;
                        if (hitPosition.x > maxX) hitPosition.x = maxX;

                        // vertical contraint
                        if (hitPosition.y < minY) hitPosition.y = minY;
                        if (hitPosition.y > maxY) hitPosition.y = maxY;

                        lineInstance.GetComponent<LineRendererScript>().addToList(hitPosition);


                        UpdateLine();

                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && lineInstance)
            {
                //when u release the finger add a collider to the line
                lineInstance.GetComponent<LineRendererScript>().AddColliderToLine();
                //checking whether lines drawn are equal to 4 (lines = num of districts)
                //if yes call the respective functions for objective one and two
                if (GameManagerScript.instance.lineColliders.Count >= 4)
                {
                   
                    if (GameManagerScript.instance.gameState == GameManagerScript.GameStates.OBJECTIVEONE)
                        Invoke("doThis", 2f);
                    else if (GameManagerScript.instance.gameState == GameManagerScript.GameStates.OBJECTIVETWO)
                        Invoke("doThat", 2f);
                }

                
            }
        }

    
    }
    void UpdateLine()
	{

		lineInstance.GetComponent<LineRendererScript> ().UpdateLine();


	}

    //calls Gamemanagerscript getActiveLines function
    void doThis()
    {
        GameManagerScript.instance.getActiveLines();
    }

    void doThat()
    {
        GameManagerScript.instance.getObjective2WinCond();
    }

}
