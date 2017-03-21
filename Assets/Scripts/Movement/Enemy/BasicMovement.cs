using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    TurnTracker turnTracker;

    Vector3 movement;
    
    GameObject player;

	// Use this for initialization
	void Start () {
        turnTracker = this.GetComponentInParent<TurnTracker>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (turnTracker.GetTurnBool())
        {
            float desiredMovement_X = player.transform.position.x - transform.position.x;
            float desiredMovement_Z = player.transform.position.z - transform.position.z;

            if (desiredMovement_X > 1) desiredMovement_X = 1;
            if (desiredMovement_Z > 1) desiredMovement_Z = 1;
            if (desiredMovement_X < -1) desiredMovement_X = -1;
            if (desiredMovement_Z < -1) desiredMovement_Z = -1;

            if (Mathf.Abs(desiredMovement_X) == Mathf.Abs(desiredMovement_Z))
            {
                if(Random.Range(0, 2) == 1) movement = new Vector3(desiredMovement_X, 0, 0);
                else movement = new Vector3(0, 0, desiredMovement_Z);
            }
            else if (Mathf.Abs(desiredMovement_X) > Mathf.Abs(desiredMovement_Z)) movement = new Vector3(desiredMovement_X, 0, 0);
            else movement = new Vector3(0, 0, desiredMovement_Z);

            transform.position += movement;
            turnTracker.TurnChange();
        }
	}
}
