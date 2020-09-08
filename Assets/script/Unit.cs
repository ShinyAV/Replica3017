using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
[System.Serializable]
public class Unit : MonoBehaviour {

    public bool isSelected = false;
    public AILerp aiLerp;
    public Seeker seeker;
    public bool imSeen;
    FieldOfView fov;
    SpriteRenderer sR;
    public float movementRange;
    float xDiff;
    float yDiff;
    public float apLost;
    public bool isAttackingNorth;
    public bool isAttackingEast;
    public bool isAttackingSouth;
    public bool isAttackingWest;
    public bool isDeath;
    public bool hasVirus = false;
    public bool canInfect = false;
    public bool hasAntiVirus = false;
    public bool canDisInfect = false;
    public GameObject virus;
    public GameObject antiVirus;
    public FieldOfView[] enemies;
    #region Variabili Classe

    public string nameClass;

    public enum UnitClass //classe del personaggio
    {
        CAPTAINHUMAN,
        TANKHUMAN,
        SNIPERHUMAN,
        SCOUTHUMAN,
        FIGHTERHUMAN,
        CAPTAINROBOT,
        TANKROBOT,
        SNIPERROBOT,
        SCOUTROBOT,
        FIGHTERROBOT
    }

    public UnitClass unitClass;

    public float baseHP;
    public float curHP;
    public float movementCostMultiplier;
    public float actionPoints;
    public float maxActionPoints;
    public float movement;
    public float primaryDamage;
    public int primaryRange;
    public int primaryCost;
    public int secondaryDamage;
    public int secondaryRange;
    public int secondaryDuration;
    public int abilityRange;
    public int abilityDuration;

    #endregion


    public int defaultLayer = 0;
    public Vector3 myTransform;
    public Vector3 graveyard;

    void Start ()
    {
        myTransform = this.transform.position;

        graveyard = new Vector3(100, 100, 0);
        maxActionPoints = actionPoints;
        seeker = this.gameObject.GetComponent<Seeker>();
        aiLerp = this.gameObject.GetComponent<AILerp>();
        fov = this.gameObject.GetComponent<FieldOfView>();
        sR = this.gameObject.GetComponent<SpriteRenderer>();
        
        if(this.nameClass == "CaptainHuman")
        {
            hasVirus = true;
        }
        if (this.nameClass == "CaptainRobot")
        {
            hasAntiVirus = true;
        }


    }
	
	
	void Update ()
    {



        movement = actionPoints/movementCostMultiplier;
        movementRange = Mathf.Floor(((2 * movement) / (Mathf.Sqrt(2))));
        if (this.curHP <= 0 && hasVirus)
        { 
            hasVirus = false;
            virus.transform.position = this.transform.position;
        }

        if (this.curHP <= 0 && hasAntiVirus)
        {
        
            hasAntiVirus = false;
            antiVirus.transform.position = this.transform.position;
        }


        SetLayerRecursively(this.gameObject, this.gameObject.layer);

        if(isSelected)
        {
            sR.color = Color.yellow;
        }
        else
        {
            sR.color = new Color(1, 1, 1, 0);
        }

        //imSeen = false;
        for (int i = 0; i < fov.visibleTargets.Count; i++)
        {
            fov.visibleTargets[i].GetComponent<Unit>().imSeen = true;
            
        }

        if (imSeen)
        {
            gameObject.layer = 10;
                        
        }
        else
        {
            gameObject.layer = defaultLayer;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].visibleTargets.Count == 0)
            {
                imSeen = false;
                /*for (int j = 0; j < enemies[i].visibleTargets.Count; j++)
                {
                    if(enemies[i].visibleTargets[j].gameObject.transform == this.gameObject.transform)
                    {
                        
                        
                    }
                }*/
            }
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public void calucalteAp()
    {
        Path path = seeker.StartPath(this.transform.position, aiLerp.target.position);
        path.BlockUntilCalculated();
        print( path.vectorPath.Count );
        apLost = (path.vectorPath.Count - 1) * movementCostMultiplier;
    }
    
    public void resetHP()
    {
        this.curHP = baseHP;
    }

    /*private IEnumerator ImSeenOrNot()
    {
        for (int i = 0; i < fov.visibleTargets.Count; i++)
        {
            fov.visibleTargets[i].GetComponent<Unit>().imSeen = true;

        }

        if (imSeen)
        {
            gameObject.layer = 10;

        }
        else
        {
            gameObject.layer = defaultLayer;
        }

        imSeen = false;
        yield return null;
    }*/
}
