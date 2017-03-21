using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breathFirst : MonoBehaviour {
    Vector2[,] parentList;
    int[,] myMap;
    List<Vector2> q;
    List<Vector2> burned;


    SimpleMapGridCreation map;
    GameObject mapGrid;

    void Start()
    {
        mapGrid = GameObject.Find("environmentObstacles");
        map = mapGrid.GetComponent<SimpleMapGridCreation>();
        myMap = map.map;

        q = new List<Vector2>();
        getPath(new Vector2(2, 2));
    }

    void createMypath(Vector2 startingPoint)
    {
        while (myMap[(int)q[0].y, (int)q[0].x] != myMap[(int)startingPoint.x, (int)startingPoint.y])
        {
            q[0] = parentList[(int)q[0].x, (int)q[0].y];
            map.tiles[(int)q[0].x, (int)q[0].y].GetComponent<Renderer>().material.color = Color.cyan;
        }
        q.RemoveAt(0);
    }

    void getPath(Vector2 startingPoint)
    {
        q.Add(startingPoint);
        while (q.Count > 0)
        {
            if(myMap[(int)q[0].x, (int)q[0].y] == 10)
            {
                createMypath(startingPoint);
                break;
            }


            if (!burned.Contains(q[0])) { 
            if(q[0].x > 0 && (myMap[(int)q[0].x - 1, (int)q[0].y] == 0))
            {
                parentList[(int)q[0].x-1, (int)q[0].y] = new Vector2(q[0].x, q[0].y);
                q.Add(new Vector2(q[0].x - 1, q[0].y));
            }
            if (q[0].x < map.mapSizeX - 1 && (myMap[(int)q[0].x + 1, (int)q[0].y] ==0))
            {
                parentList[(int)q[0].x + 1, (int)q[0].y] = new Vector2(q[0].x, q[0].y);
                q.Add(new Vector2(q[0].x + 1, q[0].y));
            }
            if(q[0].y > 0 && (myMap[(int)q[0].x, (int)q[0].y - 1] == 0))
            {
                parentList[(int)q[0].x, (int)q[0].y - 1] = new Vector2(q[0].x, q[0].y);
                q.Add(new Vector2(q[0].x, q[0].y - 1));
            }
            if(q[0].y < map.mapSizeY && (myMap[(int)q[0].x, (int)q[0].y + 1] == 0))
            {
                parentList[(int)q[0].x, (int)q[0].y + 1] = new Vector2(q[0].x, q[0].y);
                q.Add(new Vector2(q[0].x, q[0].y + 1));
            }
            burned.Add(q[0]);
            q.RemoveAt(0);
            }
        }
    }

}
