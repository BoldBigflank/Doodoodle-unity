using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public enum AnimationID : int
	{
		Animation1 = 1,
		Animation2 = 2
	}

	public Animator animator;
	public string propertyName = "AnimationID";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			animator.SetInteger(propertyName, (int)AnimationID.Animation1);
		}

		if(Input.GetKeyDown(KeyCode.B))
		{
			animator.SetInteger(propertyName, (int)AnimationID.Animation2);
		}
	
	}
}
