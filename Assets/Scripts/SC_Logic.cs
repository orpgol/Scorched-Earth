using UnityEngine;
using System.Collections;
using System;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.command;
using System.Collections.Generic;
//using MiniJSON;
using Facebook.MiniJSON;

public class SC_Logic : MonoBehaviour 
{

	private string apiKey = "7e1ee20126b82fbaef642558edc985253f05d2cf2b9e78eeff34af93d0474f1f";
	private string secretKey = "a88efe062bb3a10cb6f1891123435015bf534e47f5ab8b7d452cb0b5be8ba669";
	private string email = "@gmail.com";
	private string userName = "";
	private UILabel guiText;
	private string roomId = "";
	private List<string> rooms;
	private string opponentName = "";
	private GameObject fbObj;
	private SC_App42Kit sc_App42Kit;
	private SC_AppWarpKit sc_AppWarpKit;
	private object wager;
	private Dictionary<string, object> credit;
	private int seq_num = 0;
	public bool isOwner = false;
	public bool isGameStarted = false;
	public bool isMyTurn = false;
	private GameObject menu;
	private GameObject game;


	void OnEnable()
	{
			SC_Listener_App42.onCreatedUserApp42 += onCreatedUserApp42;
			SC_Listener_App42.OnExceptionFromApp42 += OnExceptionFromApp42;
		
			SC_Listener_AppWarp.onConnectToAppWarp += onConnectToAppWarp;
			SC_Listener_AppWarp.onDisconnectFromAppWarp += onDisconnectFromAppWarp;
			SC_Listener_AppWarp.OnMatchedRooms += OnGetMatchedRoomsDone;
			SC_Listener_AppWarp.OnSubscribeToRoom += onSubscribeToRoom;
			SC_Listener_AppWarp.OnUnSubscribeToRoom += onUnSubscribeToRoom;
			SC_Listener_AppWarp.OnJoinToRoom += OnJoinToRoom;
			SC_Listener_AppWarp.OnLeaveFromRoom += OnLeaveFromRoom;
			SC_Listener_AppWarp.OnCreateRoomDone += OnCreateRoomDone;
			SC_Listener_AppWarp.onGetLiveRoomInfo += OnGetLiveRoomInfo;
			SC_Listener_AppWarp.OnSendPrivateUpdate += OnSendPrivateUpdate;
			SC_Listener_AppWarp.OnSendPrivateChat += OnSendPrivateChat;
			SC_Listener_AppWarp.OnStartGameDone += OnStartGameDone;
			SC_Listener_AppWarp.OnStopGameDone += OnStopGameDone;
			SC_Listener_AppWarp.OnRoomCreated += OnRoomCreated;
			SC_Listener_AppWarp.OnUserJoinRoom += OnUserJoinRoom;
			SC_Listener_AppWarp.OnUserLeftRoom += OnUserLeftRoom;
			SC_Listener_AppWarp.OnPrivateUpdateReceived += OnPrivateUpdateReceived;
			SC_Listener_AppWarp.OnPrivateChatReceived += OnPrivateChatReceived;
			SC_Listener_AppWarp.OnGameStarted += OnGameStarted;
			SC_Listener_AppWarp.OnGameStopped += OnGameStopped;
			SC_Listener_AppWarp.OnSendMove += OnSendMove;
			SC_Listener_AppWarp.OnMoveCompleted += OnMoveCompleted;
	}
		
