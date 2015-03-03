using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ToggleButtonColourChanger : MonoBehaviour {

	Toggle t;


	public Color selectedColour;
	public Color offColour;
	// Use this for initialization
	void Start () {
		t = GetComponent<Toggle>();
		ChangeColour();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeColour() {
		ColorBlock temp = t.colors;
		if(t.isOn) {
			temp.normalColor = new Color(selectedColour.r, selectedColour.g, selectedColour.b, selectedColour.a);
			temp.highlightedColor = new Color(selectedColour.r, selectedColour.g, selectedColour.b, selectedColour.a);
		}
		else {
			temp.normalColor = new Color(offColour.r, offColour.g, offColour.b, offColour.a);
			temp.highlightedColor = new Color(offColour.r, offColour.g, offColour.b, offColour.a);

		}
		t.colors = temp;
	}
}
