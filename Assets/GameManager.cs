using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum States {touch, touchHold, swipeLeft, swipeRight, doubleTap};
	public States currentState;

	public SwipeManager swipeManager;



	void Start () {
		swipeManager = FindObjectOfType <SwipeManager> ();
		currentState = States.swipeLeft;
	}
	

	void Update () {

		if (currentState == States.swipeLeft) {

			if(swipeManager.IsSwiping (SwipeDirection.Left)) {
				Debug.Log ("Left");
				currentState = States.swipeRight;
			} else {
				Debug.Log ("no");
			}
		}

		if (currentState == States.swipeRight){
			if (swipeManager.IsSwiping (SwipeDirection.Right)) {
				Debug.Log ("Right");
				currentState = States.touch;
			} else {
				Debug.Log ("no");
			}
		}

		if (currentState == States.touch) {
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log ("touch");
				currentState = States.touchHold;
			}
		}


	}
}
