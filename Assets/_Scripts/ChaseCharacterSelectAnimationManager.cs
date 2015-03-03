using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChaseCharacterSelectAnimationManager : MonoBehaviour {

	public GameObject camera;
	public GameObject canvas;
	public GameObject startButton;

	public Toggle course1;
	public Toggle course2;

	bool atCourseSelect = false;
	int versusMode = -1;
	int courseIndex = -1;

	Animator canvasAnim;
	Animator cameraAnim;
	Animator butAnim;
	Text butText;
	AudioSource sparkle;

	// Use this for initialization
	void Start () {
		GameControl.control.gameMode = 2;
		sparkle = GetComponent<AudioSource>();
		canvasAnim = canvas.GetComponent<Animator>();
		cameraAnim = camera.GetComponent<Animator>();
		butAnim = startButton.GetComponent<Animator>();
		butText = startButton.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			AdvanceScreen();
		}
	}

	public void AdvanceScreen() {
		if(!atCourseSelect) {
			cameraAnim.SetTrigger("Slide1");
			canvasAnim.SetTrigger("Slide1");
			atCourseSelect = true;
			butText.text = "Press space to begin!";
			sparkle.Play();

			//butAnim.SetTrigger("Normal");
		}
		else if((course1.isOn || course2.isOn))
		{
			GameControl.control.StopMenuMusic();
			sparkle.Play();
			if(course1.isOn)
				GameControl.control.level = 0;
			else if(course2.isOn)
				GameControl.control.level = 1;
			butAnim.Play("FinalPress");
		}
	}
}
