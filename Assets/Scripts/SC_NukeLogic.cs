using UnityEngine;
using System.Collections;

public class SC_NukeLogic : MonoBehaviour 
{
	public GameObject explotion;
	private SC_GameLogic sc_GameLogic;
	
	// Use this for initialization
	void Start () 
	{
		if (sc_GameLogic == null)
			sc_GameLogic = GameObject.Find("GameLogic").GetComponent<SC_GameLogic> ();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//wind!!!
		this.GetComponentInParent<Rigidbody>().AddForce(sc_GameLogic.windFactor ,0,0);
	}
	
	void OnTriggerEnter(Collider other) 
	{
		Debug.Log(other.gameObject.tag);
		StartCoroutine(explode(7));
		if (other.gameObject.tag == "Player")
			other.GetComponent<SC_MoveTank>().takeDamage(60);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);
		StartCoroutine(explode(7));
	}
	
	public IEnumerator explode(float _timeToWait)
	{
		GameObject _explosion = (GameObject) Instantiate (explotion, this.transform.position, this.transform.rotation);
		Debug.Log(this.transform.position);
		yield return new WaitForSeconds(_timeToWait);
		Destroy(_explosion);
	}
	
}
