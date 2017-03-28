using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour {

    //Cache the relevant scripts for unit behaviour.
    UnitStats stats;
    EnemyRanged rangedAttack;
    EnemyMelee meleeAttack;

    //Cache the player gameobject
    GameObject player;

    //Cache floats that hold differences in position between 
    float desiredMove_X, desiredMove_Z, absDelta_X, absDelta_Z;

    //Cache vector for change in movement
    Vector3 movementVector;


	void Start () {
        //Reference the cached scripts and objects
        stats = this.gameObject.GetComponent<UnitStats>();

        if (stats.isRanged) rangedAttack = this.GetComponent<EnemyRanged>();
        else meleeAttack = this.GetComponent<EnemyMelee>();

        player = GameObject.FindGameObjectWithTag("PLAYER");
    }

    //Method called in the TurnTracker script after the player has taken his turn
    public void TriggerMovement()
    {
        if(player != null) { 
        //Calculate the delta position floats.
        desiredMove_X = player.transform.position.x - transform.position.x;
        desiredMove_Z = player.transform.position.z - transform.position.z;
        absDelta_X = Mathf.Abs(desiredMove_X);
        absDelta_Z = Mathf.Abs(desiredMove_Z);

        //Call the correct movement method
        if (stats.isRanged) RangedMovement();
        else MeleeMovement();
        }
    }

    //Method for movement in the unit is ranged
    void RangedMovement()
    {
        //If the unit is charging an attack, conclude it and don't move.
        if (rangedAttack.GetAttackStatus())
        {
            rangedAttack.ConcludeAttack();
            movementVector = new Vector3(0, 0, 0);
        }

        //If the unit is on the same X or Z coordinate as the player
        if (desiredMove_X == 0 || desiredMove_Z == 0)
        {
            //Start charging attack, don't move.
            rangedAttack.ChargeAttack(player.transform.position - transform.position);
            movementVector = new Vector3(0, 0, 0);

            /* ------------------------------------- 
            Insert Actual Pathfinding here.
            ------------------------------------*/

            //Move the enemy by the path.
            transform.position += movementVector * stats.movementSpeed;
        }
    }

    //Method for movement if the unit is melee
    void MeleeMovement()
    {
        //If the unit is winding up to attack
        if (meleeAttack.GetAttackStatus())
        {
            //Conclude that attack, don't move.
            meleeAttack.ConcludeAttack();
            movementVector = new Vector3(0, 0, 0);
        }

        //If the manhatten-distance to the player is <= 2 (meaning either adjacent to player, or a player-adjacent square)
        else if (absDelta_X + absDelta_Z <= 2)
        {
            //we do not move but attack a adjcent square instead
            movementVector = new Vector3(0, 0, 0);

            //if the player is to the left of the enemy
            if (absDelta_X == -1 && absDelta_Z == 0)
            {
                meleeAttack.ChargeAttack(new Vector3(-1, 0, 0));
            }
            //if the player is to the right of the enemy
            else if (absDelta_X == 1 && absDelta_Z == 0)
            {
                meleeAttack.ChargeAttack(new Vector3(1, 0, 0));
            }
            //if the player is above the enemy
            else if (absDelta_X == 0 && absDelta_Z == -1)
            {
                meleeAttack.ChargeAttack(new Vector3(0, 0, -1));
            }
            //if the player is to the below of the enemy
            else if (absDelta_X == 0 && absDelta_Z == 1) {
                meleeAttack.ChargeAttack(new Vector3(0, 0, 1));
            }
            //if the player is to the left and above the enemy
            else if (absDelta_X == -1 && absDelta_Z == -1)
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        meleeAttack.ChargeAttack(new Vector3(-1, 0, 0));
                        break;
                    case 1:
                        meleeAttack.ChargeAttack(new Vector3(0, 0, -1));
                        break;
                }
            }
            //if the player is to the left and below the enemy
            else if (absDelta_X == -1 && absDelta_Z == 1)
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        meleeAttack.ChargeAttack(new Vector3(-1, 0, 0));
                        break;
                    case 1:
                        meleeAttack.ChargeAttack(new Vector3(0, 0, 1));
                        break;
                }
            }
            //if the player is to the right and above the enemy
            else if (absDelta_X == 1 && absDelta_Z == -1)
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        meleeAttack.ChargeAttack(new Vector3(1, 0, 0));
                        break;
                    case 1:
                        meleeAttack.ChargeAttack(new Vector3(0, 0, -1));
                        break;
                }
            }
            //if the player is to the right and below the player
            else if (absDelta_X == 1 && absDelta_Z == 1)
            {
                switch (Random.Range(0, 2)) {
                    case 0:
                        meleeAttack.ChargeAttack(new Vector3(1, 0, 0));
                        break;
                    case 1:
                        meleeAttack.ChargeAttack(new Vector3(0, 0, 1));
                        break;
                }
                
            }

        }

        /* ------------------------------------- 
            Insert Actual Pathfinding here.
            ------------------------------------*/
        
        //Move the enemy by the path.
        transform.position += movementVector * stats.movementSpeed;
    }
}
