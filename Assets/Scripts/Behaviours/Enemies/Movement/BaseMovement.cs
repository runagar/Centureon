using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour {

    //Cache the relevant scripts for unit behaviour.
    UnitStats stats;
    EnemyRanged rangedAttack;
    EnemyMelee meleeAttack;
	public SpriteRenderer mySpriteRenderer;

    //Cache the player gameobject
    GameObject player;

    //Cache floats that hold differences in position
    public float desiredMove_X, desiredMove_Z, absDelta_X, absDelta_Z;

    //Cache necessities for movement
    Vector3 currentDesiredPosition;
    Vector3 myDesiredPosition;
    List<Vector3> possibleRangedPositions;
    Vector3 movementVector;
    SimpleMapGridCreation gridScript;
    int[,] map;
    int attackRange;

    TurnTracker turnTracker;

    public int xBlocked;
    public int zBlocked;

    void Start () {
        //Reference the cached scripts and objects
        stats = this.gameObject.GetComponent<UnitStats>();
        mySpriteRenderer = transform.FindChild("SpriteEnemy").GetComponent<SpriteRenderer>();
        if (stats.isRanged) rangedAttack = this.GetComponent<EnemyRanged>();
        else meleeAttack = this.GetComponent<EnemyMelee>();

        player = GameObject.FindGameObjectWithTag("PLAYER");
        gridScript = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();
        map = gridScript.map;

        turnTracker = GameObject.Find("TurnTracker").GetComponent<TurnTracker>();

        attackRange = stats.range;
    }

    //Method called in the TurnTracker script after the player has taken his turn
    public void TriggerMovement()
    {
        if(player != null) { 
        //Calculate the delta position floats.
        desiredMove_X = player.transform.position.x - transform.position.x;
        desiredMove_Z = player.transform.position.z - transform.position.z;
        absDelta_X = Mathf.Abs(desiredMove_X);
        absDelta_Z = Mathf.Abs(desiredMove_Z);

        //Call the correct movement method
        if (stats.isRanged) RangedMovement();
        else MeleeMovement();
        }
    }

    //Method for movement in the unit is ranged
    void RangedMovement()
    {
        //If the unit is charging an attack, conclude it and don't move.
        if (rangedAttack.GetAttackStatus())
        {
            rangedAttack.ConcludeAttack();
            movementVector = new Vector3(0, 0, 0);
        }

        Vector3 desiredPosition = desiredRangedPosition(player);
        Debug.Log(desiredPosition);
        //If the unit is on the same X or Z coordinate as the player
        if (this.transform.position == desiredPosition)
        {
            //Start charging attack, don't move.
            rangedAttack.ChargeAttack(player.transform.position - transform.position);
			movementVector = new Vector3(0, 0, 0);
        }
        else
        {
            Vector2 start = new Vector2(transform.position.x, transform.position.z);
            Vector2 goal = new Vector2(desiredPosition.x, desiredPosition.z);
            Vector2 tempMovement = pathfinding(start, goal)[0];

            movementVector = new Vector3(tempMovement.x, 0, tempMovement.y) - transform.position;
        }

        //Move the enemy by the path.
        /*if (movementVector.x == -1 || movementVector.z == -1)
        {
            mySpriteRenderer.flipX = true;
            if (stats.range == 2) mySpriteRenderer.transform.position = new Vector3(-0.6f, 1.5f, 0);
        }
        else
        {
            mySpriteRenderer.flipX = false;
            if (stats.range == 2) mySpriteRenderer.transform.position = new Vector3(0.6f, 1.5f, 0);
        }*/
        transform.position += new Vector3(movementVector.x, 0, movementVector.z);
    }

	//Method for movement if the unit is melee
	void MeleeMovement()
	{
		Vector2 start = new Vector2(transform.position.x, transform.position.z);
		Vector2 goal = new Vector2(player.transform.position.x, player.transform.position.z);
		Vector2 tempMovement = pathfinding(start, goal)[0];

		movementVector = new Vector3(tempMovement.x, 0, tempMovement.y) - transform.position;

		//If the unit is winding up to attack
		if (meleeAttack.GetAttackStatus())
		{
			//Conclude that attack, don't move.
			meleeAttack.ConcludeAttack();
			movementVector = new Vector3(0, 0, 0);
		}

		//If the manhatten-distance to the player is <= 2 (meaning either adjacent to player, or a player-adjacent square)
		else if (absDelta_X + absDelta_Z <= (1 + 1 * attackRange))
		{

			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					//if we found the i and j with the same sign as the desired direction, and they are not both zero
					if (Mathf.Sign(desiredMove_X) == Mathf.Sign(i) && Mathf.Sign(desiredMove_Z) == Mathf.Sign(j) && (i != 0 && j != 0))
					{
						//Ints for determining if positions are blocked
						xBlocked = 0;
						zBlocked = 0;

						//based on the range of the unit, go through all the relevant tiles and check if they are blocked.
						for (int r = 0; r < attackRange; r++)
						{
							if (gridScript.map[(int)transform.position.z, (int)transform.position.x + (r + 1) * i] != 0 && xBlocked == 0) xBlocked = r + 1;
							if (gridScript.map[(int)transform.position.z + (r + 1) * j, (int)transform.position.x] != 0 && zBlocked == 0) zBlocked = r + 1;
						}

						//If it is completely blocked in both directions
						if (xBlocked == 1 && zBlocked == 1)
						{
							i = 2;
							break;
						}

						//If the target is at a diagonal to the enemy
						if (absDelta_X == absDelta_Z)
						{
							//If only blocked in the x direction, attack in the z-direction
							if (xBlocked == 1 && (zBlocked == 2 || zBlocked == 0))
							{
								meleeAttack.ChargeAttack(new Vector3(0, 0, j * attackRange));
								movementVector = new Vector3(0, 0, 0); //Set move to zero so we stay where we are.
								i = 2;
								break;
							}
							//if only blocked in the z-direction, attack along x
							else if ((xBlocked == 2 || xBlocked == 0) && zBlocked == 1)
							{
								meleeAttack.ChargeAttack(new Vector3(i * attackRange, 0, 0));
								movementVector = new Vector3(0, 0, 0);//Set move to zero so we stay where we are.
								i = 2;
								break;
							}

							//if both ways are open, attack a random one.
							else
							{
								switch (Random.Range(0, 2))
								{
									case 0:
										meleeAttack.ChargeAttack(new Vector3(i * attackRange, 0, 0));
										movementVector = new Vector3(0, 0, 0);//Set move to zero so we stay where we are.
										break;
									case 1:
										meleeAttack.ChargeAttack(new Vector3(0, 0, j * attackRange));
										movementVector = new Vector3(0, 0, 0);//Set move to zero so we stay where we are.
										break;
								}
							}
							i = 2;
							break;
						}
						//if the target is closest to Z
						if (absDelta_X < absDelta_Z)
						{
							//if z is not blocked, or it is blocked two tiles away, but the enemy is on the immediately adjacent tile
							if (zBlocked == 0 || (zBlocked == 2 && absDelta_X + absDelta_Z == 1))
							{
								meleeAttack.ChargeAttack(new Vector3(0, 0, j * attackRange));
								movementVector = new Vector3(0, 0, 0);//Set move to zero so we stay where we are.
								i = 2;
								break;
							}
						}
						//if the target is closest to X and x is not blocked, or it is blocked two tiles away, but the enemy is on the immediately adjacent tile
						else if (xBlocked == 0 || (xBlocked == 2 && absDelta_X + absDelta_Z == 1))
						{
							meleeAttack.ChargeAttack(new Vector3(i * attackRange, 0, 0));
							movementVector = new Vector3(0, 0, 0);//Set move to zero so we stay where we are.
							i = 2;
							break;
						}
					}
				}
			}
		}

		//Move the enemy by the path.
		if (movementVector.x == -1 || movementVector.z == -1){
			mySpriteRenderer.flipX = true;
	}
		else{
			mySpriteRenderer.flipX = false;
	}


        transform.position += new Vector3(movementVector.x, 0, movementVector.z);
        this.transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);



    }

    //The pathfinding algorithm. Breadth first.
    List<Vector2> pathfinding(Vector2 start, Vector2 goal)
    {
        List<Vector2> open = new List<Vector2>();                                   //List of nodes we have not yet looked at
        List<Vector2> burned = new List<Vector2>();                                 //List of nodes we have already looked at
        List<Vector2> path = new List<Vector2>();                                   //Vector holds the destination we want to go to (out return value)
        Vector2[,] parents = new Vector2[gridScript.mapSizeX, gridScript.mapSizeY]; //2D array hold the parents for each location

        //Add the start node to the open list
        open.Add(start);
        Vector2 current;

        while(open.Count > 0)
        {
            //Set our current node to the first in the open list, and save x and y coordinates of the current pos for ease of reference
            current = open[0];
            int x = (int)current.x;
            int y = (int)current.y;

            //if we have found the shortest route to the goal
            if (current == goal)
            {
                while(current != start)
                {
                    path.Insert(0, current);
                    current = parents[(int)current.x, (int)current.y];
                }
                break;
            }

            //Go through the four neighbouring tiles
            for(int i = -1; i < 2; i++){
                for (int j = -1; j < 2; j++)
                {
                    if (Mathf.Abs(i) == Mathf.Abs(j)) continue;
                    else if (x + i >= 0 && x + i < gridScript.mapSizeX && y + j >= 0 && y + j < gridScript.mapSizeY)
                    {
                        //Assign the neighbour pos we are looking at as a temporary vector.
                        Vector2 temp = new Vector2(x + i, y + j);

                        //If that neighbour is neither in the open list nor the burned list
                        if (!open.Contains(temp) && !burned.Contains(temp) && map[(int)temp.y, (int)temp.x] == 0)
                        {
                            foreach (GameObject o in turnTracker.enemies)
                            {
                                if (temp.x == o.transform.position.x && temp.y == o.transform.position.z) break;
                                else if (o == turnTracker.enemies[turnTracker.enemies.Length - 1])
                                {
                                    //Add it to the open list, and define which pos we came to here from.
                                    open.Add(temp);
                                    parents[(int)temp.x, (int)temp.y] = current;
                                }
                            }
                        }
                    }
                }
            }

            //Burn the current pos, and remove it from the open list.
            burned.Add(current);
            open.RemoveAt(0);
        }
        
        return path;
    }

    Vector3 desiredRangedPosition(GameObject player) {
        List<Vector3> desiredPositions = new List<Vector3>();
        List<Vector3> adjecentSquares = new List<Vector3>();
        List<Vector3> possibleSquares = new List<Vector3>();
        
        for(int i = -1; i < 2; i++){
            for(int j = -1; j < 2; j++){
                if(Mathf.Abs(i) != Mathf.Abs(j))
                {
                    adjecentSquares.Add(new Vector3(player.transform.position.x + i, 0.25f, player.transform.position.z + j));
                }
            }
        }

         if (!Physics.Linecast(adjecentSquares[0], new Vector3(adjecentSquares[0].x, 0.25f, this.transform.position.z)))
         {
                desiredPositions.Add(new Vector3(adjecentSquares[0].x, 0.25f, this.transform.position.z));
         }
         if (!Physics.Linecast(adjecentSquares[0], new Vector3(this.transform.position.x, 0.25f, adjecentSquares[0].z)))
         {
                desiredPositions.Add(new Vector3( this.transform.position.x, 0.25f, adjecentSquares[0].z));
         }

        if (!Physics.Linecast(adjecentSquares[1], new Vector3(adjecentSquares[1].x, 0.25f, this.transform.position.z)))
        {
            desiredPositions.Add(new Vector3(adjecentSquares[1].x, 0.25f, this.transform.position.z));
        }
        if (!Physics.Linecast(adjecentSquares[1], new Vector3(this.transform.position.x, 0.25f, adjecentSquares[1].z)))
        {
            desiredPositions.Add(new Vector3(this.transform.position.x, 0.25f, adjecentSquares[1].z));
        }

        if (!Physics.Linecast(adjecentSquares[2], new Vector3(adjecentSquares[2].x, 0.25f, this.transform.position.z)))
        {
            desiredPositions.Add(new Vector3(adjecentSquares[2].x, 0.25f, this.transform.position.z));
        }
        if (!Physics.Linecast(adjecentSquares[2], new Vector3(this.transform.position.x, 0.25f, adjecentSquares[2].z)))
        {
            desiredPositions.Add(new Vector3(this.transform.position.x, 0.25f, adjecentSquares[2].z));
        }

        if (!Physics.Linecast(adjecentSquares[3], new Vector3(adjecentSquares[3].x, 0.25f, this.transform.position.z)))
        {
            desiredPositions.Add(new Vector3(adjecentSquares[3].x, 0.25f, this.transform.position.z));
        }
        if (!Physics.Linecast(adjecentSquares[3], new Vector3(this.transform.position.x, 0.25f, adjecentSquares[3].z)))
        {
            desiredPositions.Add(new Vector3(this.transform.position.x, 0.25f, adjecentSquares[3].z));
        }

        float lowestHcost = Mathf.Infinity;
        float hCost;
        foreach(Vector3 v in desiredPositions)
        {
            hCost = ((v.x - this.transform.position.x) + (v.z - this.transform.position.z));

            if (v.x == player.transform.position.x || v.z == player.transform.position.z) { 
                hCost = hCost - 2;
            }

            if (hCost < lowestHcost){
                lowestHcost = hCost;
                myDesiredPosition = v;
            }
        }

        return myDesiredPosition;
    }
}
