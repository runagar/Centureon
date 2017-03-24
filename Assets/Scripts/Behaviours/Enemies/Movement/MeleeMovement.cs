using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMovement: MonoBehaviour {

    TurnTracker turnTracker;

    UnitStats stats;

    Vector3 movement;
    
    GameObject player;

	// Use this for initialization
	void Start () {
        turnTracker = this.GetComponentInParent<TurnTracker>();
        stats = this.GetComponent<UnitStats>();

        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

            float desiredMovement_X = player.transform.position.x - transform.position.x;
            float desiredMovement_Z = player.transform.position.z - transform.position.z;
            float rawX = Mathf.Abs(desiredMovement_X);
            float rawY = Mathf.Abs(desiredMovement_Z);

            if (desiredMovement_X > 1) desiredMovement_X = 1;
            if (desiredMovement_Z > 1) desiredMovement_Z = 1;
            if (desiredMovement_X < -1) desiredMovement_X = -1;
            if (desiredMovement_Z < -1) desiredMovement_Z = -1;

            if (Mathf.Abs(rawX) + Mathf.Abs(rawY) <= 2 )
            {
                movement = new Vector3(0, 0, 0);
            }


            else if (Mathf.Abs(desiredMovement_X) == Mathf.Abs(desiredMovement_Z))
            {
                if(Random.Range(0, 2) == 1) movement = new Vector3(desiredMovement_X, 0, 0);
                else movement = new Vector3(0, 0, desiredMovement_Z);
            }
            else if (Mathf.Abs(desiredMovement_X) > Mathf.Abs(desiredMovement_Z)) movement = new Vector3(desiredMovement_X, 0, 0);
            else movement = new Vector3(0, 0, desiredMovement_Z);

            transform.position += movement * stats.movementSpeed;
            turnTracker.EnemiesTakeTurns();
        
	}
}
