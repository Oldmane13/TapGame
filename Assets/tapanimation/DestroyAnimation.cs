﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour {

	public float waitBeforeGone;

	void Start(){
		StartCoroutine ("Wait");

	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (waitBeforeGone);
		Destroy (gameObject);
	}
}
