using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SC_GameLogic : MonoBehaviour
{
	[HideInInspector]
	public bool isMyTurn = false;
	public int windFactor;
	public GameObject endGraphic;
	private SC_Logic sc_logic;
	private Dictionary<string, float> turnDetails;
	private SC_MoveTank player;
	private SC_MoveTank enemy;
	public GameObject[] spawnPoints;


	void OnEnable()
	{
		sc_logic = GameObject.Find("MasterLogic").GetComponent<SC_Logic>();

		if (sc_logic.isOwner)
		{
			player = GameObject.Find("Tank1").GetComponent<SC_MoveTank>();
			enemy = GameObject.Find("Tank2").GetComponent<SC_MoveTank>();
		}
		else
		{
			player = GameObject.Find("Tank2").GetComponent<SC_MoveTank>();
			enemy = GameObject.Find("Tank1").GetComponent<SC_MoveTank>();
		}

		player.isPlayer = true;
		enemy.transform.FindChild("Label").transform.gameObject.SetActive(false);

	}

	void OnDisable()
	{
	}

	void Awake()
	{

	}

	void Start () 
	{
		if (sc_logic.isOwner)
		{
			player.rigidbody.AddForce (-Vector3.up * 100);
			spawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");
			player.transform.position = spawnPoints [Random.Range (0, spawnPoints.Length - 2)].transform.position;
		}
		else
		{
			player.rigidbody.AddForce (-Vector3.up * 100);
			spawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");
			player.transform.position = spawnPoints [Random.Range (2, spawnPoints.Length)].transform.position;
		}

		Debug.Log("Negotiate...");
		if (!sc_logic.isOwner)
			sc_logic.moveTurn("");

		Debug.Log(player.transform.position.ToString("G4"));
		sc_logic.privateMessage(player.transform.position.ToString("G4"));

	}
	
	void Update () 
	{
		isMyTurn = sc_logic.isMyTurn;
	}

	public void Init()
	{

	}

//	public void UpdateState(int _Index,State _StateToUpdate)
//	{
//		SC_Menu.sc_AppWarpKit.sendMove (_isWon.ToString());
//	}
	

	public IEnumerator OnMoveCompleted(float _timeToWait)
	{     
		windFactor = Random.Range(-100, 100);
		Debug.Log ("wind " + windFactor);
		sc_logic.privateMessage(windFactor.ToString());

		yield return new WaitForSeconds(_timeToWait);
	}

	public void gameOver()
	{
		Debug.Log("Game Over!");

		StartCoroutine(endGame(8));
	}

	public IEnumerator endGame(float _timeToWait)
	{
		yield return new WaitForSeconds(3);
		GameObject _end = (GameObject) Instantiate (endGraphic, this.transform.position, this.transform.rotation);
		yield return new WaitForSeconds(_timeToWait);
		Destroy(_end);
		Application.LoadLevel("menu");
	}

	public void doEnemyTurn(string data)
	{
		string[] values = data.Split(' ');
		int shotPower = int.Parse(values[0]);
		int shotAngle = int.Parse(values[1]);
		bool nextShotIsNuke = bool.Parse(values[2]);

		enemy.fireWeapon(shotPower, shotAngle, nextShotIsNuke);
	}

	public void setEnemyLocation(string data)
	{
		Vector3 vecData = getVector3(data);

		enemy.transform.position = vecData;
	}

	public Vector3 getVector3(string rString)
	{
		string[] temp = rString.Substring(1,rString.Length-2).Split(',');
		float x = float.Parse(temp[0]);
		float y = float.Parse(temp[1]);
		float z = float.Parse(temp[2]);
		Vector3 rValue = new Vector3(x,y,z);
		return rValue;
	}

	public void setWind(string eWind)
	{
		windFactor = int.Parse(eWind);
	}
	
}






