	void OnDisable()
	{
		SC_Listener_App42.onCreatedUserApp42 -= onCreatedUserApp42;
		SC_Listener_App42.OnExceptionFromApp42 -= OnExceptionFromApp42;
		
		SC_Listener_App42.OnExceptionFromApp42 -= OnExceptionFromApp42;
		SC_Listener_AppWarp.onConnectToAppWarp -= onConnectToAppWarp;
		SC_Listener_AppWarp.onDisconnectFromAppWarp -= onDisconnectFromAppWarp;
		SC_Listener_AppWarp.OnMatchedRooms -= OnGetMatchedRoomsDone;
		SC_Listener_AppWarp.OnSubscribeToRoom -= onSubscribeToRoom;
		SC_Listener_AppWarp.OnUnSubscribeToRoom -= onUnSubscribeToRoom;
		SC_Listener_AppWarp.OnJoinToRoom -= OnJoinToRoom;
		SC_Listener_AppWarp.OnLeaveFromRoom -= OnLeaveFromRoom;
		SC_Listener_AppWarp.OnCreateRoomDone -= OnCreateRoomDone;
		SC_Listener_AppWarp.onGetLiveRoomInfo -= OnGetLiveRoomInfo;
		SC_Listener_AppWarp.OnSendPrivateUpdate -= OnSendPrivateUpdate;
		SC_Listener_AppWarp.OnSendPrivateChat -= OnSendPrivateChat;
		SC_Listener_AppWarp.OnStartGameDone -= OnStartGameDone;
		SC_Listener_AppWarp.OnStopGameDone -= OnStopGameDone;
		SC_Listener_AppWarp.OnRoomCreated -= OnRoomCreated;
		SC_Listener_AppWarp.OnUserJoinRoom -= OnUserJoinRoom;
		SC_Listener_AppWarp.OnUserLeftRoom -= OnUserLeftRoom;
		SC_Listener_AppWarp.OnPrivateUpdateReceived -= OnPrivateUpdateReceived;
		SC_Listener_AppWarp.OnPrivateChatReceived -= OnPrivateChatReceived;
		SC_Listener_AppWarp.OnGameStarted -= OnGameStarted;
		SC_Listener_AppWarp.OnGameStopped -= OnGameStopped;
		SC_Listener_AppWarp.OnSendMove -= OnSendMove;
		SC_Listener_AppWarp.OnMoveCompleted -= OnMoveCompleted;

	}

	void Awake() 
	{
		//DontDestroyOnLoad(transform.gameObject);
		menu = GameObject.Find("Menu");
		game = GameObject.Find("Game");
		game.SetActive(false);
		menu.SetActive(true);

	}
	
	void Start () 
	{
		guiText = GameObject.Find ("GUIText").GetComponent<UILabel> ();
		SC_App42Kit.App42Init(apiKey,secretKey);
		SC_AppWarpKit.WarpInit(apiKey,secretKey);
		sc_AppWarpKit = new SC_AppWarpKit ();
		fbObj = GameObject.Find("facebook");
	}
	
	void Update () 
	{
		//Init user once so we will have a user on App42
		if(seq_num == 1)
		{
			userName = fbObj.GetComponent<SC_Facebook>().fbUserName;
			Debug.Log("Creating User..." + userName);
			guiText.text = "Creating User..."+ System.Environment.NewLine;

			//if not connected thru facebook, play as a guest
			if (userName == "")
				userName = "Guest" + ((int)(Time.time * 100000)).ToString();

			SC_App42Kit.InitUser(userName,"1234",((int)(Time.time * 100000)).ToString() + email);

			seq_num = 2;
		}
		
		if(seq_num == 3)
		{
			Debug.Log("Connecting To Server...");
			guiText.text = "Connecting To Server..."+ System.Environment.NewLine;
			sc_AppWarpKit.connectToAppWarp(userName);

			seq_num = 4;
		}

		if(seq_num == 7)
		{
			Debug.Log("Room Created !!");
			guiText.text = "Room Created"+ System.Environment.NewLine;

			sc_AppWarpKit.CreateTurnBaseRoom("Stam1",userName,2,credit,30);

			seq_num = 8;
		}

		if(seq_num == 5)
		{
			Debug.Log("Get Rooms !!");
			guiText.text = "Get Rooms"+ System.Environment.NewLine;
			sc_AppWarpKit.GetRoomWithProperties(credit);

			seq_num = 6;
		}
		if(Input.GetKey(KeyCode.W))
		{
			Debug.Log("Disconnecting from Server...");
			guiText.text = "Disconnecting from Server..."+ System.Environment.NewLine;
			sc_AppWarpKit.DisconnectFromAppWarp();
		}

	}

	
	public void onCreatedUserApp42(object respond)
	{
		Debug.Log(respond );
		guiText.text = "User Created..."+ System.Environment.NewLine;
		seq_num = 3;
	}
	
