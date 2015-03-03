using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaceGameManager : GameManager {

	AudioSource audioSrc;

	public GameObject bobPrefab;
	public GameObject bobItalyPrefab;
	public GameObject bobFrancePrefab;
	public GameObject bobCanadaPrefab;

	public GameObject garyPrefab;
	public GameObject garyYellowPrefab;
	public GameObject garyRedPrefab;
	public GameObject garyJapanPrefab;

	public GameObject garyProjectorPrefab;
	public GameObject bobProjectorPrefab;

	public GameObject player1Spawn;
	public GameObject player2Spawn;

	public GameObject projector1Spawn;
	public GameObject projector2Spawn;

	public GameObject barrier1;
	public GameObject barrier2;

	GameObject proj1;
	GameObject proj2;

	public AudioSource horn;
	public AudioSource countdown;

	int rounds1;
	int rounds2;

	public bool gameOver;
	public bool gamePlaying;
	public bool gameStarting;

	public float gameTime;

	public float preGameTimeMax = 3;
	float preGameTime;

	public Text timeText;
	public Text winText;
	public Text round1Text;
	public Text round2Text;
	public GameObject replayText;
	public Text countDownText;

	Animator countDownAnim;

	void Awake() {
		GameManager.gm = this;
	}
	// Use this for initialization
	void Start () {
		countDownAnim = countDownText.GetComponent<Animator>();
		GameControl.control.StopMenuMusic();
		SpawnPlayers();
		SpawnProjectors();
		audioSrc = GetComponent<AudioSource>();
		Reset();
	}
	
	// Update is called once per frame
	void Update () {

		if(gamePlaying) {
			ReduceGameTime();
			if(gameTime <= 0f) {
				gameTime = 0;
				GameOver();
			}

		}
		else if(gameStarting) {
			preGameTime -= Time.deltaTime;
			countDownText.text = preGameTime.ToString("f2");
			if(preGameTime <= 0f)
				StartGame();
		}
		else if(gameOver)
		{
			if(Input.GetKey(KeyCode.Space))
				Reset();
		}
	}

	void DisplayTime() {
		timeText.text = "Time: " + ((int)gameTime)/60 + ":" + (((int)gameTime)%60).ToString("d2");
	}

	void Reset() {
		p1.transform.position = player1Spawn.transform.position;
		p2.transform.position = player2Spawn.transform.position;
		barrier1.SetActive(true);
		barrier2.SetActive(true);
		preGameTime = preGameTimeMax;
		gameOver = false;
		gamePlaying = false;
		gameStarting = true;
		score1 = 0;
		score2 = 0;
		gameTime = GameControl.control.maxTime;
		DisplayTime();
		winText.text = "";
		round1Text.text = rounds1.ToString();
		round2Text.text = rounds2.ToString();
		replayText.SetActive(false);
		countDownText.enabled = true;
		countDownAnim.SetTrigger("Start");
		countdown.Play();
	}

	void StartGame(){
		countDownText.enabled = false;
		barrier1.SetActive(false);
		barrier2.SetActive(false);
		gamePlaying = true;
		gameOver = false;
		gameStarting = false;
	}

	void GameOver() {
		horn.Play();
		gameOver = true;
		gamePlaying = false;
		gameStarting = false;
		if(score1 > score2)
			Player1Win();
		else if(score2 > score1)
			Player2Win();
		else
			Tie();
		round1Text.text = rounds1.ToString();
		round2Text.text = rounds2.ToString();
		replayText.SetActive(true);
	}

	public override void Player1Portal(){
		if(gamePlaying)
			score1++;
	}

	public override void Player2Portal(){
		if(gamePlaying)
			score2++;
	}

	public override void Player1Catch(){
		//Do Nothing
	}

	public override void Player2Catch(){
		//Do Nothing
	}

	public void Player1Win() {
		rounds1++;
		winText.color = new Color(1f ,0f, 0f, 1f);	//Todo: colour selecting 	winText.color = new Color(141f,6f,76f,255f)/255;
		winText.text = "Player 1 Wins!";
	}

	public void Player2Win() {
		rounds2++;
		winText.color = new Color(9f ,0f, 255f, 255f)/255f;	//Todo: colour selecting 	winText.color = new Color(141f,6f,76f,255f)/255;
		winText.text = "Player 2 Wins!";
	}

	public void Tie() {
		winText.color = new Color(221f,11f,255f,255f)/255;
		winText.text = "Tie!";
	}

	void ReduceGameTime()
	{
		int oldTime = (int)gameTime;
		gameTime -= Time.deltaTime;
		int newTime = (int)gameTime;
		if(oldTime != newTime){
			audioSrc.Play();
			Debug.Log("Beep");
		}
		DisplayTime();
	}

	void SpawnProjectors(){
		if(GameControl.control.characterNumP1 == 0)
			proj1 = (GameObject)GameObject.Instantiate(garyProjectorPrefab, projector1Spawn.transform.position, projector1Spawn.transform.rotation);
		else
			proj1 = (GameObject)GameObject.Instantiate(bobProjectorPrefab, projector1Spawn.transform.position, projector1Spawn.transform.rotation);
		proj1.GetComponent<ProjectShadowTowards>().obj = p1.transform;
		
		if(GameControl.control.characterNumP2 == 0)
			proj2 = (GameObject)GameObject.Instantiate(garyProjectorPrefab, projector2Spawn.transform.position, projector2Spawn.transform.rotation);
		else
			proj2 = (GameObject)GameObject.Instantiate(bobProjectorPrefab, projector2Spawn.transform.position, projector2Spawn.transform.rotation);
		proj2.GetComponent<ProjectShadowTowards>().obj = p2.transform;
		
	}
	
	void SpawnPlayers(){
		if(GameControl.control.characterNumP1 == 0) {
			switch (GameControl.control.styleNumP1) {
			case 0:
				p1 = (GameObject)GameObject.Instantiate(garyPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 1:
				p1 = (GameObject)GameObject.Instantiate(garyYellowPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 2:
				p1 = (GameObject)GameObject.Instantiate(garyRedPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 3:
				p1 = (GameObject)GameObject.Instantiate(garyJapanPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			}
			p1.GetComponent<CharacterController2D>().SetPlayer(1);
		}
		else{
			switch (GameControl.control.styleNumP1) {
			case 0:
				p1 = (GameObject)GameObject.Instantiate(bobPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 1:
				p1 = (GameObject)GameObject.Instantiate(bobFrancePrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 2:
				p1 = (GameObject)GameObject.Instantiate(bobCanadaPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 3:
				p1 = (GameObject)GameObject.Instantiate(bobItalyPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			}
			p1.GetComponent<BallMove>().SetPlayer(1);
		}
		
		if(GameControl.control.characterNumP2 == 0) {
			switch (GameControl.control.styleNumP2) {
			case 0:
				p2 = (GameObject)GameObject.Instantiate(garyPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 1:
				p2 = (GameObject)GameObject.Instantiate(garyYellowPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 2:
				p2 = (GameObject)GameObject.Instantiate(garyRedPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 3:
				p2 = (GameObject)GameObject.Instantiate(garyJapanPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			}
			p2.GetComponent<CharacterController2D>().SetPlayer(2);
		}
		else{
			switch (GameControl.control.styleNumP2) {
			case 0:
				p2 = (GameObject)GameObject.Instantiate(bobPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 1:
				p2 = (GameObject)GameObject.Instantiate(bobFrancePrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 2:
				p2 = (GameObject)GameObject.Instantiate(bobCanadaPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 3:
				p2 = (GameObject)GameObject.Instantiate(bobItalyPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			}
			p2.GetComponent<BallMove>().SetPlayer(2);
		}
	}
}
