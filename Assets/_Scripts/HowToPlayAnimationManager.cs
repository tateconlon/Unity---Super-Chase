using UnityEngine;
using System.Collections;

public class HowToPlayAnimationManager : MonoBehaviour {

	public GameObject courseCanvas;
	public GameObject cam;

	Animator charAnim;	Animator courseAnim;	Animator cameraAnim;

	bool atCast;
	
	AudioSource sparkle;
	
	// Use this for initialization
	void Start () {
		atCast = true;
		sparkle = GetComponent<AudioSource>();
		courseAnim = courseCanvas.GetComponentInChildren<Animator>();
		cameraAnim = cam.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void AdvanceScreen() {
		if(atCast) {
			cameraAnim.SetTrigger("Slide1");
			courseAnim.SetTrigger("Slide1");
			//charAnim.Play("HTPcharSlide1");
			sparkle.Play();
			atCast = false;
		}
		else
		{
			//courseAnim.SetTrigger("SlideBack");
			sparkle.Play();
			atCast = true;
		}
	}
}
