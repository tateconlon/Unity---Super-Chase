using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterAudioManager : MonoBehaviour {

	public AudioClip[] hitSounds;

	List<AudioSource> hitAudio;


	void Awake() {
		hitAudio = new List<AudioSource>();
		foreach(AudioClip h in hitSounds) {
			hitAudio.Add(AddAudio(h));
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	AudioSource AddAudio(AudioClip clip, bool loop = false, bool playAwake = false, float vol = 1f) {
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.collider.tag == "Player") {
			hitAudio[Random.Range(0, hitAudio.Count-1)].Play();
		}
	}

}
