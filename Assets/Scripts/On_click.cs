using UnityEngine;
using System.Collections;

public class On_click : MonoBehaviour 
{
	static int current = 1;

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
		Debug.Log("Pressed: " + this.gameObject);

		if (this.name == "guest" || this.name == "facebook")
		{
			current = 2;
			if (this.name == "facebook")
			{	
				// check if logged in
				//FB.IsLoggedIn();

				// Log in to facebook and Get the user details
				FB.Login("email", GetComponent<SC_Facebook>().AuthCallback);
				// move a screen only after FB is done...
				GetComponent<SC_Facebook>().WaitForCallback();
			}
			else
				GameObject.Find("Anchor").GetComponent<maneger>().goScreenTwo();
		
		}
		else if (this.name == "newGame")
		{
			current = 3;
			GameObject.Find("Anchor").GetComponent<maneger>().goScreenThree();	
		}
		else if (this.name == "playNow")
		{
			current = 0;
			GameObject.Find("MasterLogic").GetComponent<SC_Logic>().startConnection();
			GameObject.Find("Anchor").GetComponent<maneger>().goLoading();
		}

		else if (this.name == "back")
		{
			Debug.Log("inside back func- curren: " + current);
			if (current == 2)
			{
				current--;
				GameObject.Find("Anchor").GetComponent<maneger>().goScreenOne();
			}
			if (current == 3)
			{
				current--;
				GameObject.Find("Anchor").GetComponent<maneger>().goScreenTwo();
			}
		}

	}
	
}
