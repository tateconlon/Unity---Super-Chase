using UnityEngine;
using System.Collections;

public class GaryStyle : MonoBehaviour {

	public int styleNum = 0;
	// Use this for initialization
	void Start () {
		GetComponent<Animator>().SetInteger("styleNum", styleNum);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
