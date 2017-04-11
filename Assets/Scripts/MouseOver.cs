using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;




public class MouseOver : MonoBehaviour
{
	_2dxFX_AL_Outline outline;
	// Use this for initialization
	void Start()
	{
		outline = GameObject.Find("Start").GetComponent<_2dxFX_AL_Outline>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnMouseEnter()

	{
		outline.enabled=true;	
		//outline._OutLineSpread = 0.02f;
		print(gameObject.name);
	}
	void OnMouseExit()
	{
		outline.enabled = false;

	}

    
}