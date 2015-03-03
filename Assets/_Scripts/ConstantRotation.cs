using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour {

	public Vector3 torque;
	public float rotationSpeed;
	float currRotSpeed;
	public float maxRotation;
	public bool startLeft = true;
	public bool stopRotation = true;
	// Use this for initialization
	void Start () {
		if(startLeft)
		currRotSpeed = rotationSpeed;
		else
			currRotSpeed = -rotationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if(stopRotation) {
			if(transform.rotation.eulerAngles.y >= maxRotation && transform.rotation.eulerAngles.y <= 180)
				currRotSpeed = -rotationSpeed;
			else if(transform.rotation.eulerAngles.y <= (360 - maxRotation) && transform.rotation.eulerAngles.y >= 180)
				currRotSpeed = rotationSpeed;
		}
		transform.Rotate(new Vector3(0,1,0), currRotSpeed, Space.World);
	}
}
