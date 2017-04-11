using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMelee : MonoBehaviour {
    TurnTracker turnTracker;
    float timeSinceLastMove;

    PlayerStats stats;

    // Use this for initialization
    void Start()
    {
        turnTracker = this.GetComponentInParent<TurnTracker>();
        timeSinceLastMove = 0;

        stats = this.gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("AttackUp"))
        {
            for(int i = 0; i < turnTracker.enemies.Length; i++)
            {
                if(turnTracker.enemies[i] != null && turnTracker.enemies[i].transform.position.z == transform.position.z + 1 && turnTracker.enemies[i].transform.position.x == transform.position.x)
                {
                    turnTracker.enemies[i].GetComponent<UnitStats>().isKill = "yes";
                    Destroy(turnTracker.enemies[i]);
                    turnTracker.enemies[i] = null;
                    turnTracker.PlayerTakeTurn();
                    break;
                }
            }
        }
        if (Input.GetButtonDown("AttackDown"))
        {
            for (int i = 0; i < turnTracker.enemies.Length; i++)
            {
                if (turnTracker.enemies[i] != null && turnTracker.enemies[i].transform.position.z == transform.position.z - 1 && turnTracker.enemies[i].transform.position.x == transform.position.x)
                {
                    turnTracker.enemies[i].GetComponent<UnitStats>().isKill = "yes";
                    Destroy(turnTracker.enemies[i]);
                    turnTracker.enemies[i] = null;
                    turnTracker.PlayerTakeTurn();
                    break;
                }
            }
        }
        if (Input.GetButtonDown("AttackLeft"))
        {
            for (int i = 0; i < turnTracker.enemies.Length; i++)
            {
                if (turnTracker.enemies[i] != null && turnTracker.enemies[i].transform.position.x == transform.position.x - 1 && turnTracker.enemies[i].transform.position.z == transform.position.z)
                {
                    turnTracker.enemies[i].GetComponent<UnitStats>().isKill = "yes";
                    Destroy(turnTracker.enemies[i]);
                    turnTracker.enemies[i] = null;
                    turnTracker.PlayerTakeTurn();
                    break;
                }
            }
        }
        if (Input.GetButtonDown("AttackRight"))
        {
            for (int i = 0; i < turnTracker.enemies.Length; i++)
            {
                if (turnTracker.enemies[i] != null && turnTracker.enemies[i].transform.position.x == transform.position.x + 1 && turnTracker.enemies[i].transform.position.z == transform.position.z)
                {
                    turnTracker.enemies[i].GetComponent<UnitStats>().isKill = "yes";
                    Destroy(turnTracker.enemies[i]);
                    turnTracker.enemies[i] = null;
                    turnTracker.PlayerTakeTurn();
                    break;
                }
            }
        }
    }
}
