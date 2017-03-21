using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTracker : MonoBehaviour {

    bool playerTookTurn;

    public GameObject[] enemies;
    int numberOfEnemies;
    int enemiesWhoHasTakesTheirTurn;

    ProjectileTracker projectiles;

	// Use this for initialization
	void Start () {
        playerTookTurn = false;
        enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        numberOfEnemies = enemies.Length;
        enemiesWhoHasTakesTheirTurn = 0;

        projectiles = GameObject.Find("ProjectileTracker").GetComponent<ProjectileTracker>();
	}
	
    public bool GetTurnBool()
    {
        return playerTookTurn;
    }

    public void PlayerTakeTurn()
    {
        playerTookTurn = true;
        projectiles.MoveProjectiles();
    }

    public void EnemiesTakeTurns()
    {
        enemiesWhoHasTakesTheirTurn++;

        if (enemiesWhoHasTakesTheirTurn == numberOfEnemies)
        {
            enemiesWhoHasTakesTheirTurn = 0;
            playerTookTurn = false;
        }
    }
}
