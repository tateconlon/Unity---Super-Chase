using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalSpawnManager : MonoBehaviour {
	
	int activeIndex = -1;
	public GameObject portalSpawns;
	List<Transform> spawnPoints;
	// Use this for initialization
	void Start () {
		//Get all transforms except for root
		Transform[] spawnPointTemp = portalSpawns.GetComponentsInChildren<Transform>();
		spawnPoints = new List<Transform>();
		foreach(Transform t in spawnPointTemp){	
			if(t.GetInstanceID() != portalSpawns.transform.GetInstanceID())
				spawnPoints.Add(t);
		}
		ChangeSpawn();
	}
	

	void ChangeSpawn() {
		int tempIndex = activeIndex;
		do{
			tempIndex = Random.Range(0, spawnPoints.Count-1);
		}while(tempIndex == activeIndex);
		activeIndex = tempIndex;
		transform.position = spawnPoints[activeIndex].position;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.tag == "Player") {
			int oldActive = activeIndex;
			ChangeSpawn();
			coll.gameObject.transform.position = newPlayerPosition(oldActive);
		}
	}

	Vector3 newPlayerPosition(int oldActiveIndex) {
		int tempIndex = activeIndex;
		do{
			tempIndex = Random.Range(0, spawnPoints.Count - 1);
		}while(tempIndex == activeIndex || tempIndex == oldActiveIndex);
		return new Vector3(spawnPoints[tempIndex].position.x, spawnPoints[tempIndex].position.y, 0);
	}


}
