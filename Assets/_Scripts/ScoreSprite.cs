using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreSprite : MonoBehaviour {

	public int playerNum = 1;
	Image i;
	SpriteRenderer sp;
	RectTransform rt;
	// Use this for initialization
	void Start () {
		i = GetComponent<Image>();
		rt = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(sp == null)	{	
			if(playerNum == 1)
			sp = GameManager.gm.p1.GetComponent<SpriteRenderer>();
		else
			sp = GameManager.gm.p2.GetComponent<SpriteRenderer>();
		}

		if(playerNum == 1 && GameManager.gm.p1 != null)
			rt.rotation = GameManager.gm.p1.transform.rotation;
		else if(GameManager.gm.p2 != null)
			rt.rotation = GameManager.gm.p2.transform.rotation;

		if(i.sprite != sp.sprite)
			i.sprite = sp.sprite;
	}
}
