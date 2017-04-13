using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour {

	Tutorial check;


	// Use this for initialization
	void Start () {
		check = GameObject.Find("Logger").GetComponent<Tutorial>();
		if (check.checkker == true && check.tutorialcheck == false)
			this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
