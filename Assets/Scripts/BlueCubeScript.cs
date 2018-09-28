using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlueCubeScript : MonoBehaviour {

	public string infoAbtPlanet;
	public string infoAbtFaction;
	public string infoMiscellaneous;
	public Image factionFigureHead;

	
	private GenericPanelScript genericPanelScriptInstance;

	void Awake()
	{
		genericPanelScriptInstance = GenericPanelScript.Instance ();
	}

	void Start()
	{
		infoAbtPlanet = "Write something here";
		infoAbtFaction = "More here";
		infoMiscellaneous = "Some More here";
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0))
		{

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);


            if (hit2D.collider != null)
            {
                if (hit2D.collider.gameObject == this.gameObject)
                    popInfo();
            
            }
               
  		}
	}
	public void popInfo()
	{
		genericPanelScriptInstance.bringUpInfo (infoAbtPlanet, infoAbtFaction, infoMiscellaneous, factionFigureHead);

	}

}
