using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommands : MonoBehaviour {


    public Player myPlayer;
  

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        #region Selezione Unità
        if (Input.GetAxis("RightStickHorizontalPlayer" + myPlayer.playerNumber) == 0 && /*myPlayer.myTurn == true &&*/ myPlayer.isAttacking == false)
        {
            myPlayer.isChangingUnit = false;
        }

        if (Input.GetAxis("RightStickHorizontalPlayer" + myPlayer.playerNumber) > 0 && myPlayer.isMenuOpen == false && myPlayer.isMovingPhase == false && myPlayer.selectedUnit < myPlayer.myUnits.Count - 1 && myPlayer.isChangingUnit == false && myPlayer.isAttacking == false && myPlayer.isUsingAbility == false && myPlayer.isUsingTactic == false)
        {
            myPlayer.isChangingUnit = true;
            ++myPlayer.selectedUnit;
            myPlayer.myUnits[myPlayer.selectedUnit - 1].GetComponent<Unit>().isSelected = false;
        }

        if (Input.GetAxis("RightStickHorizontalPlayer" + myPlayer.playerNumber) > 0 && myPlayer.isMenuOpen == false && myPlayer.isMovingPhase == false && myPlayer.selectedUnit == myPlayer.myUnits.Count - 1 && myPlayer.isChangingUnit == false &&myPlayer.isAttacking == false && myPlayer.isUsingAbility == false && myPlayer.isUsingTactic == false)
        {
            myPlayer.isChangingUnit = true;
            myPlayer.myUnits[myPlayer.selectedUnit].GetComponent<Unit>().isSelected = false;
            myPlayer.selectedUnit = 0;
        }

        if (Input.GetAxis("RightStickHorizontalPlayer" + myPlayer.playerNumber) < 0 && myPlayer.isMenuOpen == false && myPlayer.isMovingPhase == false && myPlayer.selectedUnit > 0 && myPlayer.isChangingUnit == false && myPlayer.isAttacking == false && myPlayer.isUsingAbility == false && myPlayer.isUsingTactic == false)
        {
            myPlayer.isChangingUnit = true;
            --myPlayer.selectedUnit;
            myPlayer.myUnits[myPlayer.selectedUnit + 1].GetComponent<Unit>().isSelected = false;
        }

        if (Input.GetAxis("RightStickHorizontalPlayer" + myPlayer.playerNumber) < 0 && myPlayer.isMenuOpen == false && myPlayer.isMovingPhase == false && myPlayer.selectedUnit == 0 && myPlayer.isChangingUnit == false && myPlayer.isAttacking == false && myPlayer.isUsingAbility == false && myPlayer.isUsingTactic == false)
        {
            myPlayer.isChangingUnit = true;
            myPlayer.myUnits[myPlayer.selectedUnit].GetComponent<Unit>().isSelected = false;
            myPlayer.selectedUnit = myPlayer.myUnits.Count - 1;
        }
        #endregion

        #region Interazione menu
        if (Input.GetButtonDown("Player" + myPlayer.playerNumber + "B") && myPlayer.myTurn == true)
        {
            reverseAction();
        }
        #endregion

        #region Cambio Turno

        if (Input.GetButtonDown("BackPlayer" + myPlayer.playerNumber) && myPlayer.myTurn == true)
        {
            
            reverseAction();
        }

        #endregion

        


    }

    void reverseAction()
    {
        myPlayer.isMovingPhase = false;
        myPlayer.isAttacking = false;
        myPlayer.isUsingAbility = false;
        myPlayer.isUsingTactic = false;

        if (myPlayer.rangeClones.Count > 0)
        {
            for (int j = 0; j < myPlayer.rangeClones.Count; j++)
            {
                Destroy(myPlayer.rangeClones[j]);

            }
            for (int l = 0; l < myPlayer.hits.Length; l++)
            {
                if (myPlayer.hits[l].transform.tag == "walkable")
                {
                    myPlayer.hits[l].transform.tag = "Floor";

                }
            }
            myPlayer.rangeClones.Clear();
        }
    }
}
