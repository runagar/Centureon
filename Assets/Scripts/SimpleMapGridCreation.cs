using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMapGridCreation : MonoBehaviour {
    public GameObject Tile;
    public GameObject pillarObstacle;
    public GameObject pits;

    GameObject[,] environmentObjects;
    //tiles can be removed as it is only being used for testing
    GameObject[,] tiles;
    //map layout the to is the button on the game screen
    int[,] map = {
                   { 3, 0, 0, 0, 0, 0, 0, 0, 0, 3},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
    };

    void Start(){
        environmentObjects = new GameObject[10, 8];
        tiles = new GameObject[10, 8];
        for (int i = 0; i < 10; i++){
            for (int j = 0; j < 8; j++){
                tiles[i , j] = Instantiate(Tile, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                if (map[j, i] == 0)
                {
                    tiles[i, j].transform.parent = gameObject.transform;
                }
                if (map[j, i] == 1)
                {
                    tiles[i, j].transform.parent = gameObject.transform;
                }
                if (map[j, i] == 2)
                {
                    environmentObjects[i, j] = Instantiate(pits, new Vector3(i, 0.05f, j), Quaternion.identity);
                    environmentObjects[i, j].transform.parent = gameObject.transform;
                    tiles[i, j].transform.parent = gameObject.transform;
                }
                if (map[j,i] == 3)
                {
                    environmentObjects[i, j] = Instantiate(pillarObstacle, new Vector3(i, 0.25f, j), Quaternion.identity);
                    environmentObjects[i, j].transform.parent = gameObject.transform;
                    tiles[i, j].transform.parent = gameObject.transform;
                }
            }
        }
    }
}
