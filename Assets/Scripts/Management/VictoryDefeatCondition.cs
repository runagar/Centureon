using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryDefeatCondition : MonoBehaviour {

    TurnTracker turntracker;
    GameObject player;
   	public int currentIndex;
    public bool didPlayerMove;
	SimpleMapGridCreation press;

    // Use this for initialization
    void Start () {
        turntracker = GameObject.Find("TurnTracker").GetComponent<TurnTracker>();
      //  currentIndex = this.GetComponent<levelManager>().getCurrentLevel();
        didPlayerMove = false;
        player = GameObject.FindGameObjectWithTag("PLAYER");
		press = GameObject.Find("MapLayout").GetComponent<SimpleMapGridCreation>();
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
            if (currentIndex == 5){
                SceneManager.LoadScene("mainMenu");
            }
            else {
				currentIndex+=1;
                SceneManager.LoadScene(currentIndex);
            }
        }
	}

    public void reload()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("mainMenu");
        Time.timeScale = 1;
        didPlayerMove = false;
    }
}
