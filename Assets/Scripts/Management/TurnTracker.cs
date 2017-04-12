using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTracker : MonoBehaviour {

    bool playerTookTurn;
    VictoryDefeatCondition VCcondition;
    public GameObject[] enemies;

    ProjectileTracker projectiles;

    SimpleMapGridCreation gridScript;
    Color baseColour;

	// Use this for initialization
	void Start () {
        VCcondition = GameObject.Find("LevelManager").GetComponent<VictoryDefeatCondition>();
        projectiles = GameObject.Find("ProjectileTracker").GetComponent<ProjectileTracker>();
        gridScript = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();
        baseColour = gridScript.Tile.GetComponent<Renderer>().sharedMaterial.color;
    }

    public void PlayerTakeTurn()
    {
        for (int i = 0; i < gridScript.mapSizeX; i++){
            for (int j = 0; j < gridScript.mapSizeY; j++)
            {
                if (gridScript.tiles[i, j].GetComponent<Renderer>().material.color == Color.red)
                    gridScript.tiles[i, j].GetComponent<Renderer>().material.color = baseColour;
            }
        }
        //projectiles.MoveProjectiles();      
        EnemiesTakeTurns();
    }

    public void EnemiesTakeTurns()
    {
        VCcondition.didPlayerMove = true;
        enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        foreach (GameObject o in enemies)
        {
            BaseMovement ms = o.GetComponent<BaseMovement>();
            ms.TriggerMovement(); 
        }
    }
}
