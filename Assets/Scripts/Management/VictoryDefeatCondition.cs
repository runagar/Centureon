using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryDefeatCondition : MonoBehaviour {

    TurnTracker turntracker;
    GameObject player;
    int currentIndex;
    public bool didPlayerMove;
    // Use this for initialization
    void Start () {
        turntracker = GameObject.Find("TurnTracker").GetComponent<TurnTracker>();
        player = GameObject.FindGameObjectWithTag("PLAYER");
        currentIndex = this.GetComponent<levelManager>().getCurrentLevel();
        didPlayerMove = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(player == null)
        {
            Debug.Log("defeated");
            //defeat
            SceneManager.LoadScene(0);

        }
        if(turntracker.enemies.Length <= 0 && didPlayerMove == true)
        {
            Debug.Log("victory");
            //victory
            SceneManager.LoadScene(currentIndex + 1);
        }
	}
}
