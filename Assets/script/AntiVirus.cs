using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirus : MonoBehaviour {



    public List<Unit> myUnits = new List<Unit>();

    Vector3  AntiVirusPos;



    void Start ()
    {
        AntiVirusPos = this.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < myUnits.Count; i++)
        {
            if (myUnits[i] == null)
            {
                RemoveFromList(i);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "Player2" && other.GetComponent<Unit>().curHP > 0)
        {
            Debug.Log("dsadasadsdasasdasdasdasdasdasdas");
            this.transform.position = AntiVirusPos;
            other.GetComponent<Unit>().hasAntiVirus = true;
        }

    }



    void RemoveFromList(int deathone)
    {
        myUnits.RemoveAt(deathone);
        for (int i = 0; i < myUnits.Count; i++)
        {
            if (myUnits[i] == null)
            {
                myUnits.RemoveAt(i);
            }
        }
    }
}
