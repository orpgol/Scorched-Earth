using UnityEngine;
using System.Collections;

public class SC_MoveTank : MonoBehaviour 
{
	public GameObject bullet;
	public GameObject nuke;
	public GameObject bulletPos;
	public GameObject shockWave;
	public GameObject death;
	public int shotPower;
	public int shotAngle;
	public int damageTaken;
	private SC_GameLogic sc_GameLogic;
	private SC_Logic sc_logic;
	private bool delayed = false;
	private bool nextShotIsNuke = false;
	public bool isPlayer = false;

	// Use this for initialization
	void Start () 
	{
		shotPower = 5000;
		shotAngle = 1000;
		damageTaken = 0;

		sc_logic = GameObject.Find("MasterLogic").GetComponent<SC_Logic>();

		sc_GameLogic = GameObject.Find("GameLogic").GetComponent<SC_GameLogic> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(sc_logic.isMyTurn && ! delayed && isPlayer)
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{	
				shotAngle += 100;
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				shotAngle -= 100;
			}
			if (Input.GetKey(KeyCode.RightArrow) && sc_logic.isOwner)
			{	
				shotPower += 100;
			}
			if (Input.GetKey(KeyCode.LeftArrow) && !sc_logic.isOwner)
			{	
				shotPower += 100;
			}
			if (Input.GetKey(KeyCode.LeftArrow) && sc_logic.isOwner)
			{
				shotPower -= 100;
			}
			if (Input.GetKey(KeyCode.RightArrow) && !sc_logic.isOwner)
			{
				shotPower -= 100;
			}
//			if (Input.GetKey(KeyCode.W))
//			{
//				if (this.transform.rotation.eulerAngles.y != 0)
//					this.transform.rotation = Quaternion.Euler(270, 0, 0);
//
//				this.transform.Translate(Vector3.right * Time.deltaTime);
//			}
//			if (Input.GetKey(KeyCode.Q))
//			{
//				if (this.transform.rotation.eulerAngles.y != 180)
//					this.transform.rotation = Quaternion.Euler(270, 180, 0);
//
//				this.transform.Translate(Vector3.right * Time.deltaTime);
//			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Debug.Log("fire!");

					if (nextShotIsNuke)
					{
						Debug.Log("NUKE!!!");
						//first way to instantiate
						GameObject clone = (GameObject) Instantiate(nuke, transform.position, transform.rotation);
						clone.transform.position = new Vector3(bulletPos.transform.position.x, bulletPos.transform.position.y, bulletPos.transform.position.z);
						if (this.transform.rotation.eulerAngles.y == 0)
							clone.rigidbody.AddForce(shotPower,shotAngle,0);
						else
							clone.rigidbody.AddForce(-shotPower,shotAngle,0);

						StartCoroutine(endTurn(10));
						
						StartCoroutine(shot(clone, 12));
						
						//second way to instantiate
						StartCoroutine(wave(1.5f));

						sc_logic.moveTurn(shotPower + " " + shotAngle + " " + nextShotIsNuke);

						nextShotIsNuke = false;
						
						delayed = true;
					}

					else
					{
						//first way to instantiate
						GameObject clone = (GameObject) Instantiate(bullet, transform.position, transform.rotation);
						clone.transform.position = new Vector3(bulletPos.transform.position.x, bulletPos.transform.position.y, bulletPos.transform.position.z);
						if (this.transform.rotation.eulerAngles.y == 0)
							clone.rigidbody.AddForce(shotPower,shotAngle,0);
						else
							clone.rigidbody.AddForce(-shotPower,shotAngle,0);

						StartCoroutine(endTurn(3));
						
						StartCoroutine(shot(clone, 9));
						
						//second way to instantiate
						StartCoroutine(wave(1.5f));
						
						delayed = true;

						sc_logic.moveTurn(shotPower + " " + shotAngle + " " + nextShotIsNuke);
				}

			}

		}
		
	}

	public IEnumerator shot(GameObject obj, float _timeToWait)
	{
		yield return new WaitForSeconds(_timeToWait);
		Destroy(obj);
	}

	public IEnumerator wave(float _timeToWait)
	{
		GameObject _wave = (GameObject) Instantiate (shockWave, bulletPos.transform.position, bulletPos.transform.rotation);
		Debug.Log(bulletPos.transform.position);
		yield return new WaitForSeconds(_timeToWait);
		Destroy(_wave);
	}

	public IEnumerator destruct(float _timeToWait)
	{
		GameObject _des = (GameObject) Instantiate (death, this.transform.position, this.transform.rotation);
		yield return new WaitForSeconds(_timeToWait);
		Destroy(_des);
	}

	public IEnumerator endTurn(float _timeToWait)
	{
		StartCoroutine(sc_GameLogic.OnMoveCompleted(_timeToWait));
		yield return new WaitForSeconds(_timeToWait);
		delayed = false;
	}
	
	public void takeDamage(int amount)
	{
		Debug.Log(this.name + " Took damage: " + amount);
		damageTaken += amount;
		if(damageTaken >= 100)
		{
			StartCoroutine(destruct(3));
			Destroy(this.gameObject);
			sc_GameLogic.gameOver();
		}
	}

	public void setNextShotIsNuke()
	{
		nextShotIsNuke = true;
	}

	public void fireWeapon(int eShotPower, int eShotAngle, bool eIsNuke)
	{
		Debug.Log("fire!");

			if (eIsNuke)
			{
				Debug.Log("NUKE!!!");
				//first way to instantiate
				GameObject clone = (GameObject) Instantiate(nuke, transform.position, transform.rotation);
				clone.transform.position = new Vector3(bulletPos.transform.position.x, bulletPos.transform.position.y, bulletPos.transform.position.z);
				if (this.transform.rotation.eulerAngles.y == 0)
					clone.rigidbody.AddForce(eShotPower,eShotAngle,0);
				else
					clone.rigidbody.AddForce(-eShotPower,eShotAngle,0);
				
				StartCoroutine(shot(clone, 12));
				
				//second way to instantiate
				StartCoroutine(wave(1.5f));
				
			}
			
			else
			{
				//first way to instantiate
				GameObject clone = (GameObject) Instantiate(bullet, transform.position, transform.rotation);
				clone.transform.position = new Vector3(bulletPos.transform.position.x, bulletPos.transform.position.y, bulletPos.transform.position.z);
				if (this.transform.rotation.eulerAngles.y == 0)
					clone.rigidbody.AddForce(eShotPower,eShotAngle,0);
				else
					clone.rigidbody.AddForce(-eShotPower,eShotAngle,0);
				
				StartCoroutine(shot(clone, 9));
				
				//second way to instantiate
				StartCoroutine(wave(1.5f));
		
			}
	}
}
