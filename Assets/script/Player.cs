using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 

    public LayerMask ingoreLayers = 1 << 2;
    public List<GameObject> myUnits = new List<GameObject>();
    public List<GameObject> defaultUnits = new List<GameObject>();
    public List<AILerp> unitPath = new List<AILerp>();
    public Unit mySelectedUnit;
    //gestione turno e comandi devono essere uguali per permettere ad uno dei giocatori di comandare 
    public int playerNumber = 0;
    public bool myTurn = false;
    //stato del player
    public bool isMenuOpen = false;
    public bool isMovingPhase = false;
    public bool isAttacking = false;
    public bool isUsingAbility = false;
    public bool isUsingTactic = false;
    //variabili per movimento del cursore e selezione delle unità
    public bool isChangingUnit = false;
    public int selectedUnit = 0;
    public Cursor myCursor;
    // oggetto che visualizza l'area di spostamento
    public GameObject range;    
    public GameObject cloneRange;
    public GameObject attackRangeVisual;
    float attackRangeScale;
    public int player1Score;
    public int player2Score;

    
    public RaycastHit2D[] hits; //necessario per castare il range di movimento
    public List<GameObject> rangeClones = new List<GameObject>(); //lista contenente
    public int attackSelection = 0;
 
	public GameObject menu_1;
	public GameObject menu_2;
	public GameObject menu_3;
	public Vector3 startPosition;
    public Vector3 attackStartPosition;
    //Vector3 graveyard;
 




    void Start()
    {
        defaultUnits.AddRange(myUnits);
        startPosition = new Vector3 (menu_1.transform.position.x, menu_1.transform.position.y, menu_1.transform.position.z);
        attackStartPosition = attackRangeVisual.transform.position;
        Debug.Log(Input.GetJoystickNames()[0]);
        Debug.Log(Input.GetJoystickNames()[1]);

    }


    void Update()
    {
        
        if (myUnits.Count == 0)
        {
            return;
        }

        if (isMovingPhase)
        {
            mySelectedUnit.calucalteAp();
        }
        
        if( myUnits[selectedUnit] != null)
        {
            mySelectedUnit = myUnits[selectedUnit].GetComponent<Unit>();
            mySelectedUnit.isSelected = true;
            attackRangeScale = (mySelectedUnit.primaryRange * 2f) / 10f;
        }

        for (int i = 0; i < myUnits.Count; i++)
        {
            if (myUnits[i].GetComponent<Unit>().curHP <= 0)
            {
                RemoveFromList(i);
                if(myUnits[i].GetComponent<Unit>().isSelected)
                {
                    myUnits[i].GetComponent<Unit>().isSelected = false;
                    selectedUnit = 0;
                }
            }
        }
        
       
        #region Interazione Menù

        if (isMenuOpen == false)
        {
            menu_1.transform.position = startPosition;
            menu_2.transform.position = startPosition;
            menu_3.transform.position = startPosition;
        }


        if (Input.GetButton("LeftBumperPlayer" + playerNumber) && myTurn == true && !isMovingPhase && !isAttacking)
        {

            isMenuOpen = true;
            menu_1.transform.position = myUnits[selectedUnit].transform.position;
            myCursor.GetComponent<Cursor>().transform.position = myUnits[selectedUnit].transform.position;

            if (isMenuOpen && Input.GetAxis("LeftStickHorizontalPlayer" + playerNumber) < -0f && Input.GetAxis("LeftStickVerticalPlayer" + playerNumber) < 0f && myTurn == true && myUnits[selectedUnit].GetComponent<Unit>().apLost <= myUnits[selectedUnit].GetComponent<Unit>().actionPoints)
            {
                menu_1.transform.position = startPosition;
                menu_2.transform.position = myUnits[selectedUnit].transform.position;

                if (Input.GetButtonDown("Player" + playerNumber + "A"))
                {
                    myCursor.GetComponent<Cursor>().transform.position = myUnits[selectedUnit].GetComponent<Unit>().transform.position;
                    isMovingPhase = true;
                    isMenuOpen = false;
                    walkingRange(new Vector2(myUnits[selectedUnit].GetComponent<Unit>().movementRange, myUnits[selectedUnit].GetComponent<Unit>().movementRange));
                    

                }
            }
            

            if (isMenuOpen && Input.GetAxis("LeftStickHorizontalPlayer" + playerNumber) > 0f && Input.GetAxis("LeftStickVerticalPlayer" + playerNumber) < 0f && myTurn == true && myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets.Count > 0 && myUnits[selectedUnit].GetComponent<Unit>().actionPoints >= myUnits[selectedUnit].GetComponent<Unit>().primaryCost)
            {
                menu_1.transform.position = startPosition;
                menu_3.transform.position = myUnits[selectedUnit].transform.position;

                if (Input.GetButtonUp("Player" + playerNumber + "A"))
                {
                    isMenuOpen = false;
                    isAttacking = true;
                }
            }
        }
        else
        {
            isMenuOpen = false;
        }

        #endregion

        #region Movimento Unità

        if (isMovingPhase == true && Input.GetButtonDown("Player" + playerNumber + "A") && Input.GetButton("LeftBumperPlayer" + playerNumber) == false && myCursor.GetComponent<Cursor>().walkableTile == "walkable" && myUnits[selectedUnit].GetComponent<Unit>().apLost <= myUnits[selectedUnit].GetComponent<Unit>().actionPoints)
        {
            myCursor.GetComponent<Cursor>().unitIsMoving = true;

            for (int i = 0; i < unitPath.Count; i++)
            {
                if (unitPath[i].GetComponent<Unit>().isSelected && unitPath[i].canMove == false)
                {
                    unitPath[i].GetComponent<Unit>().actionPoints = unitPath[i].GetComponent<Unit>().actionPoints - unitPath[i].GetComponent<Unit>().apLost;
                    unitPath[i].canMove = true;
                }
                for (int j = 0; j < rangeClones.Count; j++)
                {
                    Destroy(rangeClones[j]);
                }
                for (int l = 0; l < hits.Length; l++)
                {
                    if (hits[l].transform.tag == "walkable")
                    {
                        hits[l].transform.tag = "Floor";

                    }
                }
            }
        }


        #endregion
        //attackRangeScale = (mySelectedUnit.primaryRange *2f) /10f;
        Debug.Log(attackRangeScale);
        if (isAttacking)
        {
            attackRangeVisual.transform.localScale = new Vector3(attackRangeScale,attackRangeScale, 0);
            attackRange();
        }
        else
        {
            attackRangeVisual.transform.position = attackStartPosition;
        }
    }

    //metodo per visualizzare l'area di movimento
    void walkingRange(Vector2 size)
    {

        hits = Physics2D.BoxCastAll(myUnits[selectedUnit].GetComponent<Unit>().transform.position, size, 45f, new Vector2(0, 0), ingoreLayers);

        if (isMovingPhase)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.tag == "Floor")
                {
                    hits[i].transform.tag = "walkable";
                    cloneRange = Instantiate(range, hits[i].transform);
                    rangeClones.Add(cloneRange);
                }
            }
        }
    }

    void attackRange()
    {
        //attackRangeVisual.transform.localScale = new Vector3 (((myUnits[selectedUnit].GetComponent<Unit>().primaryRange * 2) / 10),((myUnits[selectedUnit].GetComponent<Unit>().primaryRange * 2) / 10),0);
        attackRangeVisual.transform.position = myUnits[selectedUnit].transform.position;

        //gestione della selezione dell'attacco usando la lista nello script field of pew
        if (Input.GetAxis("LeftStickHorizontalPlayer" + playerNumber) == 0 && myTurn == true)
        {
            isChangingUnit = false;
        }

        if (Input.GetAxis("LeftStickHorizontalPlayer" + playerNumber) > 0 && attackSelection < myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets.Count -1 && isChangingUnit == false)
        {
            
            attackSelection += 1;
            isChangingUnit = true;

        }

        if (Input.GetAxis("LeftStickHorizontalPlayer" + playerNumber) < 0 && attackSelection > 0 && isChangingUnit == false)
        {
           
            attackSelection -= 1;
            isChangingUnit = true;

        }
        //muove il cursore sotto l'unità selezionata
        myCursor.GetComponent<Cursor>().transform.position = myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets[attackSelection].transform.position;
        
        if (Input.GetButtonDown("Player" + playerNumber + "A"))
        {
            Debug.Log("pew");
            myUnits[selectedUnit].GetComponent<Unit>().actionPoints = myUnits[selectedUnit].GetComponent<Unit>().actionPoints - myUnits[selectedUnit].GetComponent<Unit>().primaryCost;
            myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets[attackSelection].GetComponent<Unit>().curHP -= myUnits[selectedUnit].GetComponent<Unit>().primaryDamage;
            checkShootDirection();
            attackSelection = 0;
            isAttacking = false;
        }

    }

    void RemoveFromList(int deathone)
    {
        myUnits[deathone].GetComponent<Unit>().isDeath = true;
        myUnits.RemoveAt(deathone);
        for (int i = 0; i < unitPath.Count; i++)
        {
            if (unitPath[i] == null)
            {
                unitPath.RemoveAt(i);
            }
        }

    }
    public void resetUnits()
    {
        myUnits.Clear();
        myUnits.AddRange(defaultUnits);

        for (int i = 0; i < myUnits.Count; i++)
        {
            myUnits[i].GetComponent<Unit>().resetHP();
            myUnits[i].transform.position = myUnits[i].GetComponent<Unit>().myTransform;

        }
    }


    public void checkShootDirection()
    {
        
        if (Mathf.Abs((myUnits[selectedUnit].GetComponent<Unit>().transform.position.x - myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets[attackSelection].GetComponent<Unit>().transform.position.x)) > Mathf.Abs((myUnits[selectedUnit].GetComponent<Unit>().transform.position.y - myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets[attackSelection].GetComponent<Unit>().transform.position.y)))
        {
            if(myUnits[selectedUnit].GetComponent<Unit>().transform.position.x > myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets[attackSelection].GetComponent<Unit>().transform.position.x)
            {
                Debug.Log("west");
                myUnits[selectedUnit].GetComponent<Unit>().isAttackingWest = true;
            }
            if (myUnits[selectedUnit].GetComponent<Unit>().transform.position.x < myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets[attackSelection].GetComponent<Unit>().transform.position.x)
            {
                Debug.Log("east");
                myUnits[selectedUnit].GetComponent<Unit>().isAttackingEast = true;
            }
        }
        else
        {
            if (myUnits[selectedUnit].GetComponent<Unit>().transform.position.y < myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets[attackSelection].GetComponent<Unit>().transform.position.y)
            {
                Debug.Log("north");
                myUnits[selectedUnit].GetComponent<Unit>().isAttackingNorth = true;
            }
            if (myUnits[selectedUnit].GetComponent<Unit>().transform.position.y > myUnits[selectedUnit].GetComponent<FieldOfPew>().visibleTargets[attackSelection].GetComponent<Unit>().transform.position.y)
            {
                Debug.Log("south");
                myUnits[selectedUnit].GetComponent<Unit>().isAttackingSouth = true;
            }
        }


    }

    


}

