using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    
    public Transform tilePosition;
    public bool isWalkable = false;
    
    

    void Start ()
    {
        
        tilePosition = this.transform;
    }
	
	
	void Update ()
    {
        
	}
}
