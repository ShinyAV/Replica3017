using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {

    public Vector2 startPosition;
    public Vector2 newPosition;
    
    Unit myParent;

    Animator anim;

    public bool flipX = false;

    // Use this for initialization
    void Start ()
    {
        myParent = GetComponentInParent<Unit>();
        
        anim = GetComponent<Animator>();
        startPosition = (myParent.transform.position);
       
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(myParent.isDeath)
        {
            StartCoroutine(deathAnimation());
            
        }

        if(myParent.isAttackingWest)
        {
            anim.SetBool("isShootingFront", true);
            Debug.Log("porcamadonnatroia");
            myParent.isAttackingWest = false;
            transform.localScale = new Vector3(-1, 1, 0);
            return;
        }
        else
        {
            anim.SetBool("isShootingFront", false);
        }
        if (myParent.isAttackingNorth)
        {
            anim.SetBool("isShootingBack", true);
            Debug.Log("GesùSporco");
            myParent.isAttackingNorth = false;
            transform.localScale = new Vector3(1, 1, 0);
            return;
        }
        else
        {
            anim.SetBool("isShootingBack", false);
        }
        if (myParent.isAttackingSouth)
        {
            anim.SetBool("isShootingFront", true);
            Debug.Log("porcamadonna");
            myParent.isAttackingSouth = false;
            transform.localScale = new Vector3(1, 1, 0);
            return;
            
        }
        else
        {
            anim.SetBool("isShootingFront", false);
        }
        if (myParent.isAttackingEast)
        {
            anim.SetBool("isShootingBack", true);
            Debug.Log("porcamadonnastronza");
            myParent.isAttackingEast = false;
            transform.localScale = new Vector3(-1, 1, 0);
           
        }
        else
        {
            anim.SetBool("isShootingFront", false);
        }


        if ((Mathf.Abs(startPosition.y - myParent.transform.position.y)) > (Mathf.Abs(startPosition.x - myParent.transform.position.x)))
        {
            #region Y
            if (startPosition.y > myParent.transform.position.y)
            {
                anim.SetBool(("isMovingFront"), true);
                
                transform.localScale = new Vector3(1, 1, 0);
                startPosition = (myParent.transform.position);
                
            }
            else
            {
                anim.SetBool(("isMovingFront"), false);
            }

            if (startPosition.y < myParent.transform.position.y)
            {
                anim.SetBool(("isMovingBack"), true);
                
                transform.localScale = new Vector3(1, 1, 0);
                startPosition = (myParent.transform.position);
                return;
            }
            else
            {
                anim.SetBool(("isMovingBack"), false);
            }
            #endregion
        }
        else
        {
        
            #region X
            if (startPosition.x > myParent.transform.position.x)
            {
                anim.SetBool(("isMovingFront"), true);
                
                transform.localScale = new Vector3 (-1,1,0);
                startPosition = (myParent.transform.position);
            }
            else
            {
                anim.SetBool(("isMovingFront"), false);
            }

            if (startPosition.x < myParent.transform.position.x)
            {
                anim.SetBool(("isMovingBack"), true);
                
                transform.localScale = new Vector3(-1, 1, 0);
                startPosition = (myParent.transform.position);
            }
            else
            {
                anim.SetBool(("isMovingBack"), false);
                
            }

        }
#endregion
    }
    IEnumerator deathAnimation()
    {
        anim.SetBool("isDeathFront", true);
       
        yield return new WaitForSeconds (2.5f);
        myParent.transform.position = myParent.graveyard;
        anim.SetBool("isDeathFront", false);
        Debug.Log("oasifbnasoifbasopfasibfas");
        yield return new WaitForSeconds(5f);
        myParent.isDeath = false;

    }
}
