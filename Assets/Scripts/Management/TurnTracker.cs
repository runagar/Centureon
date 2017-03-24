using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTracker : MonoBehaviour {

    bool playerTookTurn;

    public GameObject[] enemies;

    ProjectileTracker projectiles;

	// Use this for initialization
	void Start () {
        enemies = GameObject.FindGameObjectsWithTag("ENEMY");

        projectiles = GameObject.Find("ProjectileTracker").GetComponent<ProjectileTracker>();
	}

    public void PlayerTakeTurn()
    {
        projectiles.MoveProjectiles();
        EnemiesTakeTurns();
    }

    public void EnemiesTakeTurns()
    {
        foreach(GameObject o in enemies)
        {
            BaseMovement ms = o.GetComponent<BaseMovement>();
            ms.TriggerMovement(); 
        }
    }
}
