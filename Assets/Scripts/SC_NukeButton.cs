using UnityEngine;
using System.Collections;

public class SC_NukeButton : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		Debug.Log("Pressed: " + this.gameObject);
		this.gameObject.SetActive(false);

		if(GameObject.Find("MasterLogic").GetComponent<SC_Logic>().isOwner)
			GameObject.Find("Tank1").GetComponent<SC_MoveTank>().setNextShotIsNuke();
		else
			GameObject.Find("Tank2").GetComponent<SC_MoveTank>().setNextShotIsNuke();
	}	
}
