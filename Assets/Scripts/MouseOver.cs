using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;




public class MouseOver : MonoBehaviour
{
	Ray ray;
	RaycastHit hit;
	_2dxFX_AL_Outline outline;
	// Use this for initialization
	void Start()
	{

	

		outline= GetComponent<_2dxFX_AL_Outline>();
	}

	// Update is called once per frame
	void Update()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			outline._OutLineSpread = 0.01f;
			print(hit.collider.name);
		}
		else
			outline._OutLineSpread = 0.02f;
	
	}

	void OnMouseEnter()

	{
		
		print(gameObject.name);

	}

    
}