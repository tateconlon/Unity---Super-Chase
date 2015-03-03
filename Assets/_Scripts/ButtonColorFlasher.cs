using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonColorFlasher : MonoBehaviour {

	Button b;
	Image i;
	
	public Color[] colors;
	public float cycleTime;

	public Color[] clickColors;
	public float clickCycleTime;


	public bool isClicked;

	float time;
	int currColorIndex;
	int currClickColorIndex;

	// Use this for initialization
	void Start () {
		isClicked = false;
		b = GetComponent<Button>();
		i = GetComponent<Image>();
		time = cycleTime;
		currColorIndex = -1;
		currClickColorIndex = -1;
		if(isClicked)
			CycleClickColours();
		else
			CycleColours();
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if(time < 0f){
			if(!isClicked) {
				time = cycleTime;
				CycleColours();
			} else {
				time = clickCycleTime;
				CycleClickColours();
			}
		}
	}
	
	void CycleColours() {
		currColorIndex++;
		currColorIndex = currColorIndex % colors.Length;
		ColorBlock temp = b.colors;
		temp.normalColor = new Color(colors[currColorIndex].r, colors[currColorIndex].g, colors[currColorIndex].b, colors[currColorIndex].a);
		i.color = new Color(colors[currColorIndex].r, colors[currColorIndex].g, colors[currColorIndex].b, colors[currColorIndex].a);
		b.colors = temp;
	}

	void CycleClickColours() {
		currClickColorIndex++;
		currClickColorIndex = currClickColorIndex % clickColors.Length;
		ColorBlock temp = b.colors;
		i.color = new Color(clickColors[currClickColorIndex].r, clickColors[currClickColorIndex].g, clickColors[currClickColorIndex].b, clickColors[currClickColorIndex].a);
		temp.normalColor = new Color(clickColors[currClickColorIndex].r, clickColors[currClickColorIndex].g, clickColors[currClickColorIndex].b, clickColors[currClickColorIndex].a);
		temp.highlightedColor = new Color(clickColors[currClickColorIndex].r, clickColors[currClickColorIndex].g, clickColors[currClickColorIndex].b, clickColors[currClickColorIndex].a);
		temp.pressedColor = new Color(clickColors[currClickColorIndex].r, clickColors[currClickColorIndex].g, clickColors[currClickColorIndex].b, clickColors[currClickColorIndex].a);
		b.colors = temp;
	}

	public void Clicked() {
		if(!isClicked) {
			CycleClickColours();
			time = clickCycleTime;
			isClicked = true;
		}
	}

	public void UnClicked() {
		if(isClicked) {
			CycleClickColours();
			time = clickCycleTime;
			isClicked = false;
		}
	}
}
