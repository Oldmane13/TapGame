using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public enum States {touch, touchHold, swipeLeft, swipeRight, doubleTap};
	public States currentState;
	public Text text;
	public Text timerText;
	public float theCountdown;
	public float stateTimer;
	//private int tapCounter;
	private float holdTime = 0.5f; //or whatever
	private float acumTime = 0;

	private int TapCount;
	public float MaxDubbleTapTime;
	private float NewTime;
	public GameObject tapAnimation;


	public SwipeManager swipeManager;



	void Start () {
		swipeManager = FindObjectOfType <SwipeManager> ();
		currentState = States.swipeLeft;
		TapCount = 0;


	}


	void Update () {

		theCountdown -= Time.deltaTime;
		stateTimer -= Time.deltaTime;
		timerText.text = " " + Mathf.Round(theCountdown);

		if (currentState == States.swipeLeft) {
			text.text = "Swipe Left";
			if (theCountdown <= 0) {
				text.text = "Game Over";
				Time.timeScale = 0;
			}
			if (swipeManager.IsSwiping (SwipeDirection.Left)) {
				Debug.Log ("Left");
				theCountdown = 5;
				//currentState = States.swipeRight;
				currentState = (States)Random.Range (0, 5);
			}
		}

		if (currentState == States.swipeRight){
			text.text = "Swipe Right";
			if (theCountdown <= 0) {
				text.text = "Game Over";
				Time.timeScale = 0;
			}
			if (swipeManager.IsSwiping (SwipeDirection.Right)) {
				Debug.Log ("Right");
				theCountdown = 5;
				currentState = (States) Random.Range(0,5);
			}
		}

		if (currentState == States.touch) {
			text.text = "Tap once";
			if (theCountdown <= 0) {
				text.text = "Game Over";
				Time.timeScale = 0;
			}
			if (Input.GetMouseButtonDown (0)) {
				Tap ();
				Debug.Log ("touch");
				theCountdown = 5;
				currentState = (States) Random.Range(0,5);
			}
		}

		if (currentState == States.doubleTap) {
			text.text = "Tap x2";
			if (theCountdown <= 0) {
				text.text = "Game Over";
				Time.timeScale = 0;
			}

			if (Input.touchCount == 1) {
				Touch touch = Input.GetTouch (0);

				if (touch.phase == TouchPhase.Ended) {
					TapCount += 1;
					Tap ();
				}

				if (TapCount == 1) {

					NewTime = Time.time + MaxDubbleTapTime;
				}else if(TapCount == 2 && Time.time <= NewTime){

					//Whatever you want after a dubble tap    
					print ("Dubble tap");
					TapCount = 0;
					theCountdown = 5;
					currentState = (States)Random.Range (0, 5);
				}

			}
			if (Time.time > NewTime) {
				TapCount = 0;
			}
		}
		if (currentState == States.touchHold) {
		
			text.text = "Tap and hold";
			if (theCountdown <= 0) {
			
				text.text = "Game Over";
				Time.timeScale = 0;
			}
			if (acumTime >= holdTime) {
				text.text = "Release!";
			}
			if(Input.touchCount > 0)
			{
				acumTime += Input.GetTouch(0).deltaTime;

				if(acumTime >= holdTime)
				{
					text.text = "Release!";
					currentState = (States)Random.Range (0, 5);
					theCountdown = 5;
				
				
				}

				if(Input.GetTouch(0).phase == TouchPhase.Ended) 
				{
					acumTime = 0f;
//					theCountdown = 5;
//					currentState = (States)Random.Range (0, 5);
				}
			}
		}
	}

	IEnumerator Ded(){

		yield return new WaitForSeconds (0.2f);

	
	}

	void Tap(){
		
		Instantiate (tapAnimation, Input.mousePosition, transform.rotation);
		StartCoroutine ("Ded");
		gameObject.GetComponent<Renderer> ().enabled = false;
	}
}
