using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int turn = 1;
    public int round = 1;
    public int player1Score;
    public int player2Score;
    public float time1;
	public float time2;
    public string playerTurn;
    string yourturn = "E' il tuo turno";
    string notYourTurn =  "Attendi";
    float apUnit1;
    float apUnit2;
    public bool turnPass1 = false;
    public bool turnPass2 = false;
    public Player[] player;
    GameplayManager manager;
   // public Unit unit;
    public Server[] servers;
    public bool robotWins = false;
    public bool humanWins = false;
	int humansWins = 0;
	int robotsWins = 0;



    public Text txtTime1;
    public Text txtTurn1;
    public Text txtScore1;
    public Text txtAP1;
    public Text txtFeedBack1;
	public Text txtPlayersScore1;
	public Text txtCanInfect;

    public Text txtTime2;
    public Text txtTurn2;
    public Text txtScore2;
    public Text txtAP2;
    public Text txtFeedBack2;
	public Text txtPlayersScore2;
	public Text txtCanDisInfect;

    public Canvas canvasAtk;
    public Canvas canvasDef;
    GameObject mioTurno1;
    GameObject mioTurno2;
    GameObject conferma1;
    GameObject indietro1;
    GameObject conferma2;
    GameObject indietro2;

	public Image captainHumanImage;
	public Image tank1Image;
	public Image tank2Image;
	public Image sniper1Image;
	public Image sniper2Image;

	public Image captainRobotImage;
	public Image jaggernaut1Image;
	public Image jaggernaut2Image;
	public Image executor1Image;
	public Image executor2Image;

    void Start()
    {

        //canvasAtk = GetComponent<Canvas>();
        //canvasDef = GetComponent<Canvas>();
        mioTurno1 = GameObject.FindGameObjectWithTag("Turno");
        mioTurno2 = GameObject.FindGameObjectWithTag("Turno1");
        canvasAtk = canvasAtk.GetComponent<Canvas>();
        canvasDef = canvasDef.GetComponent<Canvas>();
        txtFeedBack1.text = yourturn;
        conferma1 = GameObject.FindGameObjectWithTag("Conferma");
        conferma2 = GameObject.FindGameObjectWithTag("Conferma1");
        indietro1 = GameObject.FindGameObjectWithTag("Indietro");
        indietro2 = GameObject.FindGameObjectWithTag("Indietro1");
        conferma1.SetActive(false);
        conferma2.SetActive(false);
        indietro1.SetActive(false);
        indietro2.SetActive(false);
    }


    void Update()
    {




        if (Input.GetButton("LeftBumperPlayer" + player[0].playerNumber)) 
        {
            canvasAtk.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            canvasAtk.GetComponent<Canvas>().enabled = true;
        }

        if (Input.GetButton("LeftBumperPlayer" + player[1].playerNumber))
        {
            canvasDef.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            canvasDef.GetComponent<Canvas>().enabled = true;
        }

        if (player[0].myUnits.Count != 0 && player[1].myUnits.Count != 0)
        {
           
            if (player[0].myUnits[player[0].selectedUnit] != null && player[1].myUnits[player[1].selectedUnit] != null)
            {
                apUnit1 = player[0].myUnits[player[0].selectedUnit].GetComponent<Unit>().actionPoints;
                apUnit2 = player[1].myUnits[player[1].selectedUnit].GetComponent<Unit>().actionPoints;

            }
        }

      	#region icone
		//feedback selezione unità da icone
		switch (player[0].myUnits[player[0].selectedUnit].name)
		{
		case "CaptainHuman":
			captainHumanImage.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			tank1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			tank2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			sniper1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			sniper2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		case "TankHuman":
			tank1Image.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			captainHumanImage.rectTransform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
			tank2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			sniper1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			sniper2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		case "TankHuman (1)":
			tank2Image.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			captainHumanImage.rectTransform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
			tank1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			sniper1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			sniper2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		case "SniperHuman":
			sniper1Image.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			tank2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			captainHumanImage.rectTransform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
			tank1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			sniper2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		case "SniperHuman (1)":
			sniper2Image.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			tank2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			captainHumanImage.rectTransform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
			tank1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			sniper1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		}


		switch (player[1].myUnits[player[1].selectedUnit].name)
		{
		case "CaptainRobot":
			captainRobotImage.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			jaggernaut1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			jaggernaut2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			executor1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			executor2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		case "TankRobot":
			jaggernaut1Image.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			captainRobotImage.rectTransform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
			jaggernaut2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			executor1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			executor2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		case "TankRobot (1)":
			jaggernaut2Image.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			captainRobotImage.rectTransform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
			jaggernaut1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			executor1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			executor2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		case "SniperRobot":
			executor1Image.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			jaggernaut2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			captainRobotImage.rectTransform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
			jaggernaut1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			executor2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		case "SniperRobot (1)":
			executor2Image.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			jaggernaut2Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			captainRobotImage.rectTransform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
			jaggernaut1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			executor1Image.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
			break;

		}


        #endregion

        txtTurn1.text = turn + "/20";
        txtTurn2.text = turn + "/20";
        player1Score = player[0].player1Score;
        player2Score = player[1].player2Score;
        //gestione del tempo (testo)
        txtTime1.text = Mathf.RoundToInt(time1).ToString();
        txtTime2.text = Mathf.RoundToInt(time2).ToString();
        //gestione dell'AP (testo)
        txtAP1.text = Mathf.RoundToInt(apUnit1).ToString();
        txtAP2.text = Mathf.RoundToInt(apUnit2).ToString();
		//gestione vittorie umani/robot (testo)
		txtPlayersScore1.text = humansWins + " - " + robotsWins;
		txtPlayersScore2.text = humansWins + " - " + robotsWins;

		//feedback pressione tasto per installare virus
		if (player [0].myUnits [player [0].selectedUnit].GetComponent<Unit> ().canInfect == true) {
			txtCanInfect.text = "Premi X per installare il virus";
		} else {
			txtCanInfect.text = "";
		}

		//feedback turni rimanenti per installare l'antivirus alla difesa
		if (servers[0].infected == true) 
		{
			txtCanDisInfect.text = "Hai " + servers [0].turnInfected + " turni per installare l'antivirus";
			txtTurn1.text = servers [0].turnInfected + "/3";
			txtTurn2.text = servers [0].turnInfected + "/3";
		}
		if (servers[1].infected == true) 
		{
			txtCanDisInfect.text = "Hai " + servers [1].turnInfected + " turni per installare l'antivirus";
			txtTurn1.text = servers [1].turnInfected + "/3";
			txtTurn2.text = servers [0].turnInfected + "/3";
		}

        //inizio countdown in base al turno del player
        if (player[0].myTurn == true)
        {

            time1 -= Time.deltaTime;
            time2 = 90f;

        }
        else if (player[1].myTurn == true)
        {

            time2 -= Time.deltaTime;
            time1 = 90f;

        }

        if (time1 < 0)
        {
            GameplayManager.instance.SwitchTurn();
            turnPass1 = true;
            
        }
        if (time2 < 0)
        {
            GameplayManager.instance.SwitchTurn();
            turnPass2 = true;
            
        }

        

        //passaggio del turno (testo + image)
        if (Input.GetButtonDown("BackPlayer2" /*+ player[0].playerNumber*/) && player[0].myTurn == true && !robotWins && !humanWins)
        {
            GameplayManager.instance.SwitchTurn();
            turnPass1 = true;
            
        }

        if (Input.GetButtonDown("BackPlayer2"/* + player[1].playerNumber*/) && player[1].myTurn == true && !robotWins && !humanWins)
        {
            GameplayManager.instance.SwitchTurn();
            turnPass2 = true;
            
        }

        if (player[1].myTurn == true && !robotWins && !humanWins)
        {
            
            txtFeedBack1.text =  notYourTurn;
           
            txtFeedBack2.text = yourturn;
        }
        if (player[0].myTurn == true && !robotWins && !humanWins)
        {
            
            txtFeedBack1.text = yourturn;
            
            txtFeedBack2.text = notYourTurn;
        }


        if (turnPass1 && turnPass2)
        {
            servers[0].infectedServer();
            servers[1].infectedServer();
            turnPass1 = false;
            turnPass2 = false;
            turn += 1;
            txtTurn1.text = turn + "/20";
            txtTurn2.text = turn + "/20";
           
        }

        if (player[0].isMenuOpen)
        {
            conferma1.SetActive(true);
            indietro1.SetActive(true);
        }
        else
        {
            conferma1.SetActive(false);
            indietro1.SetActive(false);
        }

        if (player[1].isMenuOpen)
        {
            conferma2.SetActive(true);
            indietro2.SetActive(true);
        }
        else
        {
            conferma2.SetActive(false);
            indietro2.SetActive(false);
        }


        if (turn == 21)
        {
            robotWins = true;
        }

        if(robotWins)
        {
            txtFeedBack2.text = "Hai Vinto!";
            txtFeedBack1.text = "Hai Perso!";
            mioTurno1.SetActive(true);
            mioTurno2.SetActive(true);
			
        }
        
        

        
        if (humanWins)
        {
            txtFeedBack2.text = "Hai Perso!";
            txtFeedBack1.text = "Hai Vinto!";
            mioTurno1.SetActive(true);
            mioTurno2.SetActive(true);
			
        }

        if (robotWins)
        {
            if(Input.GetButtonDown("StartButtonPlayer1") || Input.GetButtonDown("StartButtonPlayer2"))
            {
                GameplayManager.instance.switchTeam();
                robotsWins += 1;
            }
        }
        if (humanWins)
        {
            if (Input.GetButtonDown("StartButtonPlayer1") || Input.GetButtonDown("StartButtonPlayer2"))
            {
                GameplayManager.instance.switchTeam();
                humansWins += 1;
            }
        }


        if (player[0].myUnits.Count == 0)
        {
            robotWins = true;
        }

        if (player[1].myUnits.Count == 0)
        {
            humanWins = true;
        }

    }

}



