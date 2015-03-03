using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColourFlasher : MonoBehaviour {

	Text t;

	public Color[] colors;
	public float cycleTime;

	float time;
	int currColorIndex;
	// Use this for initialization
	void Start () {
		t = GetComponent<Text>();
		Restart();
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if(time < 0f) {
			time = cycleTime;
			CycleColours();
		}
	}

	public void CycleColours() {
		currColorIndex++;
		currColorIndex = currColorIndex % colors.Length;
		t.color = new Color(colors[currColorIndex].r, colors[currColorIndex].g, colors[currColorIndex].b);
	}

	public void Restart() {
		time = cycleTime;
		currColorIndex = 0;
		t.color = new Color(colors[0].r, colors[0].g, colors[0].b);
	}
}
