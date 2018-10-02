﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour {

	private LineRenderer lineRenderer;
	//private Vector3 initPos;
	private List<Vector3> linePoints = new List<Vector3>();
    public Vector3 textPosition;
	int lineCount = 0;
	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer> ();
	}


	public void addToList(Vector3 newPosition)
	{
		if (linePoints == null) {
			linePoints = new List<Vector3> ();
			linePoints.Add (newPosition);
			//initPos = newPosition;
		}
		linePoints.Add (newPosition);
	}
	public void UpdateLine()
	{
		lineRenderer.positionCount = linePoints.Count;

		for(int i = lineCount; i < linePoints.Count; i++)
		{
			//lineInstance.GetComponent<LineRenderer>().SetPosition(i, linePoints[i]);
			lineRenderer.SetPosition(i, linePoints[i]);
		}
		lineCount = linePoints.Count;
	}

	public void AddColliderToLine()
	{
        bool isAtSamePlace = true;
        for (int i = 1; i < linePoints.Count; i++)
        {
            if (Vector3.Distance(linePoints[1], linePoints[i-1]) != 0f)
               isAtSamePlace = false;

        }
        if (isAtSamePlace)
        {
            
            Destroy(gameObject);
            return;
        }
		PolygonCollider2D polyCollider = new GameObject ("PolyCollider").AddComponent<PolygonCollider2D> ();
        polyCollider.gameObject.tag = "PolyCollider";

		//this.gameObject.AddComponent<PolygonCollider2D>();
		//PolygonCollider2D polyCollider = this.gameObject.GetComponent<PolygonCollider2D> ();
		//lineRenderer.useWorldSpace = false;

		//Uncomment from here
		//polyCollider.transform.position = this.transform.position;
		polyCollider.transform.parent = this.gameObject.transform;
		polyCollider.transform.localPosition = Vector3.zero;
		float zValue = this.gameObject.transform.position.z - polyCollider.transform.position.z;
		polyCollider.transform.position = new Vector3 (polyCollider.transform.position.x, polyCollider.transform.position.y, zValue);
		polyCollider.isTrigger = true;
		polyCollider.gameObject.AddComponent<Rigidbody2D> ();
		polyCollider.gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0f;
		polyCollider.gameObject.GetComponent<Rigidbody2D> ().collisionDetectionMode=  CollisionDetectionMode2D.Continuous;
		polyCollider.gameObject.AddComponent<LineColliderScript> ();
		List<Vector2> points = new List<Vector2> ();
		for (int i = 0; i < linePoints.Count; i++) {

			points.Add ((new Vector2 (linePoints [i].x, linePoints [i].y)));
		}
		polyCollider.offset = new Vector2 (-points [0].x,-points[0].y);

		polyCollider.points = points.ToArray ();
        float max = 0.1f;
        float distance;
        Vector3 maxPosition = Vector3.zero;
        for(int i = 1; i < linePoints.Count; i++)
        {
            distance = Vector3.Distance(linePoints[0], linePoints[i]);
            if (distance > max)
            {
                max = distance;
                maxPosition = linePoints[i];
            }
        }
        

        textPosition = (maxPosition + (linePoints[0])) * 0.5f;
        textPosition.z = -1f;
        
		GameManagerScript.instance.lineColliders.Add (polyCollider.gameObject);



	}



//	BoxCollider lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider>();
//	lineCollider.transform.parent = this.gameObject.transform;
//	float lineWidth = lineRenderer.endWidth;
//	float max = 1f;
//	Vector3 maxPoint = initPos;
//	foreach (Vector3 p in linePoints) 
//	{
//		float distance = Vector3.Distance (initPos, p);
//		if (distance > max) {
//			max = distance;
//			maxPoint = p;
//		}
//
//	}
//	float lineLength = max;
//	lineCollider.size = new Vector3 (lineLength, lineWidth, 1f);
//	Vector3 midpoint = (initPos + maxPoint) / 2;
//
//	lineCollider.transform.position = midpoint;
//	Debug.Log ("midpoint = " + midpoint);
//	Debug.Log ("initPos = " + initPos);
//	Debug.Log ("maxpoint = " + maxPoint);
//
//
//	float angle = Mathf.Atan2 (maxPoint.z - initPos.z, maxPoint.x - initPos.x);
//	angle *= Mathf.Rad2Deg;
//	angle *= -1;
//	lineCollider.transform.Rotate (0f, angle, 0f);

}
