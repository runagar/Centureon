using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOutline : MonoBehaviour {

	_2dxFX_AL_Outline outline;

	// Use this for initialization
	void Start () {
		outline = GameObject.Find("Start").GetComponent<_2dxFX_AL_Outline>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnMouseOver()
	{
		outline._OutLineSpread = 0.02f;
		print(outline);
	}
}
