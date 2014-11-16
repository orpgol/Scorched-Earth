using UnityEngine;
using System.Collections;

public class Audio_manager : MonoBehaviour 
{
	public AudioSource tankMove;
	public AudioSource tankShoot;
	public AudioSource tankTurret;
	public AudioSource explotion;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void playExplosion()
	{
		explotion.Play();
	}
	public void playMove()
	{
		tankMove.Play();
	}
}
