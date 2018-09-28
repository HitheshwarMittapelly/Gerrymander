using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GenericPanelScript : MonoBehaviour {

	public Text infoAbtPlanet;
	public Text infoAbtFaction;
	public Text infoMiscellaneous;
	public Image factionFigureHead;
	public Button okayButton;
	public GameObject genericPanelObject;

	private static GenericPanelScript genericPanelScript;

	public static GenericPanelScript Instance()
	{
		if (!genericPanelScript) {
			genericPanelScript = FindObjectOfType (typeof(GenericPanelScript)) as GenericPanelScript;
		}

		return genericPanelScript;

	}

	public void bringUpInfo(string infoAbtPlanet,string infoAbtFaction,string infoMiscellaneous,Image factionFigureHead)
	{
		genericPanelObject.SetActive (true);
		this.infoAbtPlanet.text = infoAbtPlanet;
		this.infoAbtFaction.text = infoAbtFaction;
		this.infoMiscellaneous.text = infoMiscellaneous;
		this.factionFigureHead = factionFigureHead;
		okayButton.onClick.RemoveAllListeners ();
		okayButton.onClick.AddListener (ClosePanel);

		okayButton.gameObject.SetActive (true);
	}

	void ClosePanel()
	{
		genericPanelObject.SetActive (false);
	}

}
