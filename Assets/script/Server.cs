using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

    public bool infected = false;
    public float turnInfected = 0;
    public Player attacker;
    public Player defender;
    public GameManager gameManager;


	// Use this for initialization
	void Start ()
    {
        turnInfected = 3;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (turnInfected == 0)
        {
            gameManager.humanWins = true;
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.transform.GetComponent<Unit>().hasVirus == true)
        {
            
            collision.transform.GetComponent<Unit>().canInfect = true;
            if(collision.transform.GetComponent<Unit>().canInfect == true && Input.GetButtonDown("Player"+attacker.playerNumber+"X") && !infected)
            {
                
                infected = true;
            }
        }

        if (collision.transform.GetComponent<Unit>().hasAntiVirus == true)
        {
            collision.transform.GetComponent<Unit>().canDisInfect = true;
            if (collision.transform.GetComponent<Unit>().canDisInfect == true && Input.GetButtonDown("Player" + defender.playerNumber + "X") && infected)
            {
                gameManager.robotWins = true;
                infected = false;
            }
        }





    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player1" && collision.transform.GetComponent<Unit>().hasVirus == true)
        {
            
            collision.transform.GetComponent<Unit>().canInfect = false;   

        }
        
    }

    public void infectedServer()
    {
        if (infected && turnInfected > 0)
        {
            --turnInfected;
        }
    }
}
