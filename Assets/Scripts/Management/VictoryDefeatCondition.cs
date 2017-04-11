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
        currentIndex = this.GetComponent<levelManager>().getCurrentLevel();
        didPlayerMove = false;
        player = GameObject.FindGameObjectWithTag("PLAYER");
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player == null & didPlayerMove == true)
        {
            Debug.Log("defeated");
            //defeat
            //SceneManager.LoadScene(currentIndex);
            reload();

        }
        if(turntracker.enemies.Length <= 0 && didPlayerMove == true)
        {
            Debug.Log("victory");
            //victory
            if (currentIndex == 2){
                SceneManager.LoadScene(0);
            }
            else {
                SceneManager.LoadScene(currentIndex + 1);
            }
        }
	}

    public void reload()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
        didPlayerMove = false;
    }
}
