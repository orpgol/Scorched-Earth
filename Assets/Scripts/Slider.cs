using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour 
{
	public GUIStyle style;
	public int hSliderValue = 0;

	void start()
	{

	}

	void OnGUI() 
	{
		hSliderValue =(int) (transform.parent.gameObject.GetComponent<UISlider>().sliderValue * 100);

		//do it yourself...
		//style.normal.textColor = Color.red;
		//GUI.Label(new Rect(200,140,100,30),"$" + hSliderValue, style);
		//label.text = hSliderValue.ToString();

		GetComponent<UILabel>().text = hSliderValue.ToString();
	}

	void update()
	{


	}
}
