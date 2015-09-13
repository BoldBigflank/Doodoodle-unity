﻿using UnityEngine;
using System.Collections;
using SocketIO;

//public struct STATE {
//	int value;
//	string name;
//	string title;
//}
//
//public struct Player {
//	int id;
//	string name;
//	string state;
//	int position;
//	int score;
//	string room;
//}
//
//public struct Drawing {
//	int playerId;
//	int[] seed;
//	int position;
//	int votingRound;
//	int[] lines;
//	int[] votes;
//}
//
//public struct GameData {
//	int id;
//	uint timer;
//	Player[] players;
//	STATE state;
//	int votingRound;
//	string room;
//	int host;
//	Drawing[] round;
//}

public class GameManagerScript : MonoBehaviour {
	[SerializeField]
	GameObject dino;
	
	JSONObject gameData;
	SocketIOComponent socket;

	// Use this for initialization
	void Start () {
		socket = gameObject.GetComponent<SocketIOComponent>();
		
		socket.On("game", HandleGame);
		socket.On("event", HandleEvent);
		socket.On("alert", HandleAlert);

		// Tell the server we're ready to host
		StartCoroutine("HostGame");
	}
	
	// Update is called once per frame
	void Update () {

	}

	private IEnumerator HostGame () {
		while(!socket.IsConnected || socket.IsInvoking()){
			Debug.Log ("Waiting...");
			yield return new WaitForSeconds(1);
		}
		socket.Emit("host", HostCallback);
	}

	public void HostCallback(JSONObject o) {
		JSONObject gameJSON = (o.IsArray) ? o[0] : o;
		Debug.Log (gameJSON);
		gameData = gameJSON;
	}

	public void HandleGame(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] " + e.name + " received: " + e.data);
		gameData = e.data;
		
		string state = e.data.GetField("state").GetField("name").str; // The game's state
		Debug.Log ("Sending trigger " + state);
		GetComponent<Animator>().SetTrigger(state);
	}

	public void HandleEvent(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] " + e.name + " received: " + e.data);
		// TODO: Send a trigger to the animator controller
		// Incoming events: join, start, drawing, vote
		
		if(e.data.GetField ("event").str == "joined"){
			Debug.Log("Getting a dino ready");
			// Create a dino, put it on the screen
			GameObject o = (GameObject)Instantiate(dino);
			o.transform.position = 3.0F * Random.insideUnitSphere;
		}
		// join: play a sound
	}

	public void HandleAlert(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] " + e.name + " received: " + e.data);
		
	}
}
