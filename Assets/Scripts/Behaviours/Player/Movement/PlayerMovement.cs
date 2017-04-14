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
	public SpriteRenderer mySpriteRenderer;

    // Use this for initialization
    void Start()
    {
        turnTracker = this.GetComponentInParent<TurnTracker>();
        mapLayout = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();
		mySpriteRenderer = GameObject.Find("Sprite").GetComponentInParent<SpriteRenderer>();
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
            if(Input.GetKeyDown(KeyCode.Space) && timeSinceLastMove > 0.5)
            {
                turnTracker.PlayerTakeTurn();
                turnTracker.PlayerTakeTurn();
        }
    }

    void MovePlayerPiece(string dir)
    {
        bool enemyInTheWay = false;
        switch (dir) {
            case "z1":
                foreach(GameObject o in turnTracker.enemies)
                {
                    if (o != null && o.transform.position == transform.position + new Vector3(0, 0, 1))
                    {
                        enemyInTheWay = true;
                        break;
                    }
                    else enemyInTheWay = false;
                }

                if (mapLayout.map[(int)currentPos.y + 1, (int)currentPos.x] != 0) break;
                else if(!enemyInTheWay)
                {
                    movement = new Vector3(0, 0, 1);
                    transform.position += movement;
                    timeSinceLastMove = 0;
                    turnTracker.PlayerTakeTurn();
                }
                break;

            case "z-1":
                foreach (GameObject o in turnTracker.enemies)
                {
                    if (o != null && o.transform.position == transform.position + new Vector3(0, 0, -1))
                    {
                        enemyInTheWay = true;
                        break;
                    }
                    else enemyInTheWay = false;
                }
                if (mapLayout.map[(int)currentPos.y - 1, (int)currentPos.x] != 0) break;
                else if (!enemyInTheWay)
                {
                    movement = new Vector3(0, 0, -1);
                    transform.position += movement;
                    timeSinceLastMove = 0;
                    turnTracker.PlayerTakeTurn();
                }
                break;

            case "x-1":
                foreach (GameObject o in turnTracker.enemies)
                {
                    if (o != null && o.transform.position == transform.position + new Vector3(-1, 0, 0))
                    {
                        enemyInTheWay = true;
                        break;
                    }
                    else enemyInTheWay = false;
                }
                if (mapLayout.map[(int)currentPos.y, (int)currentPos.x - 1] != 0) break;
                else if (!enemyInTheWay)
                {
                    mySpriteRenderer.flipX = true;
                    movement = new Vector3(-1, 0, 0);
                    transform.position += movement;
                    timeSinceLastMove = 0;
                    turnTracker.PlayerTakeTurn();
                }
                break;

            case "x1":
                foreach (GameObject o in turnTracker.enemies)
                {
                    if (o != null && o.transform.position == transform.position + new Vector3(1, 0, 0))
                    {
                        enemyInTheWay = true;
                        break;
                    }
                    else enemyInTheWay = false;
                }
                if (mapLayout.map[(int)currentPos.y, (int)currentPos.x + 1] != 0) break;
                else if (!enemyInTheWay)
                {
                    mySpriteRenderer.flipX = false;
                    movement = new Vector3(1, 0, 0);
                    transform.position += movement;
                    timeSinceLastMove = 0;
                    turnTracker.PlayerTakeTurn();
                }
                break;

            default:
                break;
        }
    }
    
}
