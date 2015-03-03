using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	Text t;
	public int playerNum;
	// Use this for initialization
	void Start () {
		t = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerNum == 1)
			t.text = GameManager.gm.score1.ToString();
		else
			t.text = GameManager.gm.score2.ToString();
	}
}
