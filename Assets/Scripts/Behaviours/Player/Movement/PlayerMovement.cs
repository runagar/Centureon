using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Cache the turn tracker, and the map layout
    TurnTracker turnTracker;
    SimpleMapGridCreation mapLayout;

    //Initialize variables
    Vector3 movement;
    public Vector2 currentPos;
    float timeSinceLastMove;


    // Use this for initialization
    void Start()
    {
        turnTracker = this.GetComponentInParent<TurnTracker>();
        mapLayout = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();

        timeSinceLastMove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastMove += Time.deltaTime;
        currentPos = new Vector2(transform.position.x, transform.position.z);

            if (Input.GetButtonDown("UP") && timeSinceLastMove > 0.05)
            {
                MovePlayerPiece("z1");
            }
            if (Input.GetButtonDown("DOWN") && timeSinceLastMove > 0.05)
            {
                MovePlayerPiece("z-1");
            }
            if (Input.GetButtonDown("LEFT") && timeSinceLastMove > 0.05)
            {
                MovePlayerPiece("x-1");
            }
            if (Input.GetButtonDown("RIGHT") && timeSinceLastMove > 0.05)
            {
                MovePlayerPiece("x1");
            }
    }

    void MovePlayerPiece(string dir)
    {
        switch (dir) {
            case "z1":
                if (mapLayout.map[(int)currentPos.y + 1, (int)currentPos.x] != 0) break;
                else
                {
                    movement = new Vector3(0, 0, 1);
                    transform.position += movement;
                    timeSinceLastMove = 0;
                    turnTracker.PlayerTakeTurn();
                    break;
                }

            case "z-1":
                if (mapLayout.map[(int)currentPos.y - 1, (int)currentPos.x] != 0) break;
                else
                {
                    movement = new Vector3(0, 0, -1);
                    transform.position += movement;
                    timeSinceLastMove = 0;
                    turnTracker.PlayerTakeTurn();
                    break;
                }

            case "x-1":
                if (mapLayout.map[(int)currentPos.y, (int)currentPos.x - 1] != 0) break;
                else
                {
                    movement = new Vector3(-1, 0, 0);
                    transform.position += movement;
                    timeSinceLastMove = 0;
                    turnTracker.PlayerTakeTurn();
                    break;
                }

            case "x1":
                if (mapLayout.map[(int)currentPos.y, (int)currentPos.x + 1] != 0) break;
                else
                {
                    movement = new Vector3(1, 0, 0);
                    transform.position += movement;
                    timeSinceLastMove = 0;
                    turnTracker.PlayerTakeTurn();
                    break;
                }

            default:
                break;
        }
    }
    
}
