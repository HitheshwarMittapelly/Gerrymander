using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GenericPanelScript : MonoBehaviour {
    
    //References to various buttons in the scene panels
	public Button okayButton;
    public Button startButton;
    public Button objectiveOkayButton;
    public Button objective2OkayButton;
    public Button pauseButton;
    public Button redoButton;
    public Button continueButton;
    public Button currentObjectiveButton;
    public Button exitGameButton;
    public Button retryButton;
    public Button exitButton;

    //All the panel objects
    public GameObject genericPanelObject;
    public GameObject startGamePanel;
    public GameObject objectivePanel1;
    public GameObject objectivePanel2;
    public GameObject gameRunPanel;
    public GameObject pausePanel;
    public GameObject retryPanel;
    public GameObject victoryPanel;

    //sounds to be played
    public AudioClip objectivePopUp;
    public AudioClip objectivePopDown;
    public AudioClip retry;


    private bool objectiveOneStarted = false;
    private bool objectiveTwoStarted = false;


    //singleton pattern
    private GameManagerScript gameManagerInstance;
    private static GenericPanelScript genericPanelScript;

    private void Start()
    {
        gameManagerInstance = GameManagerScript.instance;
        bringUpStartMenu();
    }

    public static GenericPanelScript Instance()
	{
		if (!genericPanelScript) {
			genericPanelScript = FindObjectOfType (typeof(GenericPanelScript)) as GenericPanelScript;
		}

		return genericPanelScript;

	}
    //All the panel related functions are self explanatory
    //load the start menu at the very start of the game
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

    //Pop up objective one panel and stuff related with it
    public void bringUpObjective1()
    {

        SoundManagerScript.instance.RandomizeSfx(objectivePopUp);
        
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
       
        SoundManagerScript.instance.RandomizeSfx(objectivePopDown);
 
        this.GetComponent<GameManagerScript>().startObjectiveOne();
        startGameRunPanel();
       
    }

    //This is active when the game runs. It has pause and redo buttons
    void startGameRunPanel()
    {
        gameRunPanel.SetActive(true);
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(bringUpPausePanel);

        redoButton.onClick.RemoveAllListeners();
        redoButton.onClick.AddListener(destroyAllLines);

    }
    void destroyAllLines()
    {
        gameManagerInstance.deleteAllLines();
    }

    void bringUpPausePanel()
    {
        this.GetComponent<GameManagerScript>().Map.GetComponent<MapScript>().canDraw = false;
        pausePanel.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(resumeGame);

        currentObjectiveButton.onClick.RemoveAllListeners();
        currentObjectiveButton.onClick.AddListener(bringUpCurrentObjective);

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(exitGame);

    }
    void exitGame()
    {
        Application.Quit();

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
        objectivePanel2.SetActive(false);
        pausePanel.SetActive(false);
        this.GetComponent<GameManagerScript>().Map.GetComponent<MapScript>().canDraw = true;
    }
    public void bringUpObjective2()
    {
     
        SoundManagerScript.instance.RandomizeSfx(objectivePopUp);
        objectivePanel2.SetActive(true);
        objective2OkayButton.onClick.RemoveAllListeners();
        objective2OkayButton.onClick.AddListener(startObjective2);
       
    }

    void startObjective2()
    {
        SoundManagerScript.instance.RandomizeSfx(objectivePopDown);
        objectivePanel2.SetActive(false);
        if (!objectiveTwoStarted)
        {
            this.GetComponent<GameManagerScript>().startObjectiveTwo();
            objectiveTwoStarted = true;
        }
        else if (objectiveOneStarted)
        {
            objectiveOkayButton.onClick.AddListener(resumeGame);

        }
      
    }


    public void bringUpRetryPanel()
    {
        
        SoundManagerScript.instance.RandomizeSfx(retry);
        retryPanel.SetActive(true);

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(retryLevel);
        

    }
    void retryLevel()
    {
        retryPanel.SetActive(false);
        destroyAllLines();
    }
    public void bringUpVictory()
    {
        victoryPanel.SetActive(true);
    }
}
