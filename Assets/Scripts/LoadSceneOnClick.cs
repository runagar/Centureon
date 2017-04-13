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
	GameObject start, title, quit,loading, Archimedes;
	//mainMenuVariables varKeeper;
	Button Mainmenu;
	//public Sprite MainMenuSprite;


    void Start()
    {
		anim = gameObject.GetComponent<Animator>();
		//faded = gameObject.GetComponentInChildren<Animation>();
	    //varKeeper = GameObject.Find("KeeperOfVariables").GetComponent<mainMenuVariables>();
		Mainmenu = GetComponent<Button>();
		start = GameObject.Find("/Start");
		title = GameObject.Find("/Title");
		quit = GameObject.Find("/Quit");
		loading = GameObject.Find("/Loading");
		Archimedes =GameObject.Find("/Archimedes");
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
		start.SetActive(false);
		title.SetActive(false);
		quit.SetActive(false);
		loading.transform.position = new Vector3(0, 1.6f, 4.28f);
		Archimedes.transform.position = new Vector3(0.05f, -0.7f, 2.92f);

	}

	void animation() {
		if (sceneIndex == 0)
		{
			if (Input.anyKey)
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