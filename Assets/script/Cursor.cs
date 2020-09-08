using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Cursor : MonoBehaviour
{
    public bool isMoving = false;      // bool che serve per non rendere il movimento continuo
    public bool isSelecting = false;   // bool che controlla la selezione avvenuta dell'unità
    
    public AILerp[] unitPath;          // array per interagire con i movimenti delle unità
    public bool unitIsMoving = false;  // serve per bloccare il cursore mentre l'unità 
    public Player myPlayer;
    public string walkableTile;
    public LayerMask ignoreCollisions = 1 << 1;

    void Start()
    {
        

    }

    
    void Update()
    {

        
        #region CursorMovement
        if (Input.GetAxis("LeftStickHorizontalPlayer"+myPlayer.GetComponent<Player>().playerNumber) == 0 && Input.GetAxis("LeftStickVerticalPlayer" + myPlayer.GetComponent<Player>().playerNumber) == 0 && !unitIsMoving)
        {
            isMoving = false;
        }


        if (Input.GetAxis("LeftStickHorizontalPlayer" + myPlayer.GetComponent<Player>().playerNumber) >= 0.3f && Input.GetAxis("LeftStickVerticalPlayer" + myPlayer.GetComponent<Player>().playerNumber) <= -0.3f && !isMoving && myPlayer.GetComponent<Player>().isMovingPhase == true)
        {
            isMoving = true;
            moveCursor(Vector2.left, transform.position + new Vector3(1, 0, 0));
        }


        if (Input.GetAxis("LeftStickHorizontalPlayer"+myPlayer.GetComponent<Player>().playerNumber) <= -0.3f && (Input.GetAxis("LeftStickVerticalPlayer" + myPlayer.GetComponent<Player>().playerNumber) >= 0.3f && !isMoving && myPlayer.GetComponent<Player>().isMovingPhase == true))
        {
            isMoving = true;
            moveCursor(Vector2.right, transform.position + new Vector3(-1, 0, 0));
        }


        if (Input.GetAxis("LeftStickVerticalPlayer" + myPlayer.GetComponent<Player>().playerNumber) <= -0.3f && Input.GetAxis("LeftStickHorizontalPlayer" + myPlayer.GetComponent<Player>().playerNumber) <= -0.3f && !isMoving && myPlayer.GetComponent<Player>().isMovingPhase == true)
        {
            isMoving = true;
            moveCursor(Vector2.down, transform.position + new Vector3(0, 1, 0));
        }


        if (Input.GetAxis("LeftStickVerticalPlayer" + myPlayer.GetComponent<Player>().playerNumber) >= 0.3f && Input.GetAxis("LeftStickHorizontalPlayer" + myPlayer.GetComponent<Player>().playerNumber) >= 0.3f && !isMoving && myPlayer.GetComponent<Player>().isMovingPhase == true)
        {
            isMoving = true;
            moveCursor(Vector2.up, transform.position + new Vector3(0, -1, 0));
        }

        #endregion

        #region Movimento Unità

        if(myPlayer.GetComponent<Player>().isMovingPhase == true || myPlayer.GetComponent<Player>().isAttacking == true)
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        if(Input.GetButton("LeftBumperPlayer" + myPlayer.GetComponent<Player>().playerNumber))
        {
            isMoving = true;
        }


        

        #endregion  


    }



    private void moveCursor(Vector2 tileDirection, Vector2 moveDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(moveDirection, tileDirection,10f,ignoreCollisions);
        Debug.Log(hit.transform.tag);
        if (hit.collider.transform != this.transform && hit.collider.tag == "walkable")
        {
            walkableTile = hit.collider.tag;
            this.transform.position = hit.collider.transform.position;
        }
        if (hit.collider.transform != this.transform && hit.collider.tag == "Player1" || hit.collider.transform != this.transform && hit.collider.tag == "Player2")
        {
            walkableTile = hit.collider.tag;
            this.transform.position = hit.collider.transform.position;
        }
    }


    

}



