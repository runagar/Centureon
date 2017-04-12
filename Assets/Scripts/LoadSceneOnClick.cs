using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour {

	static int selected = 0;
	public int sceneIndex;
	bool check = false;
	Animator anim;
	Animation faded;
	//mainMenuVariables varKeeper;
	Button Mainmenu;
	//public Sprite MainMenuSprite;


    void Start()
    {
		anim = gameObject.GetComponent<Animator>();
		//faded = gameObject.GetComponentInChildren<Animation>();
	    //varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();
		Mainmenu = GetComponent<Button>();
		//Mainmenu.image.sprite = MainMenuSprite;
    }
	//!this.anim.GetCurrentAnimatorStateInfo(0).IsName("New Animation")
	void Update()
	{
		if(check == false)
		animation();
	}
    public void loadMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void dnd()
    {
  //      varKeeper.useDragonDrop = true;
    }
    public void text()
    {
//        varKeeper.useDragonDrop = false;
    }
	public void OnMouseDown()
	{
		SceneManager.LoadScene(sceneIndex);
	}

	void animation() {
		if (sceneIndex == 0)
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				SceneManager.LoadScene("mainMenu");
			}
			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
			{
				anim.Stop();
				check = true;
				SceneManager.LoadScene("mainMenu");
			}
		}
		else { }
	}
}