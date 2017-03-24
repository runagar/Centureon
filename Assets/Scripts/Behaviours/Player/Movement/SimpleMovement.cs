using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour {

    TurnTracker turnTracker;
    SimpleMapGridCreation mapLayout;

    Vector3 movement;
    float timeSinceLastMove;
    public Vector2 currentPos;


    // Use this for initialization
    void Start () {
        turnTracker = this.GetComponentInParent<TurnTracker>();
        mapLayout = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();

        timeSinceLastMove = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastMove += Time.deltaTime;
        currentPos = new Vector2(transform.position.x, transform.position.z);

        if (Input.GetButtonDown("UP") && timeSinceLastMove > 0.05)
        {
            movement = new Vector3(0, 0, 1);
            transform.position += movement;
            timeSinceLastMove = 0;
            turnTracker.PlayerTakeTurn();
        }
        if (Input.GetButtonDown("DOWN") && timeSinceLastMove > 0.05)
        {
            movement = new Vector3(0, 0, -1);
            transform.position += movement;
            timeSinceLastMove = 0;
            turnTracker.PlayerTakeTurn();
        }
        if (Input.GetButtonDown("LEFT") && timeSinceLastMove > 0.05)
        {
            movement = new Vector3(-1, 0, 0);
            transform.position += movement;
            timeSinceLastMove = 0;
            turnTracker.PlayerTakeTurn();
        }
        if (Input.GetButtonDown("RIGHT") && timeSinceLastMove > 0.05)
        {
            movement = new Vector3(1, 0, 0);
            transform.position += movement;
            timeSinceLastMove = 0;
            turnTracker.PlayerTakeTurn();
        }
    }
}
