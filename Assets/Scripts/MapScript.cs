using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

	public GameObject line;

	
	private Vector3 initPos;

	private GameObject lineInstance;
	
	
	// Use this for initialization

	void Start () 
	{
		

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast (ray, out hitInfo)) 
			{
				
				lineInstance = Instantiate (line, hitInfo.point + Vector3.up * 0.1f, Quaternion.identity, transform);
			
				initPos = hitInfo.point + Vector3.up * 0.1f;

				lineInstance.GetComponent<LineRendererScript> ().addToList (initPos);


			}


		}
		if (Input.GetMouseButton (0) ) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast (ray, out hitInfo)) 
			{
				
				Vector3 hitPosition = hitInfo.point + Vector3.up * 0.1f;


				lineInstance.GetComponent<LineRendererScript> ().addToList (hitPosition);
				

				UpdateLine ();

			}
		}
		if (Input.GetMouseButtonUp (0) && lineInstance) {
			lineInstance.GetComponent<LineRendererScript> ().AddColliderToLine();
			//lineInstance = null;
		}


	}
	void UpdateLine()
	{

		lineInstance.GetComponent<LineRendererScript> ().UpdateLine();

//		//lineRenderer.startWidth = startWidth;
//		//lineInstance.GetComponent<LineRenderer>().positionCount = linePoints.Count;
//		lineRenderer.positionCount = linePoints.Count;
//
//		for(int i = lineCount; i < linePoints.Count; i++)
//		{
//			//lineInstance.GetComponent<LineRenderer>().SetPosition(i, linePoints[i]);
//			lineRenderer.SetPosition(i, linePoints[i]);
//		}
//		lineCount = linePoints.Count;
	}
}
