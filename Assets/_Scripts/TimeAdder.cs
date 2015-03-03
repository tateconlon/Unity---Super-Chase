using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeAdder : MonoBehaviour {

	Text t;
	public int time = 60;
	public int minTime = 15;
	public int maxTime = 180;
	// Use this for initialization
	void Start () {
		t = GetComponentInChildren<Text>();
		updateTimeText();
	}
	
	public void AddTime(int deltaT)
	{
		time += deltaT;
		time = Mathf.Clamp(time, minTime, maxTime);
		updateTimeText();
	}

	void updateTimeText()
	{
		GameControl.control.maxTime = time;
		t.text = "Time: " + time/60 + ":" + (time%60).ToString("d2");
	}
}
