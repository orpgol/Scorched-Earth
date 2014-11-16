using UnityEngine;
using System.Collections;

public class maneger : MonoBehaviour 
{
	private GameObject first;
	private GameObject second;
	private GameObject third;
	private GameObject loading;
	
	// Use this for initialization
	void Awake () 
	{
		first = GameObject.Find("First");
		second = GameObject.Find("Second");
		third = GameObject.Find("Third");
		loading = GameObject.Find("Loading");
	}

	void Start()
	{
		second.SetActive(false);
		third.SetActive(false);
		loading.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void goScreenOne()
	{
		first.SetActive(true);
		second.SetActive(false);
		third.SetActive(false);
	}
	public void goScreenTwo()
	{
		first.SetActive(false);
		second.SetActive(true);
		third.SetActive(false);
	}
	public void goScreenThree()
	{
		first.SetActive(false);
		second.SetActive(false);
		third.SetActive(true);
	}
	public void goLoading()
	{
		third.SetActive(false);
		loading.SetActive(true);
	}
}
