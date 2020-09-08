using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour {


    public Unit[] units;
    public FieldOfView[] unitsFov;
    public Player player;
    public Player opponent;
    SpriteRenderer sR;
    Color myColor;
    public bool isHidden;
    bool isHiddenByCollision;
    bool isHiddenByPlayer;
	
	void Start ()
    {
        player = player.GetComponent<Player>();
        sR = GetComponent<SpriteRenderer>();
        myColor = sR.color;
	}
	
	
	void Update ()
    {
        if (isHidden)
        {
            this.sR.color = new Color(1, 1, 1, 0);
        }
        else
        {
            this.sR.color = myColor;
        }

        if(isHiddenByCollision || isHiddenByPlayer)
        {
            isHidden = true;
        }
        if (!isHiddenByCollision && !isHiddenByPlayer)
        {
            isHidden = false;
        }

        if (player.isAttacking || player.isMovingPhase)
        {
            isHiddenByPlayer = true;
        }
        else
        {
            isHiddenByPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" + player.playerNumber)
        {
            isHiddenByCollision = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" + player.playerNumber)
        {
            isHiddenByCollision = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" + opponent.playerNumber)
        {
            if (collision.GetComponent<Unit>().imSeen == true)
            {
                Debug.Log("del signore");
                isHiddenByCollision = true;
            }
            else if (collision.GetComponent<Unit>().imSeen == false)
            {
                isHiddenByCollision = false;
            }
        }
    }
}
