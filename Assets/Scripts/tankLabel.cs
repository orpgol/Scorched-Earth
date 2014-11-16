using UnityEngine;
using System.Collections;

public class tankLabel : MonoBehaviour 
{
	public GUIStyle style;
	public int power = 0;
	public int angle = 0;
	public int health = 0;

	void start()
	{
		
	}
	
	void OnGUI() 
	{
		power = transform.parent.gameObject.GetComponent<SC_MoveTank>().shotPower;
		angle = transform.parent.gameObject.GetComponent<SC_MoveTank>().shotAngle;
		health = 100 - transform.parent.gameObject.GetComponent<SC_MoveTank>().damageTaken;
		
		//do it yourself...
		//style.normal.textColor = Color.red;
		//GUI.Label(new Rect(200,140,100,30),"$" + hSliderValue, style);
		//label.text = hSliderValue.ToString();
		
		GetComponent<UILabel>().text = ("A-" + angle.ToString() + "\n" + "P-" + power.ToString() + "\n" + "Health-" + health);
	}
	
	void update()
	{
		
		
	}
}

