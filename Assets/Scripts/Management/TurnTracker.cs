using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTracker : MonoBehaviour {

    bool playerTookTurn;

	// Use this for initialization
	void Start () {
        playerTookTurn = false;
	}
	
    public bool GetTurnBool()
    {
        return playerTookTurn;
    }

    public void TurnChange()
    {
        playerTookTurn = !playerTookTurn;
    }
}
