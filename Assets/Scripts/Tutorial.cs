using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public bool tutorialcheck = true;
	SimpleMapGridCreation tutorial;
	public bool checkker = false;

	// Use this for initialization
	void Start () {
		tutorial = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();
	}
	
	// Update is called once per fame
	void Update () {
		
	}
}