	public void OnExceptionFromApp42(Exception error)
	{
		Debug.Log("onConnectToApp42: " + error.Message);
		guiText.text = error.Message + System.Environment.NewLine;

		seq_num = 3;
	}
	
	public void onConnectToAppWarp(ConnectEvent eventObj)
	{
		Debug.Log("onConnectToAppWarp " + eventObj.getResult());
		guiText.text = "Connected To AppWrap" + System.Environment.NewLine;

		seq_num = 5;
	}
	
	public void onDisconnectFromAppWarp(ConnectEvent eventObj)
	{
		Debug.Log("onDisconnectFromAppWarp " + eventObj.getResult());
		guiText.text = "Disconnected from AppWrap" + System.Environment.NewLine;
	}
	
	public void OnCreateRoomDone(RoomEvent eventObj)
	{
		Debug.Log("OnCreateRoomDone " + eventObj.getResult() + " room Owner " + eventObj.getData().getRoomOwner());
		if(eventObj.getResult() == 	WarpResponseResultCode.SUCCESS)
		{
			roomId = eventObj.getData ().getId ();
			guiText.text = "Room created! " +  eventObj.getData ().getId () + System.Environment.NewLine;
			sc_AppWarpKit.JoinToRoom(eventObj.getData().getId());

			seq_num = 9;
			isOwner = true;
		}
	}
	
	//only room creator will get the notification
	public void OnDeleteRoomDone(RoomEvent eventObj)
	{
		Debug.Log("OnDeleteRoomDone " + eventObj.getResult());
	}
	
	public void onSubscribeToRoom(RoomEvent eventObj)
	{
		Debug.Log("onSubscribeToRoom " + eventObj.getResult());
		guiText.text = "SubscribeToRoom ! " +  eventObj.getData ().getId () + System.Environment.NewLine;
		//if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
		//	Debug.Log("onSubscribeRoomDone : " + eventObj.getResult());
	}
	
	public void onUnSubscribeToRoom(RoomEvent eventObj)
	{
		Debug.Log("onUnSubscribeToRoom " + eventObj.getResult());
	}
	
	public void OnJoinToRoom(RoomEvent eventObj)
	{
		if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
		{
			Debug.Log("OnJoinToRoom " + eventObj.getResult());
			opponentName = eventObj.getData().getRoomOwner();
			Debug.Log("roomId: " + roomId + ", OpponentName: " + opponentName);
			guiText.text = "Joined Room! " +  eventObj.getData ().getId () + System.Environment.NewLine;
			sc_AppWarpKit.RegisterToRoom(roomId);
		}
		else
		{
			sc_AppWarpKit.JoinToRoom(roomId);
		}
	}
	public void OnLeaveFromRoom(RoomEvent eventObj)
	{
		Debug.Log("OnLeaveFromRoom " + eventObj.getResult());
	}
	
	public void OnGetLiveRoomInfo(LiveRoomInfoEvent eventObj)
	{
		// Debug.Log("OnGetLiveRoomInfo " + eventObj.getResult() + " " + eventObj.getData().getId() + " " + eventObj.getJoinedUsers().Length);
	}
	
	public void OnSendPrivateUpdate(byte result)
	{
	}
	
	public void OnSendPrivateChat(byte result)
	{
		Debug.Log("onSendPrivateChatDone : " + result);

	}
	
	//onky room creator will get that
	public void OnStartGameDone(byte result)
	{
		Debug.Log("OnStartGameDone : " + result);
	}

	public void OnStopGameDone(byte result)
	{
		Debug.Log("OnStopGameDone : " + result);
	}
	
	public void OnGetMatchedRoomsDone(MatchedRoomsEvent eventObj)
	{
		Debug.Log("OnGetMatchedRoomsDone : " + eventObj.getResult());
		rooms = new List<string>();
		foreach (var roomData in eventObj.getRoomsData())
		{
		    Debug.Log("Room ID:" + roomData.getId() + ", " + roomData.getRoomOwner());
			guiText.text = "Room ID:" + roomData.getId() + ", " + roomData.getRoomOwner() + System.Environment.NewLine;
			rooms.Add(roomData.getId()); // add to the list of rooms id
		}
		
		Debug.Log("Rooms Amount: " + rooms.Count);
		if(rooms.Count > 0)
		{
			roomId = rooms[0];
			sc_AppWarpKit.JoinToRoom (rooms[0]);
		}
		else
			seq_num = 7;
	}
	
