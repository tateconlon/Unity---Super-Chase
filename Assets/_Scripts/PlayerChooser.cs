using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerChooser : MonoBehaviour {

	const int MAX_STYLE = 4;

	public Text p1CharText;
	public Text p2CharText;

	public int characterNumP1 = 0;
	public int styleNumP1 = 0;

	public int characterNumP2 = 1;
	public int styleNumP2 = 0;

	InputAxisDownManager axis1;
	InputAxisDownManager axis2;

	
	SpriteRenderer sr1Bob;
	SpriteRenderer sr1Gary;
	Animator anim1Gary;
	TrailRenderer tr1Bob;
	
	SpriteRenderer sr2Bob;
	SpriteRenderer sr2Gary;
	Animator anim2Gary;
	TrailRenderer tr2Bob;

	public GameObject player1Bob;
	public GameObject player1Gary;
	public GameObject player2Bob;
	public GameObject player2Gary;
	 
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
		tr2Bob = player2Bob.GetComponent<TrailRenderer>();
		sr2Bob = player2Bob.GetComponent<SpriteRenderer>();
		sr2Gary = player2Gary.GetComponent<SpriteRenderer>();
		anim2Gary = player2Gary.GetComponent<Animator>();

		canSelect = true;
		axis1 = new InputAxisDownManager(1);
		axis2 = new InputAxisDownManager(2);

		DisplayPlayerSprite(1);
		DisplayPlayerSprite(2);

		GameControl.control.characterNumP1 = characterNumP1;
		GameControl.control.styleNumP1 = styleNumP1;
		GameControl.control.characterNumP2 = characterNumP2;
		GameControl.control.styleNumP2 = styleNumP2;
	}
	
	// Update is called once per frame
	void Update () {
		if(canSelect) {
			axis1.GetKeyDowns();
			axis2.GetKeyDowns();

			if(axis1.isDownKeyDown() || axis1.isUpKeyDown()) {
				Debug.Log("1 Up/Down");
				ChangeCharacterP1();
			}
			if(axis1.isRightKeyDown())
				UpStyleP1();
			else if(axis1.isLeftKeyDown())
				DownStyleP1();

			if(axis2.isDownKeyDown() || axis2.isUpKeyDown())
				ChangeCharacterP2();
			if(axis2.isRightKeyDown())
				UpStyleP2();
			else if(axis2.isLeftKeyDown())
				DownStyleP2();

			if(Input.GetKeyDown(KeyCode.Space)) {
				canSelect = false;
			}
		}
	}

	public void ChangeCharacterP1(){
		charChange.Play();
		characterNumP1 = (characterNumP1 + 1) % 2;
		styleNumP1 = 0;
		while(styleNumP1 == styleNumP2 && characterNumP1 == characterNumP2) {
			styleNumP1 = (styleNumP1 + 1) % MAX_STYLE;
		}
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
		DisplayPlayerSprite(1);
	}

	public void ChangeCharacterP2(){
		charChange.Play();
		characterNumP2 = (characterNumP2 + 1) % 2;
		styleNumP2 = 0;
		while(styleNumP2 == styleNumP1 && characterNumP1 == characterNumP2) {
			styleNumP2 = (styleNumP2 + 1) % MAX_STYLE;
		}
		if(characterNumP2 == 0) {
			sr2Bob.enabled = false;
			tr2Bob.enabled = false;
			sr2Gary.enabled = true;
		}
		else{
			sr2Bob.enabled = true;
			tr2Bob.enabled = true;
			sr2Gary.enabled = false;
		}
		GameControl.control.characterNumP2 = characterNumP2;
		GameControl.control.styleNumP2 = styleNumP2;
		DisplayPlayerSprite(2);
	}

	public void UpStyleP1() {
		charChange.Play();
		do{
			styleNumP1 = (styleNumP1 + 1) % MAX_STYLE;
		}while(styleNumP1 == styleNumP2 && characterNumP1 == characterNumP2);
		DisplayPlayerSprite(1);
		GameControl.control.characterNumP1 = characterNumP1;
		GameControl.control.styleNumP1 = styleNumP1;
	}



	public void DownStyleP1() {
		charChange.Play();
		do{
			if(styleNumP1 == 0)
				styleNumP1 = MAX_STYLE;
			styleNumP1 = (styleNumP1 - 1) % MAX_STYLE;
		}while(styleNumP1 == styleNumP2 && characterNumP1 == characterNumP2);
		DisplayPlayerSprite(1);
		GameControl.control.characterNumP1 = characterNumP1;
		GameControl.control.styleNumP1 = styleNumP1;
	}


	public void UpStyleP2() {
		charChange.Play();
		do{
			styleNumP2 = (styleNumP2 + 1) % MAX_STYLE;
		}while(styleNumP2 == styleNumP1 && characterNumP1 == characterNumP2);
		DisplayPlayerSprite(2);
		GameControl.control.characterNumP2 = characterNumP2;
		GameControl.control.styleNumP2 = styleNumP2;
	}

	public void DownStyleP2() {
		charChange.Play();
		do{
			if(styleNumP2 == 0)
				styleNumP2 = MAX_STYLE;
			styleNumP2 = (styleNumP2 - 1) % MAX_STYLE;
		}while(styleNumP2 == styleNumP1 && characterNumP1 == characterNumP2);
		DisplayPlayerSprite(2);
		GameControl.control.characterNumP2 = characterNumP2;
		GameControl.control.styleNumP2 = styleNumP2;
	}

	void DisplayPlayerSprite(int playerNum){
		if(playerNum == 1) {
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
		else {
			if(characterNumP2 == 0) {
				sr2Bob.enabled = false;
				tr2Bob.enabled = false;
				sr2Gary.enabled = true;
				DisplaySprite(sr2Gary, anim2Gary, tr2Bob, p2CharText, characterNumP2, styleNumP2);
			}
			else {
				sr2Bob.enabled = true;
				tr2Bob.enabled = true;
				sr2Gary.enabled = false;
				DisplaySprite(sr2Bob, anim2Gary, tr2Bob, p2CharText, characterNumP2, styleNumP2);
			}
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
