using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTelegraphs : MonoBehaviour {

    SimpleMapGridCreation gridScript;

    private void Start()
    {
        gridScript = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();
    }

    public void startTelegraph(List<Vector2> targetTiles)
    {
        foreach(Vector2 v in targetTiles)
        {
            if (v.x >= 0 && v.x < gridScript.mapSizeX && v.y >= 0 && v.y < gridScript.mapSizeY)
                gridScript.tiles[(int)v.x, (int)v.y].GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
