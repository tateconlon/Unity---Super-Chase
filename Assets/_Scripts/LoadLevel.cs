using UnityEngine;
using System.Collections;

public class LoadLevel: MonoBehaviour {

	public string levelName;


	public void Load() {
		Application.LoadLevel(levelName);
	}
}
