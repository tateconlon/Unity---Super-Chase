using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public int characterNumP1 = 0;
	public int styleNumP1 = 0;
	
	public int characterNumP2 = 1;
	public int styleNumP2 = 0;

	public int level;

	public int gameMode;
	public int gameStyle;

	public int maxTime;

	AudioSource a;

	void Awake () {
		if(control == null) {
			DontDestroyOnLoad(gameObject);
			control = this;
		} else if (control != this) {
			Destroy(gameObject);
		}
		a = GetComponent<AudioSource>();
		StartMenuMusic();
	}

	void Update(){
		if(Input.GetKey(KeyCode.Escape)) {
			Application.LoadLevel("menu");
			StartMenuMusic();
		}
	}

	public void StartMenuMusic(){
		a.mute = false;
		a.Play();
	}

	public void StopMenuMusic() {
		a.mute = true;
	}
}