	public void OnRoomCreated(RoomData eventObj)
	{
		Debug.Log("OnRoomCreated : " + eventObj.getName());
	}
	
	//both player get the notification
	public void OnUserLeftRoom(RoomData eventObj, string nameOfUser)
	{
		Debug.Log("OnUserLeftRoom : " + eventObj.getName());
	}
	
	//only host recieve it
	public void OnUserJoinRoom(RoomData eventObj, string userName)
	{
		Debug.Log("OnUserJoinRoom" + " " + eventObj.getRoomOwner() + " User connected" + userName);
		opponentName = userName;
		sc_AppWarpKit.StartGame();
	}
	
	public void OnPrivateUpdateReceived(string sender, byte[] eventObj, bool fromUdp)
	{
		//Debug.Log("OnPrivateUpdateReceived" + " " + messageReceived);
	}
	
	public void OnPrivateChatReceived(string sender, string message)
	{
//		Debug.Log("OnPrivateChatReceived (" + sender + ") " + message + " " + sc_GuiManager.currentUser.CurrentUserState + " " + haveStart + " " + haveStartApproved);
	
		if(sender != userName && isGameStarted == false)
		{
			Debug.Log ("Message Recived, (" + sender + ") " + message);
			guiText.text = "Message Recived, (" + sender + ") " + message;

			GameObject.Find("GameLogic").GetComponent<SC_GameLogic>().setEnemyLocation(message);
			isGameStarted = true;
		}
		else
			GameObject.Find("GameLogic").GetComponent<SC_GameLogic>().setWind(message);
	}
	
	public void OnGameStarted(string sender, string roomId, string nextTurn)
	{
//		Debug.Log("OnGameStarted" + " " + sender + " " + roomId + " " + nextTurn + " " + sc_GuiManager.currentUser.CurrentUserState);
		isMyTurn = true;
		//Application.LoadLevel ("Game_Scene");

		menu.SetActive(false);
		game.SetActive(true);

	}
	
	public void OnGameStopped(string sender, string roomId)
	{
		Debug.Log("OnGameStopped" + " " + sender + " " + roomId);
	}

	public void OnSendMove(byte result)
	{
		Debug.Log("OnSendMove : " + result);
	}

	public void OnMoveCompleted(MoveEvent move)
	{     
		Debug.Log("OnMoveCompleted" + " " + move.getNextTurn() + " " + move.getMoveData());
		if (move.getNextTurn () == userName)
			isMyTurn = true;
		else isMyTurn = false;

		Debug.Log("Is my turn: " + isMyTurn);

		if(move.getNextTurn() == userName && move.getMoveData() != "")
		{
			GameObject.Find("GameLogic").GetComponent<SC_GameLogic>().doEnemyTurn(move.getMoveData());
		}
	}

	public void startConnection()
	{
		int money = GameObject.Find("Label1").GetComponent<Slider>().hSliderValue;
		Debug.Log ("Started: " + money);
		credit = new Dictionary<string, object>()
		{
			{"wager", money}
		};

		seq_num = 1;

	}

	public void privateMessage(string turnDetails)
	{
		Debug.Log("Sent Message: " + turnDetails + " To: " + opponentName);
		guiText.text = "Sent Message " + userName + " " + Time.time + System.Environment.NewLine;
		
		sc_AppWarpKit.sendPrivateChat(opponentName, turnDetails);
	}
	
	public void moveTurn(string turnDetails)
	{
		Debug.Log("Move turn: " + turnDetails + " To: " + opponentName);
		guiText.text = "Move turn " + System.Environment.NewLine;
		
		sc_AppWarpKit.sendMove(turnDetails);
	}

	public void disconnectFromAppWarp()
	{
		Debug.Log("Disconnecting from Server...");
		guiText.text = "Disconnecting from Server..."+ System.Environment.NewLine;
		sc_AppWarpKit.DisconnectFromAppWarp();
	}

	public void reset()
	{
		game.SetActive(false);
		menu.SetActive(true);
	}
	
}
