using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerChooser1P : MonoBehaviour {

	const int MAX_STYLE = 4;
	
	public Text p1CharText;

	public int characterNumP1 = 0;
	public int styleNumP1 = 0;
	
	InputAxisDownManager axis1;
	
	
	SpriteRenderer sr1Bob;
	SpriteRenderer sr1Gary;
	Animator anim1Gary;
	TrailRenderer tr1Bob;
	
	public GameObject player1Bob;
	public GameObject player1Gary;
	
	public Sprite ballPlain;		//1,0
	public Sprite ballFrance;		//1,1
	public Sprite ballCanada;		//1,2
	public Sprite ballItaly;		//1,3
	public Material bobPlainTail;
	public Material bobFranceTail;
	public Material bobCanadaTail;
	public Material bobItalyTail;

	public AudioSource charChange;

	bool canSelect = true;
	
	// Use this for initialization
	void Start () {
		tr1Bob = player1Bob.GetComponent<TrailRenderer>();
		sr1Bob = player1Bob.GetComponent<SpriteRenderer>();
		sr1Gary = player1Gary.GetComponent<SpriteRenderer>();
		anim1Gary = player1Gary.GetComponent<Animator>();
		
		canSelect = true;
		axis1 = new InputAxisDownManager(2);	//arrow keys
		
		DisplayPlayerSprite();
	}
	
	// Update is called once per frame
	void Update () {
		if(canSelect) {
			axis1.GetKeyDowns();
			
			if(axis1.isDownKeyDown() || axis1.isUpKeyDown()) {
				ChangeCharacterP1();
			}
			if(axis1.isRightKeyDown())
				UpStyleP1();
			else if(axis1.isLeftKeyDown())
				DownStyleP1();
			
			if(Input.GetKeyDown(KeyCode.Space)) {
				canSelect = false;
			}
		}
	}
	
	void ChangeCharacterP1(){
		charChange.Play();
		characterNumP1 = (characterNumP1 + 1) % 2;
		styleNumP1 = 0;
		if(characterNumP1 == 0) {
			sr1Bob.enabled = false;
			tr1Bob.enabled = false;
			sr1Gary.enabled = true;
		}
		else{
			sr1Bob.enabled = true;
			tr1Bob.enabled = true;
			sr1Gary.enabled = false;
		}
		GameControl.control.characterNumP1 = characterNumP1;
		GameControl.control.styleNumP1 = styleNumP1;
		DisplayPlayerSprite();
	}
	
	void UpStyleP1() {
		charChange.Play();
		styleNumP1 = (styleNumP1 + 1) % MAX_STYLE;
		DisplayPlayerSprite();
		GameControl.control.characterNumP1 = characterNumP1;
		GameControl.control.styleNumP1 = styleNumP1;
	}
	
	void DownStyleP1() {
		charChange.Play();
		if(styleNumP1 == 0)
			styleNumP1 = MAX_STYLE;
		styleNumP1 = (styleNumP1 - 1) % MAX_STYLE;
		DisplayPlayerSprite();
		GameControl.control.characterNumP1 = characterNumP1;
		GameControl.control.styleNumP1 = styleNumP1;
	}
	
	void DisplayPlayerSprite(){
			if(characterNumP1 == 0) {
				sr1Bob.enabled = false;
				tr1Bob.enabled = false;
				sr1Gary.enabled = true;
				DisplaySprite(sr1Gary, anim1Gary, tr1Bob, p1CharText, characterNumP1, styleNumP1);
			}
			else {
				sr1Bob.enabled = true;
				tr1Bob.enabled = true;
				sr1Gary.enabled = false;
				DisplaySprite(sr1Bob, anim1Gary, tr1Bob, p1CharText, characterNumP1, styleNumP1);
			}
		}
	
	void DisplaySprite(SpriteRenderer sr, Animator a, TrailRenderer tr, Text t, int charNum, int styleNum){
		if(charNum == 0) {
			t.text = "Gary";
			a.SetInteger("styleNum", styleNum);
			//t.text = "Johnny";
			//t.color = new Color(153f/255f, 88f/255f, 35/255f);
		}
		else if (charNum == 1) {
			t.text = "Bob";
			switch(styleNum) {
			case 0:
				sr.sprite = ballPlain;
				tr.material = bobPlainTail;
				break;
			case 1:
				sr.sprite = ballFrance;
				tr.material = bobFranceTail;
				break;
			case 2:
				sr.sprite = ballCanada;
				tr.material = bobCanadaTail;
				break;
			case 3:
				sr.sprite = ballItaly;
				tr.material = bobItalyTail;
				break;
			}
		}
	}
}
