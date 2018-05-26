using UnityEngine;
using System.Collections;

public class CameraAnimationManager : MonoBehaviour {

	[SerializeField]
	Animator cameraAnimator;

	[SerializeField]
	Animator gameAnimator;
	
	PrepStateBehavior prepStateBehavior;
	DrawStateBehavior drawStateBehavior;

	// Use this for initialization
	void Start () {
		prepStateBehavior = gameAnimator.GetBehaviour<PrepStateBehavior>();
		prepStateBehavior.Initialize(cameraAnimator);

		drawStateBehavior = gameAnimator.GetBehaviour<DrawStateBehavior>();
		drawStateBehavior.Initialize(cameraAnimator);

		gameObject.transform.position = new Vector3(5, 1, 0);
		gameObject.transform.LookAt(Vector3.up);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
