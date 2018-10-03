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
    public Button startButton;
    public Button objectiveOkayButton;

	public GameObject genericPanelObject;
    public GameObject startGamePanel;
    public GameObject objectivePanel1;
    public GameObject objectivePanel2;

	private static GenericPanelScript genericPanelScript;

    private void Start()
    {
        bringUpStartMenu();
    }

    public static GenericPanelScript Instance()
	{
		if (!genericPanelScript) {
			genericPanelScript = FindObjectOfType (typeof(GenericPanelScript)) as GenericPanelScript;
		}

		return genericPanelScript;

	}

	public void bringUpInfo(string infoAbtPlanet,string infoAbtFaction,string infoMiscellaneous,Sprite factionFigureHead)
	{
		genericPanelObject.SetActive (true);
		this.infoAbtPlanet.text = infoAbtPlanet;
		this.infoAbtFaction.text = infoAbtFaction;
		this.infoMiscellaneous.text = infoMiscellaneous;
		this.factionFigureHead.sprite = factionFigureHead;
		okayButton.onClick.RemoveAllListeners ();
		okayButton.onClick.AddListener (closeGenericPanel);
       

		okayButton.gameObject.SetActive (true);
	}
    public void bringUpStartMenu()
    {
        startGamePanel.SetActive(true);
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(closeStartMenu);
        

    }
	void closeGenericPanel()
	{
		genericPanelObject.SetActive (false);
	}
    void closeStartMenu()
    {
        startGamePanel.SetActive(false);
        bringUpObjective1();
    }
    public void bringUpObjective1()
    {
        objectivePanel1.SetActive(true);
        objectiveOkayButton.onClick.RemoveAllListeners();
        objectiveOkayButton.onClick.AddListener(startObjective1);

    }
    void startObjective1()
    {
        objectivePanel1.SetActive(false);
        this.GetComponent<GameManagerScript>().startObjectiveOne();
    }

}
