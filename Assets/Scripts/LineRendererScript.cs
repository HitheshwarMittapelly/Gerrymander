using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script draws the line using LineRenderer object
public class LineRendererScript : MonoBehaviour {

	private LineRenderer lineRenderer;
	
	private List<Vector3> linePoints = new List<Vector3>();

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
			
		}
		linePoints.Add (newPosition);
	}

    //Update the line with the new points added
	public void UpdateLine()
	{
		lineRenderer.positionCount = linePoints.Count;

		for(int i = lineCount; i < linePoints.Count; i++)
		{
			
			lineRenderer.SetPosition(i, linePoints[i]);
		}
		lineCount = linePoints.Count;
	}

    //Attach a Poly collider to check collision with planet objects
	public void AddColliderToLine()
	{
        //if user holds down the finger at the same position.. no line is drawn
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

        //stuff with the collider. Collider is attached to a child object of the line
		PolygonCollider2D polyCollider = new GameObject ("PolyCollider").AddComponent<PolygonCollider2D> ();
        polyCollider.gameObject.tag = "PolyCollider";

		
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

		polyCollider.points = points.ToArray ();        //This sets the poly collider 
        
       
        //Add the newly added line game object to a List in GameManager
		GameManagerScript.instance.lineColliders.Add (polyCollider.gameObject);
           
    }
   


}
