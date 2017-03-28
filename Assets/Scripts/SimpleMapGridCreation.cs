using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMapGridCreation : MonoBehaviour {
    public GameObject Tile;
    public GameObject pillarObstacle;
    public GameObject pits;

    GameObject[,] environmentObjects;
    //tiles can be removed as it is only being used for testing
    public GameObject[,] tiles;
    //map layout the to is the button on the game screen
    //0 indicates flat ground
    //2 indicates a hole/pit in the ground
    //3 indicates a pillar
    public int mapSizeX = 10;
    public int mapSizeY = 10;
    public int[,] map = {
                   { 0, 3, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 2, 0, 0},
                   { 0, 3, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 2, 0, 0, 0, 0, 0, 0, 3},
                   { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0},
                   { 0, 0, 2, 0, 0, 0, 0, 0, 0, 3},
                   { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0},
    };
    /*
    public int[,] map = {
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 10, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    };*/

    void Start(){
        environmentObjects = new GameObject[mapSizeX, mapSizeY];
        tiles = new GameObject[mapSizeX, mapSizeY];
        generateMapGridWithObstacles();
    }

    void generateMapGridWithObstacles()
    {
        for (int y = 0; y < mapSizeY; y++){
            for (int x = 0; x < mapSizeX; x++){
                tiles[x, y] = Instantiate(Tile, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                if (map[y, x] == 0)
                {
                    tiles[x, y].transform.parent = gameObject.transform;
                }
                if (map[y, x] == 1)
                {
                    tiles[x, y].transform.parent = gameObject.transform;
                }
                if (map[y, x] == 2)
                {
                    environmentObjects[x, y] = Instantiate(pits, new Vector3(x, 0.05f, y), Quaternion.identity);
                    environmentObjects[x, y].transform.parent = gameObject.transform;
                    tiles[x, y].GetComponent<Renderer>().material.color = Color.gray;
                    tiles[x, y].transform.parent = gameObject.transform;
                }
                if (map[y, x] == 3)
                {
                    environmentObjects[x, y] = Instantiate(pillarObstacle, new Vector3(x, 0.25f, y), Quaternion.identity);
                    tiles[x, y].GetComponent<Renderer>().material.color = Color.gray;
                    environmentObjects[x, y].transform.parent = gameObject.transform;
                    tiles[x, y].transform.parent = gameObject.transform;
                }
            }
        }
    }
}
