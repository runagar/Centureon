using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //Cache the mapLayout
    SimpleMapGridCreation mapLayout;

    //Cache the prefabs for the units
    public GameObject player, meleeEnemy, rangedEnemy;

    //The object all units should be children of
    GameObject turnTracker;

    private void Start()
    {
        //Reference the map layout script
        mapLayout = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();
        turnTracker = GameObject.Find("TurnTracker");

        Instantiate(player, new Vector3(0, 0.25f, 0), Quaternion.identity, turnTracker.transform);
        Instantiate(meleeEnemy, new Vector3(9, 0.25f, 2), Quaternion.identity, turnTracker.transform);
        Instantiate(rangedEnemy, new Vector3(3, 0.25f, 7), Quaternion.identity, turnTracker.transform);
        Instantiate(meleeEnemy, new Vector3(8, 0.25f, 7), Quaternion.identity, turnTracker.transform);
        Instantiate(rangedEnemy, new Vector3(5, 0.25f, 5), Quaternion.identity, turnTracker.transform);

    }
}
