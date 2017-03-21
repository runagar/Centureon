using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMovement: MonoBehaviour {

    TurnTracker turnTracker;
    RangedBasic attackScript;
    UnitStats stats;

    Vector3 movement;
    
    GameObject player;

    // Use this for initialization
    void Start () {
        turnTracker = this.GetComponentInParent<TurnTracker>();
        attackScript = this.GetComponent<RangedBasic>();
        stats = this.GetComponent<UnitStats>();

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

            if (attackScript.GetAttackStatus())
            {
                attackScript.ConcludeAttack();
                movement = new Vector3(0, 0, 0);
            }

            if (desiredMovement_X == 0 || desiredMovement_Z == 0)
            {
                attackScript.ChargeAttack(player.transform.position - transform.position);
                movement = new Vector3(0, 0, 0);
            }

            else if (Mathf.Abs(player.transform.position.x - transform.position.x) < Mathf.Abs(player.transform.position.z - transform.position.z)) movement = new Vector3(desiredMovement_X, 0, 0);
            else movement = new Vector3(0, 0, desiredMovement_Z);

            transform.position += movement * stats.movementSpeed;
            turnTracker.EnemiesTakeTurns();
        }
	}
}
