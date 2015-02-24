using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	Text timeText;
	public float time = 300;

	// Use this for initialization
	void Start () {
		timeText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		timeText.text = ((int)time)/60 + ":" + ((int)time)%60;
	}
}
