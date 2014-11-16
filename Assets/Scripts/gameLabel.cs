using UnityEngine;
using System.Collections;

public class gameLabel : MonoBehaviour 
{
	public GUIStyle style;

	void start()
	{
		
	}
	
	void OnGUI()
	{
		
		//do it yourself...
		//style.normal.textColor = Color.red;
		//GUI.Label(new Rect(200,140,100,30),"$" + hSliderValue, style);
		//label.text = hSliderValue.ToString();
		
		GetComponent<UILabel>().text = "My turn? " + transform.parent.gameObject.GetComponent<SC_GameLogic>().isMyTurn;
	}
	
	void update()
	{
		
		
	}
}
