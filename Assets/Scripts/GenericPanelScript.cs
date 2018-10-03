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
    public Button objective2OkayButton;
    public Button pauseButton;
    public Button redoButton;
    public Button continueButton;
    public Button currentObjectiveButton;
    public Button exitGameButton;
 

    public GameObject genericPanelObject;
    public GameObject startGamePanel;
    public GameObject objectivePanel1;
    public GameObject objectivePanel2;
    public GameObject gameRunPanel;
    public GameObject pausePanel;
    private bool objectiveOneStarted = false;
    private bool objectiveTwoStarted = false;

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

    public void bringUpStartMenu()
    {
        startGamePanel.SetActive(true);
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(closeStartMenu);


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
        if (!objectiveOneStarted)
        {
            objectiveOkayButton.onClick.AddListener(startObjective1);
            objectiveOneStarted = true;
        }
        else if (objectiveOneStarted)
        {
            objectiveOkayButton.onClick.AddListener(resumeGame);
           
        }

    }

    void startObjective1()
    {
        objectivePanel1.SetActive(false);
        this.GetComponent<GameManagerScript>().startObjectiveOne();
        startGameRunPanel();
       
    }

    void startGameRunPanel()
    {
        gameRunPanel.SetActive(true);
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(bringUpPausePanel);

    }

    void bringUpPausePanel()
    {
        this.GetComponent<GameManagerScript>().Map.GetComponent<MapScript>().canDraw = false;
        pausePanel.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(resumeGame);

        currentObjectiveButton.onClick.RemoveAllListeners();
        currentObjectiveButton.onClick.AddListener(bringUpCurrentObjective);

    }
    void bringUpCurrentObjective()
    {
        if(this.GetComponent<GameManagerScript>().gameState == GameManagerScript.GameStates.OBJECTIVEONE)
        {
            bringUpObjective1();

        }
        else if(this.GetComponent<GameManagerScript>().gameState == GameManagerScript.GameStates.OBJECTIVETWO)
        {
            bringUpObjective2();
        }
    }


    void resumeGame()
    {
        objectivePanel1.SetActive(false);

        pausePanel.SetActive(false);
        this.GetComponent<GameManagerScript>().Map.GetComponent<MapScript>().canDraw = true;
    }
    public void bringUpObjective2()
    {
        objectivePanel2.SetActive(true);
        objective2OkayButton.onClick.RemoveAllListeners();
        objective2OkayButton.onClick.AddListener(startObjective2);
    }

    void startObjective2()
    {
        objectivePanel2.SetActive(false);
        this.GetComponent<GameManagerScript>().startObjectiveTwo();
    }

    public void bringUpInfo(string infoAbtPlanet, string infoAbtFaction, string infoMiscellaneous, Sprite factionFigureHead)
    {
        genericPanelObject.SetActive(true);
        this.infoAbtPlanet.text = infoAbtPlanet;
        this.infoAbtFaction.text = infoAbtFaction;
        this.infoMiscellaneous.text = infoMiscellaneous;
        this.factionFigureHead.sprite = factionFigureHead;
        okayButton.onClick.RemoveAllListeners();
        okayButton.onClick.AddListener(closeGenericPanel);


        okayButton.gameObject.SetActive(true);
    }
    void closeGenericPanel()
    {
        genericPanelObject.SetActive(false);
    }

}
