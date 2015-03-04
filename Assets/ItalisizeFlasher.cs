using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItalisizeFlasher : MonoBehaviour {

	Text t;
		// Use this for initialization
		void Start () {
		t = GetComponent<Text>();
		}
		
	public void Dance() {
		if(t.fontStyle == FontStyle.Italic)
			t.fontStyle = FontStyle.Normal;
		else
			t.fontStyle = FontStyle.Italic;
	}
}
