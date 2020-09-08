using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    public Player[] player;
    public Camera cameraPlayer1;
    public Camera cameraPlayer2;
    bool player1Human = true;
    public Canvas player1Canvas;
    public Canvas player2Canvas;
    public GameManager gameManager;
    private bool nextTurn = false;
    public Server[] servers;
    private void Awake()
    {
       
        if (!instance)
        {
            instance = this as GameplayManager;
        }
        else
        {
            DestroyObject(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    IEnumerator Start()
    {
        while (true /* or when game finishes */)
        {
            for (int i = 0; i < player.Length; i++)
            {
                yield return StartCoroutine(PlayRound(i));
                yield return StartCoroutine(DisableAllPlayers());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator PlayRound(int playerNumber)
    {
        player[playerNumber].GetComponent<PlayerCommands>().enabled = true;
        nextTurn = false;
        player[playerNumber].myTurn = true;
        resetAP(playerNumber);
        while (!nextTurn)
        {
            yield return null;
        }

        // Play musichetta

    }

    private IEnumerator DisableAllPlayers()
    {
        player[0].myTurn = false;
        player[1].myTurn = false;
        player[0].GetComponent<PlayerCommands>().enabled = false;
        player[1].GetComponent<PlayerCommands>().enabled = false;
        // disable all player components
        yield return null;
    }

    /// <summary>
    /// Called by Player when finishing the turn
    /// </summary>
    public void SwitchTurn()
    {
        nextTurn = true;
        reverseAction();

        Debug.Log("Sono stato chiamato");
    }

    public void resetAP(int actualPlayer)
    {
        for (int i = 0; i < player[actualPlayer].myUnits.Count; i++)
        {
            player[actualPlayer].myUnits[i].GetComponent<Unit>().actionPoints = player[actualPlayer].myUnits[i].GetComponent<Unit>().maxActionPoints;
        }
    }

    public void switchTeam()
    {
        

        if (player1Human)
        {
            player1Human = false;
            cameraPlayer1.targetDisplay = 1;
            cameraPlayer2.targetDisplay = 0;
            player[0].playerNumber = 2;
            player[1].playerNumber = 1;
            gameManager.round += 1;
            gameManager.turn = 1;
            resetAP(1);
            resetAP(0);
            player[0].resetUnits();
            player[1].resetUnits();
            player1Canvas.targetDisplay = 1;
            player2Canvas.targetDisplay = 0;
            gameManager.txtFeedBack1.text = "";
            gameManager.txtFeedBack2.text = "";
            StartCoroutine(DisableAllPlayers());
            StartCoroutine(PlayRound(0));
            servers[0].infected = false;
            servers[1].infected = false;
            servers[0].turnInfected = 3;
            servers[1].turnInfected = 3;
            reverseAction();
        }
        else if(!player1Human)
        {
            player1Human = true;
            cameraPlayer1.targetDisplay = 0;
            cameraPlayer2.targetDisplay = 1;
            player[0].playerNumber = 1;
            player[1].playerNumber = 2;
            gameManager.round += 1;
            gameManager.turn = 1;
            resetAP(1);
            resetAP(0);
            player[0].resetUnits();
            player[1].resetUnits();
            player1Canvas.targetDisplay = 0;
            player2Canvas.targetDisplay = 1;
            gameManager.txtFeedBack1.text = "";
            gameManager.txtFeedBack2.text = "";
            StartCoroutine(DisableAllPlayers());
            StartCoroutine(PlayRound(0));
            servers[0].infected = false;
            servers[1].infected = false;
            servers[0].turnInfected = 3;
            servers[1].turnInfected = 3;
            reverseAction();

        }
        gameManager.robotWins = false;
        gameManager.humanWins = false;
    }

    void reverseAction()
    {
        player[0].isMovingPhase = false;
        player[0].isAttacking = false;
        player[0].isUsingAbility = false;
        player[0].isUsingTactic = false;
        player[1].isMovingPhase = false;
        player[1].isAttacking = false;
        player[1].isUsingAbility = false;
        player[1].isUsingTactic = false;

        if (player[0].rangeClones.Count > 0)
        {
            for (int j = 0; j < player[0].rangeClones.Count; j++)
            {
                Destroy(player[0].rangeClones[j]);

            }
            for (int l = 0; l < player[0].hits.Length; l++)
            {
                if (player[0].hits[l].transform.tag == "walkable")
                {
                    player[0].hits[l].transform.tag = "Floor";

                }
            }
            player[0].rangeClones.Clear();
        }

        if (player[1].rangeClones.Count > 0)
        {
            for (int j = 0; j < player[1].rangeClones.Count; j++)
            {
                Destroy(player[1].rangeClones[j]);

            }
            for (int l = 0; l < player[1].hits.Length; l++)
            {
                if (player[1].hits[l].transform.tag == "walkable")
                {
                    player[1].hits[l].transform.tag = "Floor";

                }
            }
            player[1].rangeClones.Clear();
        }
    }
}
