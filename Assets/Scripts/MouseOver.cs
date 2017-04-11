using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;




public class MouseOver : MonoBehaviour
{
	_2dxFX_AL_Outline outline;
	_2dxFX_Shiny_Reflect reflection;
	// Use this for initialization
	void Start()
	{
		outline = this.gameObject.GetComponent<_2dxFX_AL_Outline>();
		reflection = this.gameObject.GetComponent<_2dxFX_Shiny_Reflect>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnMouseEnter()
	{
		reflection.enabled = false;
		outline.enabled = true;
		//outline._OutLineSpread = 0.02f;
		print(gameObject.name);
	}
	void OnMouseExit()
	{
		outline.enabled = false;
		reflection.enabled = true;

	}

    
}