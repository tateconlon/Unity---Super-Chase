using UnityEngine;
using System.Collections;

public class ProjectShadowTowards : MonoBehaviour {

	public Transform obj;	//Object you want to look at
		
	// Update is called once per frame
	void Update () {
		Vector3 dir = obj.position - transform.position;
		Quaternion rot = Quaternion.LookRotation(dir);
		transform.rotation = rot;
	}
}
