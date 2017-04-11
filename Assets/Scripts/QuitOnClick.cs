using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitOnClick : MonoBehaviour {
	Button Mainmenu;

	void Update() {

	}
	void Start() { 
		Mainmenu = GetComponent<Button>();
	}
	public void OnMouseDown()
	{
		print("hi");
		Quit();
	}
	void Quit() { 
			Application.Quit();
	}
}
