using UnityEngine;
using System.Collections;

public class WindLabel : MonoBehaviour 
{
	public GUIStyle style;
	private SC_GameLogic sc_GameLogic;
	
	void start()
	{
	
	}
	
	void OnGUI()
	{
		if (sc_GameLogic == null)
			sc_GameLogic = GameObject.Find("GameLogic").GetComponent<SC_GameLogic> ();

		if (sc_GameLogic.windFactor > 0)
			GetComponent<UILabel>().text = "Wind -> " + sc_GameLogic.windFactor;
		else if (sc_GameLogic.windFactor < 0)
			GetComponent<UILabel>().text = "Wind <- " + sc_GameLogic.windFactor * -1;
		else
			GetComponent<UILabel>().text = "Wind = " + sc_GameLogic.windFactor;
	}
	
	void update()
	{
		
		
	}
}
