using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMelee : MonoBehaviour {
    TurnTracker turnTracker;
    float timeSinceLastMove;
	private AudioSource audio;
	public AudioClip death;
	SimpleMapGridCreation press;

    PlayerStats stats;

    // Use this for initialization
    void Start()
    {
		audio = this.GetComponent<AudioSource>();
		audio.clip = death;
		turnTracker = this.GetComponentInParent<TurnTracker>();
        timeSinceLastMove = 0;

        stats = this.gameObject.GetComponent<PlayerStats>();
		press = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();

    }
	void teleport() {
		
	}
    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("AttackUp"))
        {
            for(int i = 0; i < turnTracker.enemies.Length; i++)
            {
                if(turnTracker.enemies[i] != null && turnTracker.enemies[i].transform.position.z == transform.position.z + 1 && turnTracker.enemies[i].transform.position.x == transform.position.x)
                {
					audio.Play();
					turnTracker.enemies[i].GetComponent<UnitStats>().isKill = "yes";
                    Destroy(turnTracker.enemies[i]);
                    turnTracker.enemies[i] = null;
                    turnTracker.PlayerTakeTurn();
                    break;
                }
            }
        }
        if (Input.GetButtonDown("AttackDown"))
        {
            for (int i = 0; i < turnTracker.enemies.Length; i++)
            {
                if (turnTracker.enemies[i] != null && turnTracker.enemies[i].transform.position.z == transform.position.z - 1 && turnTracker.enemies[i].transform.position.x == transform.position.x)
                {
					audio.Play();
                    turnTracker.enemies[i].GetComponent<UnitStats>().isKill = "yes";
                    Destroy(turnTracker.enemies[i]);
                    turnTracker.enemies[i] = null;
                    turnTracker.PlayerTakeTurn();
                    break;
                }
            }
        }
        if (Input.GetButtonDown("AttackLeft"))
        {
            for (int i = 0; i < turnTracker.enemies.Length; i++)
            {
                if (turnTracker.enemies[i] != null && turnTracker.enemies[i].transform.position.x == transform.position.x - 1 && turnTracker.enemies[i].transform.position.z == transform.position.z)
                {
					audio.Play();
                    turnTracker.enemies[i].GetComponent<UnitStats>().isKill = "yes";
                    Destroy(turnTracker.enemies[i]);
                    turnTracker.enemies[i] = null;
                    turnTracker.PlayerTakeTurn();
                    break;
                }
            }
        }
        if (Input.GetButtonDown("AttackRight"))
        {
            for (int i = 0; i < turnTracker.enemies.Length; i++)
            {
                if (turnTracker.enemies[i] != null && turnTracker.enemies[i].transform.position.x == transform.position.x + 1 && turnTracker.enemies[i].transform.position.z == transform.position.z)
                {
					audio.Play();
                    turnTracker.enemies[i].GetComponent<UnitStats>().isKill = "yes";
                    Destroy(turnTracker.enemies[i]);
                    turnTracker.enemies[i] = null;
                    turnTracker.PlayerTakeTurn();
                    break;
                }
            }
        }
    }
}
