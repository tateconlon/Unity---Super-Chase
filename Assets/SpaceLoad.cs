﻿using UnityEngine;
using System.Collections;

public class SpaceLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space))
			Application.LoadLevel("menu");
	}
}
