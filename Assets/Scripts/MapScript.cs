using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

	public GameObject line;

    private LayerMask mask = -1;
	private Vector3 initPos;

	private GameObject lineInstance;
	
	
	// Use this for initialization

	void Start () 
	{
		

		
	}

    // Update is called once per frame
    void Update()
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

                    lineInstance.GetComponent<LineRendererScript>().addToList(initPos);
                }

            }


        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject == this.gameObject)
                {
                    


                        Vector3 hitPosition = hitInfo.point + Vector3.up * 0.1f;


                        lineInstance.GetComponent<LineRendererScript>().addToList(hitPosition);


                        UpdateLine();
                    
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && lineInstance)
        {

            lineInstance.GetComponent<LineRendererScript>().AddColliderToLine();

            //lineInstance = null;
        }

        //}
    }
    void UpdateLine()
	{

		lineInstance.GetComponent<LineRendererScript> ().UpdateLine();


	}
}
