using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //Cache the mapLayout
    SimpleMapGridCreation mapLayout;

    //Cache the prefabs for the units
    public GameObject player, swordEnemy, spearEnemy;

    //The object all units should be children of
    GameObject turnTracker;

    //Cache the current level we are in 
    int currentLevel;

    private void Start()
    {
        //Reference the map layout script
        mapLayout = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();
        turnTracker = GameObject.Find("TurnTracker");
        currentLevel = GameObject.Find("LevelManager").GetComponent<levelManager>().getCurrentLevel();

        if (currentLevel == 0){
            Instantiate(player, new Vector3(0, 0.25f, 0), Quaternion.identity, turnTracker.transform);
            Instantiate(swordEnemy, new Vector3(9, 0.25f, 2), Quaternion.identity, turnTracker.transform);
            Instantiate(swordEnemy, new Vector3(3, 0.25f, 7), Quaternion.identity, turnTracker.transform);
            Instantiate(swordEnemy, new Vector3(8, 0.25f, 7), Quaternion.identity, turnTracker.transform);
            Instantiate(swordEnemy, new Vector3(5, 0.25f, 5), Quaternion.identity, turnTracker.transform);
        }
        else if (currentLevel == 1){
            Instantiate(player, new Vector3(0, 0.25f, 0), Quaternion.identity, turnTracker.transform);
            Instantiate(swordEnemy, new Vector3(9, 0.25f, 2), Quaternion.identity, turnTracker.transform);
            Instantiate(swordEnemy, new Vector3(3, 0.25f, 7), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(8, 0.25f, 5), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(4, 0.25f, 2), Quaternion.identity, turnTracker.transform);
            Instantiate(swordEnemy, new Vector3(0, 0.25f, 6), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(6, 0.25f, 0), Quaternion.identity, turnTracker.transform);
        }
        else{
            Instantiate(player, new Vector3(0, 0.25f, 0), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(0, 0.25f, 9), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(6, 0.25f, 7), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(9, 0.25f, 2), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(5, 0.25f, 3), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(1, 0.25f, 4), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(4, 0.25f, 0), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(8, 0.25f, 5), Quaternion.identity, turnTracker.transform);
            Instantiate(spearEnemy, new Vector3(3, 0.25f, 7), Quaternion.identity, turnTracker.transform);
        }
    }
}
