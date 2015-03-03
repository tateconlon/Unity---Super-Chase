using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class arrowRotate : MonoBehaviour {

	RectTransform r;
	// Use this for initialization
	void Start () {
		r = GetComponent<RectTransform>();
		GameControl.control.gameStyle = 0;
	}
	
	public void Flip() {
		r.transform.Rotate(new Vector3(0f, 0f, 1f), 180f);
		Debug.Log("Flip");
		GameControl.control.gameStyle = (GameControl.control.gameStyle + 1) % 2;	//0 is player1 as Chaser, 1 is player2 as Chaser
	}
}
