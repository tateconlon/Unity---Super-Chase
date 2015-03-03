using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SinglePlayerCharacterSelectAnimationManager : MonoBehaviour {
	public GameObject camera;
	public GameObject canvas;
	public GameObject startButton;
	
	public Toggle course1;
	public Toggle course2;

	public Toggle easyMode;
	public Toggle mediumMode;
	public Toggle hardMode;
	
	bool atCourseSelect = false;
	
	Animator canvasAnim;
	Animator cameraAnim;
	Animator butAnim;
	Text butText;
	AudioSource sparkle;
	
	// Use this for initialization
	void Start () {
		sparkle = GetComponent<AudioSource>();
		GameControl.control.gameMode = 0;
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
		if(!atCourseSelect) {// && (easyMode.isOn || mediumMode.isOn || hardMode.isOn)) {
			//if(easyMode.isOn
			cameraAnim.SetTrigger("Slide1");
			canvasAnim.SetTrigger("Slide1");
			atCourseSelect = true;
			butText.text = "Press space to begin!";
			sparkle.Play();
			//butAnim.SetTrigger("Normal");
		}
		else if(course1.isOn || course2.isOn && (easyMode.isOn || mediumMode.isOn || hardMode.isOn))
		{
			GameControl.control.StopMenuMusic();
			sparkle.Play();
			if(easyMode.isOn)
				GameControl.control.gameStyle = 0;
			else if(mediumMode.isOn)
				GameControl.control.gameStyle = 1;
			else if(hardMode.isOn)
				GameControl.control.gameStyle = 2;
			if(course1.isOn)
				GameControl.control.level = 0;
			else if(course2.isOn)
				GameControl.control.level = 1;
			butAnim.Play("FinalPress");
		}
	}
}
