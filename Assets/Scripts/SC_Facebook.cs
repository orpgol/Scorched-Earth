using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;
using System.Collections.Generic;

public class SC_Facebook : MonoBehaviour {

	public string fbUserName = "";
	public string fbEmail = "" ;
	private string profilePictureUrl = string.Empty;
	public bool canContinue = false;
	private GameObject FacebookPic;

	// Use this for initialization
	void Awake()
	{
		FacebookPic = GameObject.Find("FacebookPic");
	}

	void Start () 
	{
		FacebookPic.SetActive(false);
		FB.Init(setInit);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void setInit()
	{

	}

	void onHideUnity()
	{

	}

	public void AuthCallback(FBResult result)
	{
		if (FB.IsLoggedIn)
		{
			Debug.Log("Logged in to facebook");
			FB.API("me", Facebook.HttpMethod.GET, MeGraphCallBack);
			StartCoroutine(WaitForCallback());
		}
	}

	public void MeGraphCallBack(FBResult _res)
	{
		Debug.Log(_res.Text);

		var _dict = Json.Deserialize(_res.Text) as Dictionary<string, object>;
		fbUserName = _dict ["name"].ToString();
		//fbEmail = dict ["email"].ToString (); - this will work only when the app will be activated
		canContinue = true;
		
	}

	private void MyPictureGraphCallBack(FBResult _Res)
	{
		Debug.Log(_Res.Text);
		var _dict = Json.Deserialize(_Res.Text) as Dictionary<string, object>;
		
		object friendsH;
		if (_dict.TryGetValue("picture", out friendsH))
		{
			profilePictureUrl = (string)((Dictionary<string, object>)(((Dictionary<string, object>)friendsH)["data"]))["url"];
		}
		
		StartCoroutine(DownloadPicture(profilePictureUrl));
	}
	
	public IEnumerator DownloadPicture(string _Link)
	{
		WWW www = new WWW(_Link);
		yield return www;
		
		Texture2D textFb2 = new Texture2D(128, 128, TextureFormat.ARGB32, false);
		www.LoadImageIntoTexture (textFb2);
		GameObject.Find("Anchor").GetComponent<maneger>().goScreenTwo();
		FacebookPic.SetActive(true);
		FacebookPic.renderer.material.mainTexture = textFb2;
	}

	public IEnumerator WaitForCallback()
	{
		while (!canContinue)
		{
			//Debug.Log(canContinue);
			yield return null;
		}
		Debug.Log("end loop");
		FB.API("me?fields=picture.height(200).width(200)", Facebook.HttpMethod.GET, MyPictureGraphCallBack);
		StopCoroutine(WaitForCallback());
		yield return null;
	}
}
