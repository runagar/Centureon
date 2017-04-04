using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTelegraphs : MonoBehaviour {

	public void startTelegraph(List<Vector2> targetTiles)
    {
        print("Attacking: " + targetTiles);
    }

    public void endTelegraph(List<Vector2> targetTiles)
    {
        print("Hit: " + targetTiles);
    }
}
