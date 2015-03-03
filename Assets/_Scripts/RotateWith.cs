using UnityEngine;
using System.Collections;

public class RotateWith : MonoBehaviour {

	public Transform matchRotation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = matchRotation.rotation;
	}
}
