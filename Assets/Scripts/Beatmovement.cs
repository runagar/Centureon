using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatmovement : MonoBehaviour
{
	bool check = false;
	private AudioSource audio;

	void Awake()
	{
		StartCoroutine("ToggleRenderer");
		audio = GetComponent<AudioSource>();

	}

	void Update()
	{
		if (check == true)
		{
			check = false;
			StartCoroutine("ToggleRenderer");
		}
	}
	IEnumerator ToggleRenderer()
	{
		yield return new WaitForSeconds(0.66f); 
		this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("img1");
		//playaudio();
	/*	yield return new WaitForSeconds(0.6f);
		this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("img2");
		playaudio();
		yield return new WaitForSeconds(0.6f);
		this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("img1");
		playaudio();*/
		yield return new WaitForSeconds(0.66f);
		this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("img2");
		//playaudio();
		check = true;
	}
	void playaudio() { 
			audio.Play();
	}

}