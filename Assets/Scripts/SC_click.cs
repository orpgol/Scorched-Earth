using UnityEngine;
using System.Collections;

public class SC_click : MonoBehaviour 
{

	public AudioSource backGroundMusic;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnClick()
	{
		Debug.Log("sound pressed");
		backGroundMusic.Play();
	}
}
