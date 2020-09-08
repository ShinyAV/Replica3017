using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

	public BaseHumans human;


	// Use this for initialization
	void Start () {

		human = GetComponent<BaseHumans> ();






	}

	// Update is called once per frame
	void Update () {
		human.movement = 4;
	}
}
