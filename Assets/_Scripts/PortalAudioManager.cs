using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalAudioManager : MonoBehaviour {
	
	public AudioClip[] wooshSounds;
	
	List<AudioSource> wooshAudio;
	
	
	void Awake() {
		wooshAudio = new List<AudioSource>();
		foreach(AudioClip w in wooshSounds) {
			wooshAudio.Add(AddAudio(w));
		}
	}
	
	AudioSource AddAudio(AudioClip clip, bool loop = false, bool playAwake = false, float vol = 1f) {
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.tag == "Player") {
			Debug.Log("Woosh");
			wooshAudio[Random.Range(0, wooshAudio.Count)].Play();
		}
	}
	
}
